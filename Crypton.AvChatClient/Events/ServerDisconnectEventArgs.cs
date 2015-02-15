// -----------------------------------------------------------------------
// <copyright file="ServerDisconnectEventArgs.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Crypton.AvChat.Client.Events {
    using System;
    using System.Collections.Generic;
    
    using System.Text;
using System.Collections.Specialized;

    /// <summary>
    /// Information when you have been disconnected from the server
    /// </summary>
    public class ServerDisconnectEventArgs : PacketEventArgs {
        /// <summary>
        /// Additional text (reason)
        /// </summary>
        public string Text {
            get;
            private set;
        }
        
        internal ServerDisconnectEventArgs(NameValueCollection packet) : base(packet) {
            this.Text = packet["text"];
        }
    }
}
