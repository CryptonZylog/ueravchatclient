using System;
using System.Collections.Generic;

using System.Text;
using System.Threading;

namespace Crypton.AvChat.Client
{

    /// <summary>
    /// Manages automatically setting away status
    /// </summary>
    public sealed class AutoAwayResponder : IDisposable
    {
        /// <summary>
        /// Gets the default away message
        /// </summary>
        public const string DefaultMessage = "Auto Away after 15 minutes of idle time";
        /// <summary>
        /// Gets default away threshold of 15 minutes
        /// </summary>
        public static readonly TimeSpan DefaultAwayThreshold = new TimeSpan(0, 15, 0);

        private Timer _idleTimeWatcher = null;
        private IdleTimeProvider _idleTimeProvider = null;
        private ChatClient _client = null;
        private bool autoAwaySet = false;

        internal AutoAwayResponder(IdleTimeProvider idleTimeProvider, ChatClient client)
        {
            this._idleTimeProvider = idleTimeProvider;
            this._client = client;
            this.AwayThreshold = DefaultAwayThreshold;
            this.AwayMessage = DefaultMessage;
            this.Enable = true;
        }
        
        private void refreshIdleTime(object state)
        {
            if (this._client.ConnectionStatus == ConnectionStatusTypes.Connected)
            {
                if (autoAwaySet)
                {
                    // currently away
                    if (this._idleTimeProvider.IdleTime < this.AwayThreshold)
                    {
                        // we are idling less than threshold
                        // clear away
                        if (this.autoAwaySet) {
                            this._client.SendCommand("/autoaway", "--status--");
                            this.autoAwaySet = false;
                        }
                    }
                }
                else
                {
                    // not away
                    if (this._idleTimeProvider.IdleTime > this.AwayThreshold)
                    {
                        // we are idling more than threshold
                        // set away
                        if (!this.autoAwaySet) {
                            this._client.SendCommand(string.Format("/autoaway {0}", this.AwayMessage));
                            this.autoAwaySet = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the state of auto away responder
        /// </summary>
        public bool Enable
        {
            get
            {
                return this._idleTimeWatcher != null;
            }
            set
            {
                if (value)
                {
                    this._idleTimeWatcher = new Timer(refreshIdleTime, null, 0, 1000);
                }
                else
                {
                    if (this._idleTimeWatcher != null)
                    {
                        this._idleTimeWatcher.Dispose();
                        this._idleTimeWatcher = null;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the away threshold
        /// </summary>
        public TimeSpan AwayThreshold
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the away message
        /// </summary>
        public string AwayMessage
        {
            get;
            set;
        }

        public void Dispose()
        {
            if (this._idleTimeWatcher != null)
            {
                this._idleTimeWatcher.Dispose();
                this._idleTimeWatcher = null;
            }
        }
    }
}
