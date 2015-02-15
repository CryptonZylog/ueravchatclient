using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crypton.AvChat.Win.ChatModel;

namespace Crypton.AvChat.Win.History
{
    /// <summary>
    /// Defines a standard for a history provider
    /// </summary>
    public interface IHistoryProvider
    {

        /// <summary>
        /// Gets or sets the document source of text
        /// </summary>
        ChatDocument Document { get; set; }
        /// <summary>
        /// Gets the date and time of current history log
        /// </summary>
        DateTime Date { get; }
        /// <summary>
        /// Gets or sets the name of the history log, such as channel name, user name, or else
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Begins logging history
        /// </summary>
        void BeginHistoryLog();
        /// <summary>
        /// Ends logging history
        /// </summary>
        void EndHistoryLog();
        /// <summary>
        /// Causes immediate save of the current history
        /// </summary>
        void Flush();

    }
}
