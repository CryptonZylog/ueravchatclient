// -----------------------------------------------------------------------
// <copyright file="ChannelJoinEventArgs.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Crypton.AvChat.Client.Events {
    using System;
    using System.Collections.Generic;
    
    using System.Text;
using System.Collections.Specialized;

    /// <summary>
    /// Contains information about the received inchannel packet (joined channel)
    /// </summary>
    public class ChannelJoinEventArgs : PacketEventArgs {

        /// <summary>
        /// Gets the name of channel
        /// </summary>
        public string Name {
            get;
            private set;
        }

        internal ChannelJoinEventArgs(NameValueCollection packet)
            : base(packet) {
                this.Name = packet["channel"];
        }

    }
}
