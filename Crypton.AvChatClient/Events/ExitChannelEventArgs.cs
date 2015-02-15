using System;
using System.Collections.Generic;

using System.Text;
using System.Collections.Specialized;

namespace Crypton.AvChat.Client.Events {
    /// <summary>
    /// Information about a channel exit event
    /// </summary>
    public class ExitChannelEventArgs : PacketEventArgs {
        /// <summary>
        /// Gets the name of the channel
        /// </summary>
        public string Name {
            get;
            private set;
        }

        internal ExitChannelEventArgs(NameValueCollection packet)
            : base(packet) {
            //exitchannel

                this.Name = packet["channel"];

        }
    }
}
