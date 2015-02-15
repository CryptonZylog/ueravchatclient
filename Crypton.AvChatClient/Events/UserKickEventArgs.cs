// -----------------------------------------------------------------------
// <copyright file="UserKickEventArgs.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Crypton.AvChat.Client.Events {
    using System;
    using System.Collections.Generic;
    
    using System.Text;
    using System.Collections.Specialized;
    using System.Web;

    /// <summary>
    /// Fires when a user has been kicked
    /// </summary>
    public class UserKickEventArgs : PacketEventArgs {
        /// <summary>
        /// Gets the name of the channel
        /// </summary>
        public string ChannelName {
            get;
            private set;
        }
        /// <summary>
        /// Gets the reason
        /// </summary>
        public string Reason {
            get;
            private set;
        }
        /// <summary>
        /// Gets the name of user that was kicked
        /// </summary>
        public string KickedUser {
            get;
            private set;
        }
        /// <summary>
        /// Gets the user name who issued the kick command
        /// </summary>
        public string WhoKicked {
            get;
            private set;
        }

        internal UserKickEventArgs(NameValueCollection packet)
            : base(packet) {
            //Crypton3#kicked#Crypton

                this.ChannelName = packet["channel"];
                string[] fields = packet["data"].Split('#');

                this.KickedUser = Uri.UnescapeDataString(fields[0]);
                this.Reason = Uri.UnescapeDataString(fields[1]);
                this.WhoKicked = Uri.UnescapeDataString(fields[2]);

        }
    }
}
