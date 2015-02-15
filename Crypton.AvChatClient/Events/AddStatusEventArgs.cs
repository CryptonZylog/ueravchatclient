using System;
using System.Collections.Generic;

using System.Text;
using System.Collections.Specialized;

namespace Crypton.AvChat.Client.Events {
    /// <summary>
    /// Information about channel status
    /// </summary>
    public class AddStatusEventArgs : PacketEventArgs {
        /*
addstat channel="#general" text="Status message?"
Add status text to the channel, such as "topic changed" or "user joined"
*/
        /// <summary>
        /// Gets the channel name
        /// </summary>
        public string Name {
            get;
            private set;
        }
        /// <summary>
        /// Gets message text
        /// </summary>
        public string Text {
            get;
            private set;
        }

        internal AddStatusEventArgs(NameValueCollection packet)
            : base(packet) {

                this.Name = packet["channel"];
                this.Text = packet["text"];

        }
    }
}
