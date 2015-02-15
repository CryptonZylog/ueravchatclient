using System;
using System.Collections.Generic;

using System.Text;

namespace Crypton.AvChat.Client {
    /// <summary>
    /// Occurs when a specific chat comamnd has failed
    /// </summary>
    public class CommandException : Exception {
        /// <summary>
        /// Gets the original command text
        /// </summary>
        public string CommandText {
            get;
            private set;
        }

        public CommandException(string command, string message) : base(message) { this.CommandText = command; }
    }
}
