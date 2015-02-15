using System;
using System.Collections.Generic;

using System.Text;
using System.Collections.Specialized;
using System.Web;
using System.Globalization;

namespace Crypton.AvChat.Client.Events {

    /// <summary>
    /// User list for a channel has been received
    /// </summary>
    public class UserListEventArgs : PacketEventArgs {

        /// <summary>
        /// Contains information about the user in the channel
        /// </summary>
        public class UserListItem {
            /// <summary>
            /// Gets the user ID
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
            /// Gets user flags
            /// </summary>
            public UserFlags Flags {
                get;
                private set;
            }
            /// <summary>
            /// Gets user's local time
            /// </summary>
            public DateTime? LocalTime {
                get;
                private set;
            }
            /// <summary>
            /// Gets client version info
            /// </summary>
            public string ClientVersion {
                get;
                private set;
            }

            internal UserListItem(string item) {
                string[] fields = item.Split('#');
                /*
                The fields are:
                - User ID
                - User Name
                - User Flags (see below)
                - User Local Time
                - Client Version (what chat client and version they are using)
                 */

                this.UserID = int.Parse(fields[0]);
                this.Name = Uri.UnescapeDataString(fields[1]);
                this.Flags = (UserFlags)int.Parse(fields[2]);
                if (!string.IsNullOrEmpty(fields[3])) {
                    this.LocalTime = DateTime.Parse(Uri.UnescapeDataString(fields[3]), new CultureInfo("en-US"));
                }
                this.ClientVersion = Uri.UnescapeDataString(fields[4]);
            }
        }

        /// <summary>
        /// Gets the name of the channel
        /// </summary>
        public string Name {
            get;
            private set;
        }
        /// <summary>
        /// Gets a list of users in the channel
        /// </summary>
        public IEnumerable<UserListItem> Users {
            get;
            private set;
        }

        internal UserListEventArgs(NameValueCollection packet)
            : base(packet) {

            this.Name = packet["channel"];
            string list = packet["list"];

            /*
             *  userlist channel="#general" list="userID#Bob%20Smith#User Flags#Local Time#Client Version"
                The server is telling you who is in this channel.  The list is comma-separated, with each field delineated with a # symbol.  Certain fields are URL encoded.  The fields are:
                - User ID
                - User Name
                - User Flags (see below)
                - User Local Time
                - Client Version (what chat client and version they are using)

                User Flags are:
                - 1: The user is away
                - 2: The user is an op
                - 4: The user has voice

                So if a user was away and an op, that would be 1+2 = 3.
             */

            string[] userList = list.Split(',');

            List<UserListItem> users = new List<UserListItem>();
            foreach (string userfield in userList) {
                users.Add(new UserListItem(userfield));
            }
            this.Users = users.AsReadOnly();


        }

    }



}
