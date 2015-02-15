// -----------------------------------------------------------------------
// <copyright file="Client.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Crypton.AvChat.Client
{
    using Crypton.AvChat.Client.Events;
    using Crypton.AvChat.Client.PacketProcessors;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.IO;
    using System.Net.Sockets;
    using System.Reflection;

    /// <summary>
    /// Exposes a high level chat client
    /// </summary>
    public class ChatClient : IDisposable
    {
        /// <summary>
        /// Connection port
        /// </summary>
        private const int port = 6374;
        /// <summary>
        /// Connection Host
        /// </summary>
        private const string host = "www.uer.ca";

        private bool _disposing = false;
        private bool _disposed = false;

        private string username = null;
        private string password = null;

        private Net.ISendDispatcher sendDispatcher = null;
        private Net.IReceiveDispatcher recvDispatcher = null;

        private ConnectionStatusTypes _connectionStatus = ConnectionStatusTypes.Disconnected;
        private TcpClient tcpClient = null;

        /// <summary>
        /// Initializes a new chat client to use for connecting to AvChat Server
        /// </summary>
        public ChatClient()
        {
            this.Events = new ChatEvents();
            this.IdleTimer = new IdleTimeProvider() { ReportMode = IdleResetModes.ResetCounter };
            this.tcpClient = new TcpClient();
            this.AutoAway = new AutoAwayResponder(this.IdleTimer, this) { Enable = true };
            this.recvDispatcher = new Net.ReceiveDispatcher();
            this.sendDispatcher = new Net.SendDispatcher();
            this.recvDispatcher.PacketReceived += receiveDispatcherPacketReceivedCallback;
            this.ConnectionWatchdog = new ConnectionWatchdog(this, this.sendDispatcher, this.recvDispatcher);
            this.ConnectionWatchdog.Enable = true;

            this.ConnectionStatus = ConnectionStatusTypes.Disconnected;
            this.AutoReconnect = true;
        }
        
        internal void InternalReconnect()
        {
            this.sendDispatcher.Stop();
            this.recvDispatcher.Stop();
            this.ConnectionStatus = ConnectionStatusTypes.Reconnecting;
            this.beginConnect();
        }

        private void beginConnect()
        {
            this.tcpClient = new TcpClient();
            this.tcpClient.BeginConnect(host, port, beginSocketConnect, null);
        }

        /// <summary>
        /// Begins a login operation to the AvChat server asynchronously, with a callback function to execute with login operation result
        /// </summary>
        /// <param name="username">Username to use</param>
        /// <param name="password">Password to use</param>
        /// <param name="callback">Method to be executed with login result</param>
        public void BeginLogin(string username, string password)
        {
            this.username = username;
            this.password = password;

            if (this._disposed || this._disposing)
                throw new ObjectDisposedException(typeof(ChatClient).Name);

            if (this.ConnectionStatus == ConnectionStatusTypes.Connected)
            {
                throw new InvalidOperationException("An existing server connection is already established and authenticated");
            }
            if (this.ConnectionStatus == ConnectionStatusTypes.Connecting)
            {
                throw new InvalidOperationException("An existing connection attempt is currently being made");
            }
            if (this.ConnectionStatus == ConnectionStatusTypes.Reconnecting)
            {
                throw new InvalidOperationException("An existing connection has been lost, the client is currently trying to reconnect");
            }

            this.ConnectionStatus = ConnectionStatusTypes.Connecting;
            this.beginConnect();
        }

        private void beginSocketConnect(IAsyncResult result)
        {
            try
            {
                this.tcpClient.EndConnect(result);
                this.ConnectionStatus = ConnectionStatusTypes.SocketConnected;
                this.initializeDispatchers();
            }
            catch (SocketException socketException)
            {
                if (this.LoginEvent != null)
                {
                    foreach (LoginEventHandler handler in this.LoginEvent.GetInvocationList())
                    {
                        handler.BeginInvoke(this, false, socketException.Message, null, null);
                    }
                }
            }
            catch (ObjectDisposedException objectDisposed)
            {
                if (this.LoginEvent != null)
                {
                    foreach (LoginEventHandler handler in this.LoginEvent.GetInvocationList())
                    {
                        handler.BeginInvoke(this, false, objectDisposed.Message, null, null);
                    }
                }
            }
        }

        private void loginRequest()
        {
            LoginProcessor loginProcessor = new LoginProcessor(this.recvDispatcher, this.sendDispatcher);
            loginProcessor.Username = this.username;
            loginProcessor.Password = this.password;
            loginProcessor.LoginSuccess += loginProcessor_LoginSuccess;
            loginProcessor.LoginTimeout += loginProcessor_LoginTimeout;
            loginProcessor.BeginProcess();
        }

        private void initializeDispatchers()
        {
            this.recvDispatcher.Start(this.tcpClient);
            this.sendDispatcher.Start(this.tcpClient);
        }

        private void loginProcessor_LoginTimeout(LoginProcessor obj)
        {
            obj.Dispose();
            this.tcpClient.Close();
            this.ConnectionStatus = ConnectionStatusTypes.Disconnected;
            if (this.LoginEvent != null)
            {
                foreach (LoginEventHandler handler in this.LoginEvent.GetInvocationList())
                {
                    handler.BeginInvoke(this, false, "Login timeout, verify username and password and try again", null, null);
                }
            }
        }

        private void loginProcessor_LoginSuccess(LoginProcessor obj)
        {
            this.Name = obj.GivenName;
            obj.Dispose();
            this.ConnectionWatchdog.Enable = true;
            this.ConnectionStatus = ConnectionStatusTypes.Connected;
            if (this.LoginEvent != null)
            {
                foreach (LoginEventHandler handler in this.LoginEvent.GetInvocationList())
                {
                    handler.BeginInvoke(this, true, null, null, null);
                }
            }
        }

        private void receiveDispatcherPacketReceivedCallback(NameValueCollection obj)
        {
            string action = obj["action"];
            // filter the login packet
            // default will pass the packet on to ChatEvents class
            switch (action)
            {
                case "ident":
                    this.ConnectionStatus = ConnectionStatusTypes.Login;
                    this.loginRequest();
                    break;
                case "ping":
                    this.IdleTimer.ServerPing(this.sendDispatcher);
                    this.Events.DispatchPacketEvent(obj);
                    break;
                case "game_invite":
                    this.declineGameRequest(obj);
                    break;
                case "closewindow":
                    this.Dispose();
                    Environment.Exit(1);
                    break;
                case "byebye":
                    this.Logout();
                    this.Events.DispatchPacketEvent(obj);
                    break;
                case "quit":
                    this.Logout();
                    this.Events.DispatchPacketEvent(obj);
                    break;
                default:
                    this.Events.DispatchPacketEvent(obj);
                    break;
            }
        }

        private void declineGameRequest(NameValueCollection packet)
        {
            NameValueCollection nvcGameDecline = new NameValueCollection();
            nvcGameDecline["action"] = "game_message";
            nvcGameDecline["gamemsg"] = "decline";
            nvcGameDecline["touser"] = packet["fromUser"];
            nvcGameDecline["gamesess"] = packet["gameSess"];

            this.sendDispatcher.SendPacket(nvcGameDecline);
        }

        /// <summary>
        /// For debugging, closes underlying socket
        /// </summary>
        public void TriggerConnectionLost()
        {
            if (this.tcpClient != null && this.tcpClient.Connected)
            {
                this.tcpClient.Client.Shutdown(SocketShutdown.Both);
            }
        }

        /// <summary>
        /// Disconnects from chat server
        /// </summary>
        public void Logout()
        {
            this.ConnectionWatchdog.Enable = false;
            if (this._disposed || this._disposing)
            {
                throw new ObjectDisposedException(typeof(ChatClient).Name);
            }
            if (this.ConnectionStatus != ConnectionStatusTypes.Connected)
            {
                throw new ConnectionException(ConnectionExceptionProblems.Disconnected);
            }
            if (this.recvDispatcher != null)
            {
                this.recvDispatcher.Stop();
                this.recvDispatcher.Dispose();
                this.recvDispatcher = null;
            }
            if (this.sendDispatcher != null)
            {
                this.sendDispatcher.Stop();
                this.sendDispatcher.Dispose();
                this.sendDispatcher = null;
            }
            this.tcpClient.Close();
        }

        /// <summary>
        /// Changes user color
        /// </summary>
        /// <param name="color">New user color</param>
        public void SetColor(int red, int green, int blue)
        {
            if (this._disposed || this._disposing)
            {
                throw new ObjectDisposedException(typeof(ChatClient).Name);
            }
            if (this.ConnectionStatus != ConnectionStatusTypes.Connected)
            {
                throw new ConnectionException(ConnectionExceptionProblems.Disconnected);
            }

            string htmlColor = string.Format("{0:X2}{1:X2}{2:X2}", red, green, blue);

            NameValueCollection nvcPacket = new NameValueCollection();
            nvcPacket["action"] = "mycol";
            nvcPacket["color"] = htmlColor.ToUpper();

            this.sendDispatcher.SendPacket(nvcPacket);
        }

        /// <summary>
        /// Sends a given command to the server
        /// </summary>
        /// <param name="command">Command to send, e.g. /join #general</param>
        /// <param name="channel">Name of channel, valid only for channel-level commands</param>
        public void SendCommand(string command, string channel = null)
        {
            if (this._disposed || this._disposing)
            {
                throw new ObjectDisposedException(typeof(ChatClient).Name);
            }
            if (this.ConnectionStatus == ConnectionStatusTypes.Connected)
            {
                NameValueCollection packet = new NameValueCollection();
                packet["action"] = "command";
                packet["text"] = command;
                if (!string.IsNullOrEmpty(channel))
                {
                    packet["channel"] = channel;
                }
                this.sendDispatcher.SendPacket(packet);
            }
        }

        /// <summary>
        /// Releases all resources used and disconnects from the server (if connection was established)
        /// </summary>
        public void Dispose()
        {
            if (this._disposed || this._disposing)
                return;
            this._disposing = true;
            if (this.recvDispatcher != null)
            {
                this.recvDispatcher.Stop();
                this.recvDispatcher.Dispose();
                this.recvDispatcher = null;
            }
            if (this.sendDispatcher != null)
            {
                this.sendDispatcher.Stop();
                this.sendDispatcher.Dispose();
                this.sendDispatcher = null;
            }
            this.tcpClient.Close();
            this.tcpClient = null;
            this._disposed = true;
            this._disposing = false;
        }

        /// <summary>
        /// Fires when the connection status has changed
        /// </summary>
        public event EventHandler<ConnectionStatusChangeEventArgs> ConnectionStatusChanged;

        /// <summary>
        /// Fires on login
        /// </summary>
        public event LoginEventHandler LoginEvent;

        /// <summary>
        /// Gets auto away manager
        /// </summary>
        public AutoAwayResponder AutoAway
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the name of authenticated chat session. This is usually your username, but can have a number at the end of it
        /// </summary>
        public string Name
        {
            get;
            private set;
        }
        /// <summary>
        /// Gets or sets value indicating the chat client will auto-reconnect on lost connections
        /// </summary>
        public bool AutoReconnect
        {
            get;
            set;
        }
        /// <summary>
        /// Gets the current status of the connection
        /// </summary>
        public ConnectionStatusTypes ConnectionStatus
        {
            get { return this._connectionStatus; }
            private set
            {
                this._connectionStatus = value;
                if (this.ConnectionStatusChanged != null)
                {
                    foreach (EventHandler<ConnectionStatusChangeEventArgs> callback in this.ConnectionStatusChanged.GetInvocationList())
                    {
                        callback.BeginInvoke(this, new ConnectionStatusChangeEventArgs(this._connectionStatus, this), null, null);
                    }
                }
            }
        }

        private static Version _clientVersion = Assembly.GetExecutingAssembly().GetName().Version;
        /// <summary>
        /// Gets or sets custom client implementation version
        /// </summary>
        public static Version ClientVersion
        {
            get { return _clientVersion; }
            set { _clientVersion = value; } 
        }

        private static string _userAgent = "Crypton.AvChat.Win";
        public static string UserAgent
        {
            get { return _userAgent; }
            set { _userAgent = value; }
        }

        /// <summary>
        /// Gets the Events dispatcher
        /// </summary>
        public ChatEvents Events
        {
            get;
            private set;
        }
        /// <summary>
        /// Gets the idle timer
        /// </summary>
        public IdleTimeProvider IdleTimer
        {
            get;
            private set;
        }

        /// <summary>
        /// Handles connection health such as reconnection
        /// </summary>
        public ConnectionWatchdog ConnectionWatchdog
        {
            get;
            private set;
        }


    }

}
