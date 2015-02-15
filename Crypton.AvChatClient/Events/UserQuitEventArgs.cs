using System;
using System.Collections.Generic;

using System.Text;
using System.Web;
using System.Collections.Specialized;

namespace Crypton.AvChat.Client.Events {
    /// <summary>
    /// Information about a user who quit the chat
    /// </summary>
    public class UserQuitEventArgs : PacketEventArgs {
        /// <summary>
        /// Gets the information about the user
        /// </summary>
        public class UserQuitData {
            /// <summary>
            /// Get the user who quit
            /// </summary>
            public string Name {
                get;
                private set;
            }
            /// <summary>
            /// Gets user's quit message (if available)
            /// </summary>
            public string Message {
                get;
                private set;
            }

            internal UserQuitData(string data) {
                string[] fields = data.Split('#');

                this.Name = Uri.UnescapeDataString(fields[0]);
                this.Message = Uri.UnescapeDataString(fields[1]);
            }
        }

/*
userquit channel="#general" data="Bob%20Smith#I%20be%20gone!"
This user is quitting the chat system, and has provided a message.  The first field is the user name, then the message, delineated with # and URL encoded.
*/
        /// <summary>
        /// Gets the name of the channel
        /// </summary>
        public string Name {
            get;
            private set;
        }
        /// <summary>
        /// Gets the information about the user who quit
        /// </summary>
        public UserQuitData UserInfo {
            get;
            private set;
        }

        internal UserQuitEventArgs(NameValueCollection packet)
            : base(packet) {

                this.Name = packet["channel"];
                this.UserInfo = new UserQuitData(packet["data"]);
        }
    }
}
