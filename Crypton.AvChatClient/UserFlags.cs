using System;
using System.Collections.Generic;

using System.Text;

namespace Crypton.AvChat.Client {
    /// <summary>
    /// Different represented user genders
    /// </summary>
    [Flags]
    public enum UserGenders : int {
        /// <summary>
        /// Gender not set
        /// </summary>
        Unknown,
        /// <summary>
        /// User is a male
        /// </summary>
        Male,
        /// <summary>
        /// User is a female
        /// </summary>
        Female
    }

    /// <summary>
    /// User status flags (in channel)
    /// </summary>
    [Flags]
    public enum UserFlags : int {
        /// <summary>
        /// No flags set, or unknown
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// User is away
        /// </summary>
        Away = 1,
        /// <summary>
        /// User is an operator
        /// </summary>
        Op = 2,
        /// <summary>
        /// User has voice permissions
        /// </summary>
        Voice = 4
    }
}
