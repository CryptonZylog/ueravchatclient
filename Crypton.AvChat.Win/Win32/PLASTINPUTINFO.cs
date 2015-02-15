using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Crypton.AvChat.Win.Win32
{
    /// <summary>
    /// Contains the time of the last input.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PLASTINPUTINFO {
        /// <summary>
        /// The size of the structure, in bytes. This member must be set to sizeof(LASTINPUTINFO). 
        /// </summary>
        public UInt32 cbSize;
        /// <summary>
        /// The tick count when the last input event was received. 
        /// </summary>
        public UInt32 dwTime;
    }
}
