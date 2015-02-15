using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using Crypton.AvChat.Client;
using Crypton.AvChat.Win.AppWindows;
using Crypton.AvChat.Win.ChatModel;
using Crypton.AvChat.Win.Tabs;

namespace Crypton.AvChat.Win
{
    /// <summary>
    /// Handles events and thread juggling for the client
    /// </summary>
    class ChatDispatcher : IDisposable
    {

        #region Static
        public static ChatDispatcher Singleton
        {
            get;
            set;
        }
        #endregion

        private ChatClient chatClient = null;
        private bool _disposing = false;
        private bool _disposed = false;
        private Dispatcher appDispatcher = null;
        private object lockHandle = new object();

        private Action<bool, string> loginCallback;

        #region Constructor / Disposal
        public ChatDispatcher(App instance)
        {
            this.appDispatcher = App.Current.Dispatcher;
            this.chatClient = new ChatClient();
            this.attachEvents();
        }
        public void Dispose()
        {
            if (_disposed || _disposing)
                return;
            _disposing = true;
            this.detachEvents();
            this.chatClient.Dispose();
            GC.SuppressFinalize(this);
            _disposed = true;
            _disposing = false;
        }
        #endregion

        #region Attaching/Detaching Events
        private void attachEvents()
        {
            this.chatClient.Events.AddMessageEvent += chatClient_AddMessageEvent;
            this.chatClient.Events.AddStatusEvent += chatClient_AddStatusEvent;
            this.chatClient.Events.AddTextEvent += chatClient_AddTextEvent;
            this.chatClient.Events.AlertReceived += chatClient_AlertReceived;
            this.chatClient.Events.ChannelFlagsReceived += chatClient_ChannelFlagsReceived;
            this.chatClient.Events.ChannelJoined += chatClient_ChannelJoined;
            this.chatClient.Events.ChannelKick += chatClient_ChannelKick;
            this.chatClient.Events.ChannelListReceived += chatClient_ChannelListReceived;
            this.chatClient.Events.ChannelTopicChanged += chatClient_ChannelTopicChanged;
            this.chatClient.Events.ChannelTopicReceived += chatClient_ChannelTopicReceived;
            this.chatClient.Events.ChannelUserFlagsChanged += chatClient_ChannelUserFlagsChanged;
            this.chatClient.Events.ChannelUserJoin += chatClient_ChannelUserJoin;
            this.chatClient.ConnectionStatusChanged += chatClient_ConnectionStatusChanged;
            this.chatClient.Events.ExitChannel += chatClient_ExitChannel;
            this.chatClient.Events.PingEvent += chatClient_PingEvent;
            this.chatClient.Events.PlayDogSound += chatClient_PlayDogSound;
            this.chatClient.Events.QuitEvent += chatClient_QuitEvent;
            this.chatClient.Events.ServerDisconnect += chatClient_ServerDisconnect;
            this.chatClient.Events.UserColorChanged += chatClient_UserColorChanged;
            this.chatClient.Events.UserKicked += chatClient_UserKicked;
            this.chatClient.Events.UserLeave += chatClient_UserLeave;
            this.chatClient.Events.UserListReceived += chatClient_UserListReceived;
            this.chatClient.Events.UserQuit += chatClient_UserQuit;
            this.chatClient.LoginEvent += chatClient_LoginEvent;
        }


        private void detachEvents()
        {
            this.chatClient.Events.AddMessageEvent -= chatClient_AddMessageEvent;
            this.chatClient.Events.AddStatusEvent -= chatClient_AddStatusEvent;
            this.chatClient.Events.AddTextEvent -= chatClient_AddTextEvent;
            this.chatClient.Events.AlertReceived -= chatClient_AlertReceived;
            this.chatClient.Events.ChannelFlagsReceived -= chatClient_ChannelFlagsReceived;
            this.chatClient.Events.ChannelJoined -= chatClient_ChannelJoined;
            this.chatClient.Events.ChannelKick -= chatClient_ChannelKick;
            this.chatClient.Events.ChannelListReceived -= chatClient_ChannelListReceived;
            this.chatClient.Events.ChannelTopicChanged -= chatClient_ChannelTopicChanged;
            this.chatClient.Events.ChannelTopicReceived -= chatClient_ChannelTopicReceived;
            this.chatClient.Events.ChannelUserFlagsChanged -= chatClient_ChannelUserFlagsChanged;
            this.chatClient.Events.ChannelUserJoin -= chatClient_ChannelUserJoin;
            this.chatClient.ConnectionStatusChanged -= chatClient_ConnectionStatusChanged;
            this.chatClient.Events.ExitChannel -= chatClient_ExitChannel;
            this.chatClient.Events.PingEvent -= chatClient_PingEvent;
            this.chatClient.Events.PlayDogSound -= chatClient_PlayDogSound;
            this.chatClient.Events.QuitEvent -= chatClient_QuitEvent;
            this.chatClient.Events.ServerDisconnect -= chatClient_ServerDisconnect;
            this.chatClient.Events.UserColorChanged -= chatClient_UserColorChanged;
            this.chatClient.Events.UserKicked -= chatClient_UserKicked;
            this.chatClient.Events.UserLeave -= chatClient_UserLeave;
            this.chatClient.Events.UserListReceived -= chatClient_UserListReceived;
            this.chatClient.Events.UserQuit -= chatClient_UserQuit;
            this.chatClient.LoginEvent -= chatClient_LoginEvent;
        }
        #endregion

        #region Properties
        public string Username
        {
            get
            {
                return this.chatClient.Name;
            }
        }

        public ConnectionStatusTypes ConnectionStatus
        {
            get
            {
                return this.chatClient.ConnectionStatus;
            }
        }

        public AutoAwayResponder AwayResponder
        {
            get
            {
                return this.chatClient.AutoAway;
            }
        }

        public IdleTimeProvider IdleTimer
        {
            get
            {
                return this.chatClient.IdleTimer;
            }
        }
        #endregion

        #region Events
        void chatClient_UserQuit(object sender, Client.Events.UserQuitEventArgs e)
        {
            this.appDispatcher.Invoke((Action)delegate
            {
                var tab = this.GetChatTab(e.Name);
                if (tab != null)
                {
                    try
                    {
                        tab.OnUserQuit(e);
                    }
                    catch (NotImplementedException) { }
                }
            });
        }

        void chatClient_UserListReceived(object sender, Client.Events.UserListEventArgs e)
        {
            this.appDispatcher.Invoke((Action)delegate
            {
                var tab = this.GetChatTab(e.Name);
                if (tab != null)
                {
                    try
                    {
                        tab.OnUserListReceived(e);
                    }
                    catch (NotImplementedException) { }
                }
            });
        }

        void chatClient_UserLeave(object sender, Client.Events.UserLeaveEventArgs e)
        {
            this.appDispatcher.Invoke((Action)delegate
            {
                var tab = this.GetChatTab(e.Name);
                if (tab != null)
                {
                    try
                    {
                        tab.OnUserLeave(e);
                    }
                    catch (NotImplementedException) { }
                }
            });
        }

        void chatClient_UserKicked(object sender, Client.Events.UserKickEventArgs e)
        {
            this.appDispatcher.Invoke((Action)delegate
            {
                var tab = this.GetChatTab(e.ChannelName);
                if (tab != null)
                {
                    try
                    {
                        tab.OnUserKicked(e);
                    }
                    catch (NotImplementedException) { }
                }
            });
        }

        void chatClient_UserColorChanged(object sender, Client.Events.ColorChangeEventArgs e)
        {
            Properties.Settings.Default.UserColor = e.HtmlColor;
            Properties.Settings.Default.Save();
        }

        void chatClient_ServerDisconnect(object sender, Client.Events.ServerDisconnectEventArgs e)
        {
            this.LogToConsole(e.Text);
        }

        void chatClient_QuitEvent(object sender, EventArgs e)
        {
        }

        void chatClient_PlayDogSound(object sender, Client.Events.PlayDogEventArgs e)
        {
            NotificationService.NotifyDogSound(e.Index);
        }

        void chatClient_PingEvent(object sender, Client.Events.PingEventArgs e)
        {
            this.appDispatcher.Invoke((Action)delegate
            {
                var allTabs = this.GetAllTabs();
                foreach (IChatTab tab in allTabs)
                {
                    try
                    {
                        tab.OnTimeListUpdate(e.TimeList);
                    }
                    catch (NotImplementedException) { }
                }
            });
        }

        void chatClient_ExitChannel(object sender, Client.Events.ExitChannelEventArgs e)
        {

        }

        void chatClient_ConnectionStatusChanged(object sender, ConnectionStatusChangeEventArgs e)
        {
            this.appDispatcher.BeginInvoke(new Action(delegate
            {
                if (e.Status == ConnectionStatusTypes.Connected)
                {
                    // if we are connected, auto join chans
                    this.AutoJoinList();
                }
                foreach (Window wnd in App.Current.Windows)
                {
                    if (wnd is ChatWindow)
                    {
                        (wnd as ChatWindow).OnConnectionStatusChange(e.Status);
                    }
                }
            }));
            switch (e.Status)
            {
                case ConnectionStatusTypes.Connecting:
                    this.LogToAll("connecting to server...");
                    break;
                case ConnectionStatusTypes.ConnectionLost:
                    this.LogToAll("server connection lost");
                    break;
                case ConnectionStatusTypes.Disconnected:
                    this.LogToAll("disconnected from server");
                    break;
                case ConnectionStatusTypes.Login:
                    this.LogToAll("logging in...");
                    break;
                case ConnectionStatusTypes.Reconnecting:
                    this.LogToAll("reconnecting to server...");
                    break;
                case ConnectionStatusTypes.SocketConnected:
                    this.LogToAll("socket established");
                    break;
                case ConnectionStatusTypes.Connected:
                    this.LogToAll("connection established, welcome to AvChat");
                    if (!string.IsNullOrEmpty(Properties.Settings.Default.UserColor))
                    {
                        this.SetColor(Properties.Settings.Default.UserColor);
                    }
                    break;
            }
        }

        void chatClient_ChannelUserJoin(object sender, Client.Events.UserJoinEventArgs e)
        {
            this.appDispatcher.Invoke((Action)delegate
            {
                var tab = this.GetChatTab(e.Name);
                if (tab != null)
                {
                    try
                    {
                        tab.OnUserJoined(e);
                    }
                    catch (NotImplementedException) { }
                }
            });
        }

        void chatClient_ChannelUserFlagsChanged(object sender, Client.Events.FlagStatusEventArgs e)
        {
            this.appDispatcher.Invoke((Action)delegate
            {
                var tab = this.GetChatTab(e.Name);
                if (tab != null)
                {
                    try
                    {
                        tab.OnUserFlagsChanged(e);
                    }
                    catch (NotImplementedException) { }
                }
            });
        }

        void chatClient_ChannelTopicReceived(object sender, Client.Events.ChannelTopicEventArgs e)
        {
            this.appDispatcher.Invoke((Action)delegate
            {
                var channelTab = this.GetChannelTab(e.Name);
                try
                {
                    channelTab.OnTopicReceived(e);
                }
                catch (NotImplementedException) { }
            });
        }

        void chatClient_ChannelTopicChanged(object sender, Client.Events.ChangeTopicEventArgs e)
        {
            this.appDispatcher.Invoke((Action)delegate
            {
                var channelTab = this.GetChannelTab(e.Name);
                try
                {
                    channelTab.OnTopicChanged(e);
                }
                catch (NotImplementedException) { }
            });
        }

        void chatClient_ChannelListReceived(object sender, Client.Events.ChannelListEventArgs e)
        {
            this.appDispatcher.Invoke((Action)delegate
            {
                App.Current.OpenChannelList(e);
            });
        }

        void chatClient_ChannelKick(object sender, Client.Events.KickChannelEventArgs e)
        {
            this.appDispatcher.Invoke((Action)delegate
            {
                var tab = this.GetChatTab(e.Name);
                if (tab != null)
                {
                    try
                    {
                        tab.OnKickedFromChannel(e);
                    }
                    catch (NotImplementedException) { }
                }
            });
        }

        void chatClient_ChannelJoined(object sender, Client.Events.ChannelJoinEventArgs e)
        {
            this.appDispatcher.Invoke((Action)delegate
            {
                IChatTab tab = this.GetChannelTab(e.Name);
                if (tab != null)
                {
                    try
                    {
                        tab.OnChannelJoined(e);
                    }
                    catch (NotImplementedException) { }
                }
            });
        }

        void chatClient_ChannelFlagsReceived(object sender, Client.Events.ChannelFlagsEventArgs e)
        {
            // Not Implemented on the server
        }

        void chatClient_AlertReceived(object sender, Client.Events.AlertEventArgs e)
        {
            this.appDispatcher.BeginInvoke(new Action(delegate
            {
                IEnumerable<IChatTab> allTabs = this.GetAllTabs();
                string alert = string.Format("Alert received from {0}", e.Name);
                if (allTabs.Count() == 0)
                {
                    // log to console
                    this.LogToConsole(alert);
                }
                else
                {
                    foreach (var tab in allTabs)
                    {
                        tab.ChatDocument.Nodes.Add(new ChatStatusMessageNode() { Text = alert });
                    }
                }
                NotificationService.NotifyAlert();
            }));
        }

        void chatClient_AddTextEvent(object sender, Client.Events.AddTextEventArgs e)
        {
            this.appDispatcher.BeginInvoke(new Action(delegate
            {
                // find the relevant tab                
                IChatTab chatTab = this.GetTab(e.Name);
                ChatDocument document = chatTab.ChatDocument;
                document.Nodes.Add(new ChatStatusMessageNode(e));
            }));
        }

        void chatClient_AddStatusEvent(object sender, Client.Events.AddStatusEventArgs e)
        {
            this.appDispatcher.BeginInvoke(new Action(delegate
            {
                // find the relevant tab
                IChatTab chatTab = this.GetTab(e.Name);
                ChatDocument document = chatTab.ChatDocument;
                document.Nodes.Add(new ChatStatusMessageNode(e));
            }));
        }

        void chatClient_AddMessageEvent(object sender, Client.Events.AddMessageEventArgs e)
        {
            this.appDispatcher.BeginInvoke(new Action(delegate
            {
                IChatTab tab = this.GetTab(e.ChannelName);
                ChatDocument document = tab.ChatDocument;
                document.Nodes.Add(new ChatUserMessageNode(e));
                tab.OnMessageReceived(e);
            }));
        }

        void chatClient_LoginEvent(ChatClient chatClient, bool loginResult, string errorMessage)
        {
            if (this.loginCallback != null)
            {
                this.appDispatcher.Invoke(this.loginCallback, loginResult, errorMessage);
            }
        }
        #endregion

        #region Login
        /// <summary>
        /// Begins a Login operation, results of which are returned in the callback
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <param name="callback">The callback function to execute when login operation completes. First argument is true/false if operation succeeded, second is the error message.</param>
        public void BeginLogin(string username, string password, Action<bool, string> callback)
        {
            this.loginCallback = callback;
            this.chatClient.BeginLogin(username, password);
        }
        #endregion

        #region Getting Tab References
        private IChatTab GetChatTab(string tabName)
        {
            IChatTab foundTab = null;
            lock (this.lockHandle)
            {
                ChatWindow foundWindow = null;
                foreach (Window window in App.Current.Windows)
                {
                    if (window is ChatWindow)
                    {
                        foundWindow = window as ChatWindow;
                        foundTab = foundWindow.FindTab<IChatTab>(tabName);
                        if (foundTab != null)
                        {
                            break;
                        }
                    }
                }
            }
            return foundTab;
        }

        /// <summary>
        /// Gets or creates a console tab
        /// </summary>
        /// <returns></returns>
        private ConsoleTab GetConsoleTab()
        {
            ConsoleTab foundTab = null;
            lock (this.lockHandle)
            {
                ChatWindow foundWindow = null;
                foreach (Window window in App.Current.Windows)
                {
                    if (window is ChatWindow)
                    {
                        foundWindow = window as ChatWindow;
                        foundTab = foundWindow.FindTab<ConsoleTab>("Console");
                        if (foundTab != null)
                        {
                            break;
                        }
                    }
                }

                if (foundWindow == null)
                {
                    // no open windows exist, create a new window
                    foundWindow = new ChatWindow();
                    foundWindow.Show();
                }

                if (foundTab == null)
                {
                    // no tab found, create it in the window we have
                    foundTab = new ConsoleTab();
                    foundWindow.RegisterTab(foundTab);
                }
            }
            return foundTab;
        }

        private IChatTab GetTab(string tabName)
        {
            if (string.IsNullOrEmpty(tabName) || tabName == "--status--")
            {
                return GetConsoleTab();
            }
            if (tabName.StartsWith("#"))
            {
                return GetChannelTab(tabName);
            }
            return GetPrivateTab(tabName);
        }

        /// <summary>
        /// Gets or creates a channel tab
        /// </summary>
        /// <param name="channelName"></param>
        /// <returns></returns>
        private ChannelTab GetChannelTab(string channelName)
        {
            ChannelTab foundTab = null;
            lock (this.lockHandle)
            {
                ChatWindow foundWindow = null;
                foreach (Window window in App.Current.Windows)
                {
                    if (window is ChatWindow)
                    {
                        foundWindow = window as ChatWindow;
                        foundTab = foundWindow.FindTab<ChannelTab>(channelName);
                        if (foundTab != null)
                        {
                            break;
                        }
                    }
                }

                if (foundWindow == null)
                {
                    // no open windows exist, create a new window
                    foundWindow = new ChatWindow();
                    foundWindow.Show();
                }

                if (foundTab == null)
                {
                    // no tab found, create it in the window we have
                    foundTab = new ChannelTab(channelName);
                    foundWindow.RegisterTab(foundTab);
                }
            }
            return foundTab;
        }

        public PrivateTab GetPrivateTab(string name)
        {
            PrivateTab foundTab = null;
            lock (this.lockHandle)
            {
                ChatWindow foundWindow = null;
                foreach (Window window in App.Current.Windows)
                {
                    if (window is ChatWindow)
                    {
                        foundWindow = window as ChatWindow;
                        foundTab = foundWindow.FindTab<PrivateTab>(name);
                        if (foundTab != null)
                        {
                            break;
                        }
                    }
                }

                if (foundWindow == null)
                {
                    // no open windows exist, create a new window
                    foundWindow = new ChatWindow();
                    foundWindow.Show();
                }

                if (foundTab == null)
                {
                    // no tab found, create it in the window we have
                    foundTab = new PrivateTab(name);
                    foundWindow.RegisterTab(foundTab);
                }
            }
            return foundTab;
        }

        /// <summary>
        /// Gets all tabs in the client
        /// </summary>
        /// <returns></returns>
        private IEnumerable<IChatTab> GetAllTabs()
        {
            lock (this.lockHandle)
            {
                foreach (Window wnd in App.Current.Windows)
                {
                    if (wnd is ChatWindow)
                    {
                        foreach (IChatTab tab in (wnd as ChatWindow).GetAllTabs())
                        {
                            yield return tab;
                        }
                    }
                }
            }
        }
        #endregion

        private void AutoJoinList()
        {
            var list = Properties.Settings.Default.AutoJoinList;
            if (list != null)
            {
                foreach (string name in list)
                {
                    if (!string.IsNullOrEmpty(name))
                    {
                        this.SendCommand("/join " + name);
                    }
                }
            }
        }

        public void LogToConsole(string message)
        {
            if (!(_disposed || _disposing))
            {
                this.appDispatcher.Invoke(new Action(delegate
                {
                    var consoleTab = this.GetConsoleTab();
                    consoleTab.ChatDocument.Nodes.Add(new ChatStatusMessageNode() { Text = "** " + message });
                }));
            }
        }

        public void LogToAll(string message)
        {
            if (!(_disposed || _disposing))
            {
                this.appDispatcher.Invoke((Action)delegate
                {
                    var tabs = this.GetAllTabs();
                    foreach (var tab in tabs)
                    {
                        tab.ChatDocument.Nodes.Add(new ChatStatusMessageNode() { Text = "** " + message });
                    }
                });
            }
        }

        public void SendCommand(string command, string channel = null)
        {
            try
            {
                this.chatClient.SendCommand(command, channel);
            }
            catch (Exception any)
            {
                this.LogToConsole("SendCommand failed: " + any.Message);
            }
        }

        public void SetColor(string color)
        {
            var sysColor = System.Drawing.ColorTranslator.FromHtml(color);
            this.chatClient.SetColor(sysColor.R, sysColor.G, sysColor.B);
        }

        public void DebugKillConnection()
        {
            this.LogToConsole("DEBUG: Killing tcp socket");
            this.chatClient.TriggerConnectionLost();
        }

    }
}
