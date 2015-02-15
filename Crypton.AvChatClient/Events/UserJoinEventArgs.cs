using System;
using System.Collections.Generic;

using System.Text;
using System.Collections.Specialized;
using System.Web;
using System.Globalization;

namespace Crypton.AvChat.Client.Events {
    /// <summary>
    /// Information about a user who has joined a channel
    /// </summary>
    public class UserJoinEventArgs : PacketEventArgs {

        /// <summary>
        /// Information about the user
        /// </summary>
        public class UserData {
            /// <summary>
            /// Gets the UER User Id
            /// </summary>
            public int UserID {
                get;
                private set;
            }
            /// <summary>
            /// Gets the user name
            /// </summary>
            public string Name {
                get;
                private set;
            }
            /// <summary>
            /// Gets the local time of user, if available. Do not rely on date information
            /// </summary>
            public DateTime? LocalTime {
                get;
                private set;
            }
            /// <summary>
            /// Gets the information about user's chat client
            /// </summary>
            public string ClientVersion {
                get;
                private set;
            }
            /// <summary>
            /// Gets user channel flags
            /// </summary>
            public UserFlags Flags {
                get;
                private set;
            }

            internal UserData(string data) {
                string[] fields = data.Split('#');

                this.UserID = int.Parse(fields[0]);
                this.Name = Uri.UnescapeDataString(fields[1]);
                this.Flags = (UserFlags)int.Parse(fields[2]);
                if (!string.IsNullOrEmpty(fields[3])) {
                    this.LocalTime = DateTime.Parse(Uri.UnescapeDataString(fields[3]), new CultureInfo("en-US"));
                }
                this.ClientVersion = Uri.UnescapeDataString(fields[4]);
            }
        }

        /*
userjoin channel="#general" data="userID#Bob%20Smith#User Flags#User Local Time#Client Version"
A new person has joined this channel.  The information is the same as above in User List.
*/

        /// <summary>
        /// Gets the name of the channel
        /// </summary>
        public string Name {
            get;
            private set;
        }

        /// <summary>
        /// Gets the user who joined
        /// </summary>
        public UserData User {
            get;
            private set;
        }

        internal UserJoinEventArgs(NameValueCollection packet)
            : base(packet) {

                this.Name = packet["channel"];
                this.User = new UserData(packet["data"]);

        }

    }
}
