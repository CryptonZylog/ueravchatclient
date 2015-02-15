using System;
using System.Collections.Generic;

using System.Text;
using System.Collections.Specialized;

namespace Crypton.AvChat.Client.Events {
    /// <summary>
    /// Information contained within the addtext packet (server message)
    /// </summary>
    public class AddTextEventArgs : PacketEventArgs {

        /// <summary>
        /// Gets the name of the channel. If null, there is no channel and the message should be displayed somewhere in the main window or the log
        /// </summary>
        public string Name {
            get;
            private set;
        }
        /// <summary>
        /// The text from the server
        /// </summary>
        public string Text {
            get;
            private set;
        }

        internal AddTextEventArgs(NameValueCollection packet)
            : base(packet) {
            /*
             When something simple needs to be printed into a channel, usually for server commands, the server will send you an "addtext" command.  This command looks like this:

SERVER: addtext channel="#general" text="** you're not in this channel."*/

                this.Name = packet["channel"];
                this.Text = packet["text"];

        }

    }
}
