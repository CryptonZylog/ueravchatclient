// -----------------------------------------------------------------------
// <copyright file="PacketEventArgs.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Crypton.AvChat.Client.Events {
    using System;
    using System.Collections.Generic;
    
    using System.Text;
    using System.Collections.Specialized;

    /// <summary>
    /// The base class for a packet-originated event
    /// </summary>
    public abstract class PacketEventArgs : EventArgs {
        /// <summary>
        /// Gets the original packet that caused the event
        /// </summary>
        protected NameValueCollection Packet {
            get;
            private set;
        }

        internal PacketEventArgs(NameValueCollection packet) {
            if (packet == null) {
                throw new ArgumentNullException("packet");
            }
            this.Packet = packet;
        }
    }
}
