// -----------------------------------------------------------------------
// <copyright file="ChannelTopicEventArgs.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Crypton.AvChat.Client.Events {
    using System;
    using System.Collections.Generic;
    
    using System.Text;
    using System.Collections.Specialized;
    using System.Web;
    using System.Globalization;
    using System.Diagnostics;

    /// <summary>
    /// Contains information about a channel topic
    /// </summary>
    public class ChannelTopicEventArgs : PacketEventArgs {

        /// <summary>
        /// Gets the name of the channel
        /// </summary>
        public string Name {
            get;
            private set;
        }

        /// <summary>
        /// Gets the topic text
        /// </summary>
        public string Topic {
            get;
            private set;
        }

        /// <summary>
        /// Gets the date/time topic was set
        /// </summary>
        public DateTime? Date {
            get;
            private set;
        }

        /// <summary>
        /// Gets the name of the user who set the topic
        /// </summary>
        public string SetBy {
            get;
            private set;
        }

        internal ChannelTopicEventArgs(NameValueCollection packet)
            : base(packet) {
            //topic channel="#general" topic="Escaped%20Topic,Topic%20Set%20Date,Topic%20Set%20By"

            this.Name = packet["channel"];
            string topic = packet["topic"];

            string[] topicFields = topic.Split(',');

            this.Topic = Uri.UnescapeDataString(topicFields[0]);
            if (!string.IsNullOrEmpty(topicFields[1])) {
                DateTime dtSet = DateTime.MinValue;
                if (!DateTime.TryParse(Uri.UnescapeDataString(topicFields[1]), new CultureInfo("en-US"), DateTimeStyles.None, out dtSet))
                {
                    //Trace.TraceWarning("Cannot DateTime.TryParse in ChannelTopicEventArgs for topic set date: topicFields[1] is " + Uri.UnescapeDataString(topicFields[1]));
                }
                this.Date = dtSet;
            }
            this.SetBy = Uri.UnescapeDataString(topicFields[2]);

        }

    }
}
