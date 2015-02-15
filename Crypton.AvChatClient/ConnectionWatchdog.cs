using System;
using System.Collections.Generic;

using System.Text;
using System.Threading;

namespace Crypton.AvChat.Client
{
    public class ConnectionWatchdog : IDisposable
    {

        public const int RECV_PACKET_TIMEOUT = 30;

        private bool failureDetected = false;
        private bool failureHandling = false;
        private Timer observerTimer = null;
        private Timer timeSinceLastPacketObserver = null;
        private AutoResetEvent connectionEvent = null;
        private ChatClient _client = null;
        private object lockState = new object();
        private Net.IReceiveDispatcher recvDispatcher = null;

        internal ConnectionWatchdog(ChatClient client, Net.ISendDispatcher sendDispatcher, Net.IReceiveDispatcher receiveDispatcher)
        {
            this._client = client;
            this.recvDispatcher = receiveDispatcher;
            this.observerTimer = new Timer(connectionStateObserver, null, 100, 100);
            this.timeSinceLastPacketObserver = new Timer(observeLastSentPacketTime, null, 100, 1000);
            sendDispatcher.SocketException += sendDispatcher_SocketException;
            receiveDispatcher.SocketException += receiveDispatcher_SocketException;
        }

        private void receiveDispatcher_SocketException(System.Net.Sockets.SocketException obj)
        {
            lock (this.lockState)
            {
                if (!this.failureHandling)
                {
                    failureDetected = true;
                }
            }
        }

        private void sendDispatcher_SocketException(System.Net.Sockets.SocketException obj)
        {
            lock (this.lockState)
            {
                if (!this.failureHandling)
                {
                    failureDetected = true;
                }
            }
        }

        private void connectionStateObserver(object state)
        {
            if (Enable)
            {
                lock (this.lockState)
                {
                    if (failureDetected && !failureHandling)
                    {
                        failureHandling = true;
                        this.handleReconnect();
                        failureDetected = false;
                        failureHandling = false;
                    }
                }
            }
        }

        private void observeLastSentPacketTime(object state)
        {
            if (Enable)
            {
                if (this.recvDispatcher.TimeSinceLastPacket.TotalSeconds > RECV_PACKET_TIMEOUT)
                {
                    if (!this.failureHandling && !this.failureDetected)
                    {
                        // not receiving any data from server
                        failureDetected = true;
                        this.timeSinceLastPacketObserver.Dispose();
                    }
                }
            }
        }

        private void handleReconnect()
        {
            this._client.ConnectionStatusChanged += _client_ConnectionStatusChanged;
            connectionEvent = new AutoResetEvent(false);
            this._client.InternalReconnect();
            connectionEvent.WaitOne();
        }

        private void _client_ConnectionStatusChanged(object sender, ConnectionStatusChangeEventArgs e)
        {
            if (e.Status == ConnectionStatusTypes.SocketConnected)
            {
                connectionEvent.Set();
            }
            if (e.Status == ConnectionStatusTypes.Connected)
            {
                this._client.ConnectionStatusChanged -= _client_ConnectionStatusChanged;
                this.timeSinceLastPacketObserver = new Timer(observeLastSentPacketTime, null, 100, 1000);
            }
        }

        public bool Enable
        {
            get;
            set;
        }

        public void Dispose()
        {
            this.timeSinceLastPacketObserver.Dispose();
            this.observerTimer.Dispose();
            if (connectionEvent != null)
                connectionEvent.Close();
        }
    }
}
