using System;
using System.Collections.Generic;

using System.Text;

namespace Crypton.AvChat.Client {
    /// <summary>
    /// Chat Client Statistics
    /// </summary>
    public sealed class Statistics {

        /// <summary>
        /// Gets total packets that were sent
        /// </summary>
        public uint TotalPacketsSent {
            get;
            internal set;
        }
        /// <summary>
        /// Gets total packets that were received
        /// </summary>
        public uint TotalPacketsReceived {
            get;
            internal set;
        }
        /// <summary>
        /// Gets total number of bytes sent to the server
        /// </summary>
        public uint BytesSent {
            get;
            internal set;
        }
        /// <summary>
        /// Gets total number of bytes received from the server
        /// </summary>
        public uint BytesReceived {
            get;
            internal set;
        }
        /// <summary>
        /// Gets date and time last session was initiated by a Connect() statement
        /// </summary>
        public DateTime? SessionStart {
            get;
            internal set;
        }

        internal Statistics() { }
    }
}
