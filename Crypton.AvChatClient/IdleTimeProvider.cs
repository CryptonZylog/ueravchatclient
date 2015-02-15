using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;

using System.Text;

namespace Crypton.AvChat.Client
{
    /// <summary>
    /// Manages user's idle time and reports that to the server
    /// </summary>
    public sealed class IdleTimeProvider
    {

        private Stopwatch _resettableIdleTimer = null;
        private TimeSpan _customIdleTime;

        internal IdleTimeProvider()
        {
            this._resettableIdleTimer = new Stopwatch();
            this._resettableIdleTimer.Start();
        }

        /// <summary>
        /// Gets current idle time
        /// </summary>
        public TimeSpan IdleTime
        {
            get
            {
                switch (this.ReportMode)
                {
                    case IdleResetModes.ReportIdleTime:
                        return this._customIdleTime;
                    case IdleResetModes.ResetCounter:
                        return this._resettableIdleTimer.Elapsed;
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        /// <summary>
        /// Changes how the idle time is reported to the server
        /// </summary>
        public IdleResetModes ReportMode
        {
            get;
            set;
        }

        /// <summary>
        /// Resets internal idle timer
        /// </summary>
        public void Reset()
        {
            this._resettableIdleTimer.Reset();
            this._resettableIdleTimer.Start();
        }

        /// <summary>
        /// Reports idle time from an external source, such as time since last mouse or keyboard move
        /// </summary>
        /// <param name="idleTime"></param>
        public void Report(TimeSpan idleTime)
        {
            this._customIdleTime = idleTime;
        }

        /// <summary>
        /// Sends time back to the server
        /// </summary>
        /// <param name="dispatcher"></param>
        internal void ServerPing(Net.ISendDispatcher dispatcher)
        {            
            NameValueCollection pingResponse = new NameValueCollection();
            pingResponse["action"] = "ping";
            pingResponse["localtime"] = DateTime.Now.ToString("hh:mm tt", new CultureInfo("en-US"));
            pingResponse["idletime"] = string.Format("{0:00}:{1:00}:{2:00}", this.IdleTime.Hours, this.IdleTime.Minutes, this.IdleTime.Seconds);
            dispatcher.SendPacket(pingResponse);
        }

    }
}
