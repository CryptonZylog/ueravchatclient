// -----------------------------------------------------------------------
// <copyright file="PlayDogEventArgs.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Crypton.AvChat.Client.Events {
    using System;
    using System.Collections.Generic;
    
    using System.Text;
    using System.Collections.Specialized;

    /// <summary>
    /// Contains information about a sound to play
    /// </summary>
    public class PlayDogEventArgs : PacketEventArgs {
        /// <summary>
        /// Index of the sound file
        /// </summary>
        public int Index {
            get;
            private set;
        }

        internal PlayDogEventArgs(NameValueCollection packet) : base(packet) {
            this.Index = int.Parse(packet["dognum"]);
        }
    }
}
