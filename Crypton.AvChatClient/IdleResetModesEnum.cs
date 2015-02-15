// -----------------------------------------------------------------------
// <copyright file="IdleResetModesEnum.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Crypton.AvChat.Client {
    using System;
    using System.Collections.Generic;
    
    using System.Text;

    /// <summary>
    /// Determines how idle time will be reported to the server
    /// </summary>
    public enum IdleResetModes {
        /// <summary>
        /// The ChatClient will run a counter which will need to be reset by a call to ResetIdleTimer()
        /// </summary>
        ResetCounter,
        /// <summary>
        /// The implementing application code will call ReportIdleTime() with the TimeSpan information on how long the user has been idle
        /// </summary>
        ReportIdleTime
    }

}
