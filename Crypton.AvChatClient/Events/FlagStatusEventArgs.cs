using System;
using System.Collections.Generic;

using System.Text;
using System.Collections.Specialized;
using System.Web;

namespace Crypton.AvChat.Client.Events {
    /// <summary>
    /// Information about updated user status
    /// </summary>
    public class FlagStatusEventArgs : PacketEventArgs {
/*
 flagstatus channel="#general" data="Bob%20Smith:User Flags"
This user's User Flags have changed.  This is usually broadcast when a user is no longer away, or goes away.  The first field is the name, the second field is the new User Flags (see above)
*/

        /// <summary>
        /// Gets the name of the channel
        /// </summary>
        public string Name {
            get;
            private set;
        }
        /// <summary>
        /// Gets the name of the user
        /// </summary>
        public string UserName {
            get;
            private set;
        }
        /// <summary>
        /// Gets the status flags
        /// </summary>
        public UserFlags Flags {
            get;
            private set;
        }

        internal FlagStatusEventArgs(NameValueCollection packet)
            : base(packet) {

                this.Name = packet["channel"];
                string data = packet["data"];
                string[] fields = data.Split(':');

                this.UserName = Uri.UnescapeDataString(fields[0]);
                this.Flags = (UserFlags)int.Parse(fields[1]);

        }

    }
}
