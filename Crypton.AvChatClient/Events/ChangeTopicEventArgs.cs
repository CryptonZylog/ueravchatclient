using System;
using System.Collections.Generic;

using System.Text;
using System.Collections.Specialized;
using System.Web;

namespace Crypton.AvChat.Client.Events {
    /// <summary>
    /// Information about a changed topic in the channel
    /// </summary>
    public class ChangeTopicEventArgs : PacketEventArgs {
        /*
        changetopic channel="#general" topic="New%20Topic:Bob%20Smith"
        The topic in this channel has changed.  The new topic is the first field, the person who changed it is the second field, separated by : and URL encoded.
         */

        /// <summary>
        /// Contains information about the topic
        /// </summary>
        public class TopicInfo {
            /// <summary>
            /// Gets the topic text
            /// </summary>
            public string Topic {
                get;
                private set;
            }
            /// <summary>
            /// Gets the user's name
            /// </summary>
            public string UserName {
                get;
                private set;
            }

            internal TopicInfo(string topic) {
                string[] fields = topic.Split(':');
                this.Topic = Uri.UnescapeDataString(fields[0]);
                this.UserName = Uri.UnescapeDataString(fields[1]);
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
        /// Gets the topic information
        /// </summary>
        public TopicInfo Topic {
            get;
            private set;
        }

        internal ChangeTopicEventArgs(NameValueCollection packet)
            : base(packet) {
                this.Name = packet["channel"];
                this.Topic = new TopicInfo(packet["topic"]);
        }

    }
}
