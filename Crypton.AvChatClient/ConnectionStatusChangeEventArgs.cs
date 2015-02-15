// -----------------------------------------------------------------------
// <copyright file="ConnectionStatusChangeEventArgs.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Crypton.AvChat.Client {
    using System;
    using System.Collections.Generic;
    
    using System.Text;

    /// <summary>
    /// Contains information about the changed state of the connection
    /// </summary>
    public class ConnectionStatusChangeEventArgs : EventArgs {
        /// <summary>
        /// Contains information about the new status
        /// </summary>
        public ConnectionStatusTypes Status {
            get;
            private set;
        }
        /// <summary>
        /// Gets the client reference
        /// </summary>
        public ChatClient Client {
            get;
            private set;
        }

        private ConnectionStatusChangeEventArgs() { }
        internal ConnectionStatusChangeEventArgs(ConnectionStatusTypes newStatus, ChatClient client) {
            this.Status = newStatus;
            this.Client = client;
        }
    }
}
