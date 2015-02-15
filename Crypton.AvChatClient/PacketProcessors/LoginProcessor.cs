using Crypton.AvChat.Client.Net;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

using System.Text;
using System.Threading;

namespace Crypton.AvChat.Client.PacketProcessors
{
    class LoginProcessor : IDisposable
    {
        private Timer loginTimeoutTimer = null;
        private bool loginSuccessful = false;

        private IReceiveDispatcher receiveDispatcher;
        private ISendDispatcher sendDispatcher;

        public const int TIMEOUT_S = 10;
        
        public string Username
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public string GivenName
        {
            get;
            private set;
        }

        public event Action<LoginProcessor> LoginSuccess;
        public event Action<LoginProcessor> LoginTimeout;

        public LoginProcessor(IReceiveDispatcher receiveDispatcher, ISendDispatcher sendDispatcher)
        {
            this.receiveDispatcher = receiveDispatcher;
            this.sendDispatcher = sendDispatcher;
        }

        public void BeginProcess()
        {
            this.receiveDispatcher.PacketReceived += packetReceived;
            this.loginTimeoutTimer = new Timer(loginTimeout, null, TIMEOUT_S * 1000, Timeout.Infinite);
            this.sendLoginCredentials();
            this.sendClientVersion();
        }

        public void Dispose()
        {
            this.receiveDispatcher.PacketReceived -= packetReceived;
        }

        private void packetReceived(NameValueCollection obj)
        {
            string action = obj["action"];
            switch (action)
            {
                case "ident":
                    this.sendLoginCredentials();
                    this.sendClientVersion();
                    break;
                case "ready": // login auth success
                    this.loginSuccess(obj);
                    break;
            }
        }

        private void loginTimeout(object state)
        {
            this.loginTimeoutTimer.Dispose();
            if (!loginSuccessful)
            {
                // timeout has happened since info was never received from server
                if (this.LoginTimeout != null)
                {
                    foreach (Action<LoginProcessor> callback in this.LoginTimeout.GetInvocationList())
                    {
                        callback.BeginInvoke(this, null, null);
                    }
                }
            }
            this.Dispose();
        }

        private void loginSuccess(NameValueCollection resp)
        {
            this.loginSuccessful = true;
            this.GivenName = resp["youare"];
            if (this.LoginSuccess != null) {
                foreach (Action<LoginProcessor> callback in this.LoginSuccess.GetInvocationList())
                {
                    callback.BeginInvoke(this, null, null);
                }
            }
            this.Dispose();
        }

        private void sendLoginCredentials()
        {
            NameValueCollection nvcIdentResponse = new NameValueCollection();
            nvcIdentResponse["action"] = "ident";
            nvcIdentResponse["usern"] = this.Username;
            nvcIdentResponse["passw"] = this.Password;
            this.sendDispatcher.SendPacket(nvcIdentResponse);
        }

        private void sendClientVersion()
        {
            NameValueCollection nvcMyVer = new NameValueCollection();
            nvcMyVer["action"] = "myver";
            nvcMyVer["version"] = ChatClient.UserAgent + "/" + ChatClient.ClientVersion;
            this.sendDispatcher.SendPacket(nvcMyVer);
        }
    }
}
