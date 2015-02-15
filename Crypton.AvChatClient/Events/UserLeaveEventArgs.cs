using System;
using System.Collections.Generic;

using System.Text;
using System.Web;
using System.Collections.Specialized;

namespace Crypton.AvChat.Client.Events {
    /// <summary>
    /// Information about a user who left the channel
    /// </summary>
    public class UserLeaveEventArgs : PacketEventArgs {
        /// <summary>
        /// Gets the information about the user
        /// </summary>
        public class UserLeaveData {
            /// <summary>
            /// Get the user who left
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

            internal UserLeaveData(string data) {
                string[] fields = data.Split('#');
                
                this.Name = Uri.UnescapeDataString(fields[0]);
                this.Message = Uri.UnescapeDataString(fields[1]);
            }
        }

/*
userleave channel="#general" data="Bob%20Smith#I%20be%20gone!"
Same as userquit above, except this time he's just leaving the channel, not the whole server.  The message is usually blank.
*/
        /// <summary>
        /// Gets the name of the channel
        /// </summary>
        public string Name {
            get;
            private set;
        }
        /// <summary>
        /// Gets the information about the user who left
        /// </summary>
        public UserLeaveData UserInfo {
            get;
            private set;
        }

        internal UserLeaveEventArgs(NameValueCollection packet)
            : base(packet) {

                this.Name = packet["channel"];
                this.UserInfo = new UserLeaveData(packet["data"]);
        }
    }
}
