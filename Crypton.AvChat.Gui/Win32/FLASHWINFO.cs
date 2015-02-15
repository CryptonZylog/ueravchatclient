using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Crypton.AvChat.Gui.Win32 {
    /// <summary>
    /// Contains the flash status for a window and the number of times the system should flash the window.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct FLASHWINFO {
        /// <summary>
        /// The size of the structure, in bytes.
        /// </summary>
        public UInt32 cbSize;
        /// <summary>
        /// A handle to the window to be flashed. The window can be either opened or minimized.
        /// </summary>
        public IntPtr hwnd;
        /// <summary>
        /// The flash status
        /// </summary>
        public FLASHW_FLAGS dwFlags;
        /// <summary>
        /// The number of times to flash the window.
        /// </summary>
        public UInt32 uCount;
        /// <summary>
        /// The rate at which the window is to be flashed, in milliseconds. If dwTimeout is zero, the function uses the default cursor blink rate.
        /// </summary>
        public UInt32 dwTimeout;
    }

    /// <summary>
    /// The flash status. This parameter can be one or more of the following values. 
    /// </summary>
    [Flags]
    public enum FLASHW_FLAGS : uint {
        /// <summary>
        /// Flash both the window caption and taskbar button. This is equivalent to setting the FLASHW_CAPTION | FLASHW_TRAY flags.
        /// </summary>
        FLASHW_ALL = 0x00000003,
        /// <summary>
        /// Flash the window caption.
        /// </summary>
        FLASHW_CAPTION = 0x00000001,
        /// <summary>
        /// Stop flashing. The system restores the window to its original state.
        /// </summary>
        FLASHW_STOP = 0,
        /// <summary>
        /// Flash continuously, until the FLASHW_STOP flag is set.
        /// </summary>
        FLASHW_TIMER = 0x00000004,
        /// <summary>
        /// Flash continuously until the window comes to the foreground.
        /// </summary>
        FLASHW_TIMERNOFG = 0x0000000C,
        /// <summary>
        /// Flash the taskbar button.
        /// </summary>
        FLASHW_TRAY = 0x00000002
    }
}
