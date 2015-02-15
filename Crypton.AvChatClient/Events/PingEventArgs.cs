using System;
using System.Collections.Generic;

using System.Text;
using System.Collections.Specialized;
using System.Web;
using System.Globalization;

namespace Crypton.AvChat.Client.Events {
    /// <summary>
    /// Contains information about users that are logged in to the server and their local times
    /// </summary>
    public class PingEventArgs : PacketEventArgs {
        /// <summary>
        /// Information about individual user in a time list
        /// </summary>
        public class TimeListUserBlock {
            /// <summary>
            /// Gets user's name
            /// </summary>
            public string Name {
                get;
                private set;
            }
            /// <summary>
            /// Gets user's local time (do not assume date information is the same)
            /// </summary>
            public DateTime? LocalTime {
                get;
                private set;
            }
            /// <summary>
            /// Gets user's idle time
            /// </summary>
            public TimeSpan? IdleTime {
                get;
                private set;
            }

            internal TimeListUserBlock(string block) {
                string[] fields = block.Split('#');

                this.Name = Uri.UnescapeDataString(fields[0]);

                if (fields.Length > 1) {
                    if (!string.IsNullOrEmpty(fields[1])) {
                        this.LocalTime = DateTime.Parse(fields[1], new CultureInfo("en-US"));
                    }
                    if (!string.IsNullOrEmpty(fields[2])) {
                        string[] ts = fields[2].Split(':');
                        this.IdleTime = new TimeSpan(int.Parse(ts[0]), int.Parse(ts[1]), int.Parse(ts[2]));
                    }
                }
            }
        }

        /// <summary>
        /// Information about users
        /// </summary>
        public IEnumerable<TimeListUserBlock> TimeList {
            get;
            private set;
        }

        /*
        SERVER: ping timelist="Bob#10:34 PM#00:07:18,Jim#11:34 PM#00:00:01"
        CLIENT: ping localtime="10:35 PM" idletime="00:00:34"

        When you respond with a ping packet, you should also include the current local time at your location, as well as the amount of hours/minutes/seconds the user has been idle.  This allows other users of the chat to more easily recognize when you are not at your chat client, and also to determine what time zone you are in.

        You may have noticed that the server's ping packet included a "timelist".  This is a list of all of the clients currently connected to the chat, as well as their local time, and their idle time.  The format is as follows:
        - User blocks are delineated with commas
        - Each user block consists of three fields, delineated with hash marks (#).
        - The blocks are Name, Local Time, Idle Time.

        Idle time should not just be the time since the last message was sent, but also whether the user is physically at the chat window, perhaps just lurking, or perhaps chatting in a private window.
        */

        internal PingEventArgs(NameValueCollection packet)
            : base(packet) {

            string[] timelist = packet["timelist"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            List<TimeListUserBlock> blocks = new List<TimeListUserBlock>();
            foreach (string timeuser in timelist) {
                blocks.Add(new TimeListUserBlock(timeuser));
            }
            this.TimeList = blocks.AsReadOnly();

        }

    }
}
