using System;
using System.Collections.Generic;

using System.Text;

namespace Crypton.AvChat.Client {

    /// <summary>
    /// List of possible problems that could happen with underlying connection
    /// </summary>
    [Flags]
    public enum ConnectionExceptionProblems {
        /// <summary>
        /// Other or unknown
        /// </summary>
        Other,
        /// <summary>
        /// Issues with network such as high latency, loss of connection
        /// </summary>
        NetworkIssue,
        /// <summary>
        /// Usually a bad username or password
        /// </summary>
        Authentication,
        /// <summary>
        /// Disconnect by an administrator
        /// </summary>
        Administrative,
        /// <summary>
        /// User is not connected to the server
        /// </summary>
        Disconnected
    }

    /// <summary>
    /// Occurs when there is a problem with connection
    /// </summary>
    public class ConnectionException : Exception {

        private string localMessage = null;
        /// <summary>
        /// Gets the exception message
        /// </summary>
        public override string Message {
            get {
                return localMessage ?? base.Message;
            }
        }

        /// <summary>
        /// Gets the underlying strongly-typed reason for exception
        /// </summary>
        public ConnectionExceptionProblems Reason {
            get;
            private set;
        }

        public ConnectionException() : base("An unknown error has occured in the connection") { this.Reason = ConnectionExceptionProblems.Other; }
        
        public ConnectionException(string message) : base(message) { this.Reason = ConnectionExceptionProblems.Other; }

        public ConnectionException(ConnectionExceptionProblems reason, string message) : base(message) { this.Reason = reason; }

        public ConnectionException(ConnectionExceptionProblems reason, string message, Exception inner) : base(message, inner) { this.Reason = reason; }

        public ConnectionException(ConnectionExceptionProblems reason)
            : base() {
                switch (reason) {
                    case ConnectionExceptionProblems.Disconnected:
                        this.localMessage = "The client is not connected to the server";
                        break;
                }
        }

    }
}
