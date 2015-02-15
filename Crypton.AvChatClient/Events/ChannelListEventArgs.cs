using System;
using System.Collections.Generic;

using System.Text;
using System.Web;
using System.Globalization;
using System.Collections.Specialized;

namespace Crypton.AvChat.Client.Events {
    /// <summary>
    /// Response after issuing a channel list command
    /// </summary>
    public sealed class ChannelListEventArgs : PacketEventArgs {
        /// <summary>
        /// List of channels
        /// </summary>
        public IEnumerable<ChannelListItem> Channels {
            get;
            private set;
        }

        internal ChannelListEventArgs(NameValueCollection packet) : base(packet) {
            //#chan1@12@The%20Topic,#chan2@2@Nothing
            //A response to the "/list" command, this is the list of channels.  Each channel is separated by a comma, and provided as 3 fields, separated by @.  The first is the channel name, then the number of users, and then the URL-encoded channel topic.
            string[] channelList = packet["data"].Split(',');

            List<ChannelListItem> channels = new List<ChannelListItem>();

            foreach (string channelEnc in channelList) {
                channels.Add(new ChannelListItem(channelEnc));
            }

            this.Channels = channels.ToArray();
        }
    }

    /// <summary>
    /// Channel in a list (after /list command)
    /// </summary>
    public sealed class ChannelListItem {
        /// <summary>
        /// Gets the name of the channel
        /// </summary>
        public string Name {
            get;
            private set;
        }
        /// <summary>
        /// Gets the number of users in the channel
        /// </summary>
        public int UserCount {
            get;
            private set;
        }
        /// <summary>
        /// Gets the assigned topic of the channel
        /// </summary>
        public string Topic {
            get;
            private set;
        }

        internal ChannelListItem(string packetData) {

            string[] data = packetData.Split('@');

            this.Name = data[0];
            this.UserCount = int.Parse(data[1], new CultureInfo("en-US"));
            try
            {
                this.Topic = Uri.UnescapeDataString(data[2]);
            }
            catch (Exception) {
                this.Topic = data[2];
            }

        }

        public override string ToString() {
            return string.Format("{0} - {1}", this.Name, this.Topic);
        }
    }
}
