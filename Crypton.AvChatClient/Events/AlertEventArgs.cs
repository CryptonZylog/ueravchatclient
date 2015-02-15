// -----------------------------------------------------------------------
// <copyright file="AlertEventArgs.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Crypton.AvChat.Client.Events {
    using System;
    using System.Collections.Generic;
    
    using System.Text;
    using System.Collections.Specialized;

    /// <summary>
    /// Information about an incoming alert
    /// </summary>
    public class AlertEventArgs : PacketEventArgs {
        /// <summary>
        /// Username of person who sent the alert
        /// </summary>
        public string Name {
            get;
            private set;
        }

        internal AlertEventArgs(NameValueCollection packet) : base(packet) {
            this.Name = packet["alertfrom"];
        }
    }
}
