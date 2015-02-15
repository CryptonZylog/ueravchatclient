using System;
using System.Collections.Generic;

using System.Text;
using System.Collections.Specialized;

namespace Crypton.AvChat.Client.Events {
    /// <summary>
    /// Information about a kicked out user
    /// </summary>
    public class KickChannelEventArgs : PacketEventArgs {
        /*
        kickchannel channel="#general" text="you've been kicked"
        You've been kicked out of this channel, with the reason given in Text.  Please do not auto-rejoin.
         */

        /// <summary>
        /// Gets the channel name
        /// </summary>
        public string Name {
            get;
            private set;
        }
        /// <summary>
        /// Gets the reason message
        /// </summary>
        public string Message {
            get;
            private set;
        }

        internal KickChannelEventArgs(NameValueCollection packet)
            : base(packet) {

                this.Name = packet["channel"];
                this.Message = packet["text"];

        }

    }
}
