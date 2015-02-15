// -----------------------------------------------------------------------
// <copyright file="ChannelFlagsEventArgs.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Crypton.AvChat.Client.Events {
    using System;
    using System.Collections.Generic;
    
    using System.Text;
    using System.Collections.Specialized;

    /// <summary>
    /// Channel configuration flags, only 'TopicChangeOps' is supported on the server at this time
    /// </summary>
    [Flags]
    public enum ChannelFlags : int {
        /// <summary>
        /// No flags have been set, or unknown
        /// </summary>
        Undefined = 0,
        /// <summary>
        /// Only ops can change the topic
        /// </summary>
        TopicChangeOps = 1,
        /// <summary>
        /// Moderated channel
        /// </summary>
        Moderated = 2,
        /// <summary>
        /// Secret channel
        /// </summary>
        Secret = 4,
        /// <summary>
        /// Invite-only channel
        /// </summary>
        Invite = 8
    }

    /// <summary>
    /// Information about channel flags
    /// </summary>
    public class ChannelFlagsEventArgs : PacketEventArgs {

        /// <summary>
        /// Gets the name of the channel
        /// </summary>
        public string Name {
            get;
            private set;
        }

        /// <summary>
        /// Gets the channel flags
        /// </summary>
        public ChannelFlags Flags {
            get;
            private set;
        }

        internal ChannelFlagsEventArgs(NameValueCollection packet)
            : base(packet) {

            this.Name = packet["channel"];
            this.Flags = (ChannelFlags)int.Parse(packet["data"]);

        }

    }
}
