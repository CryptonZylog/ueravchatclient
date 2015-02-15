// -----------------------------------------------------------------------
// <copyright file="ColorChangeEventArgs.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Crypton.AvChat.Client.Events {
    using System;
    using System.Collections.Generic;
    
    using System.Text;
using System.Collections.Specialized;

    /// <summary>
    /// Contains information about the user color
    /// </summary>
    public class ColorChangeEventArgs : PacketEventArgs {

        /// <summary>
        /// Gets the HTML Color
        /// </summary>
        public string HtmlColor {
            get;
            private set;
        }

        internal ColorChangeEventArgs(NameValueCollection packet)
            : base(packet) {
                this.HtmlColor = "#" + packet["newcol"];
        }
    }
}
