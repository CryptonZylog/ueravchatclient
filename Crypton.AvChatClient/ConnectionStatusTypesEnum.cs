// -----------------------------------------------------------------------
// <copyright file="ConnectionStatusTypesEnum.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Crypton.AvChat.Client {
    using System;
    using System.Collections.Generic;
    
    using System.Text;

    /// <summary>
    /// Defines different states of the connection
    /// </summary>
    public enum ConnectionStatusTypes {
        /// <summary>
        /// The client is disconnected
        /// </summary>
        Disconnected,
        /// <summary>
        /// Socket has connected
        /// </summary>
        SocketConnected,
        /// <summary>
        /// The client is connected
        /// </summary>
        Connected,
        /// <summary>
        /// The client is connecting
        /// </summary>
        Connecting,
        /// <summary>
        /// The connection has been lost
        /// </summary>
        ConnectionLost,
        /// <summary>
        /// The client is trying to auto connect back to the server
        /// </summary>
        Reconnecting,
        /// <summary>
        /// Client is processing a login request
        /// </summary>
        Login
    }
}
