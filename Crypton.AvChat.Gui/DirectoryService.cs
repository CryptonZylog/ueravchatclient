using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Crypton.AvChat.Gui {
    /// <summary>
    /// Provides access to specialised directories for the application
    /// </summary>
    internal static class DirectoryService {
        /// <summary>
        /// Gets the UER folder location in MyDocuments
        /// </summary>
        public static string UERFolder {
            get {
                string dpath = Path.GetFullPath(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UER"));
                if (!Directory.Exists(dpath)) {
                    Directory.CreateDirectory(dpath);
                }
                return dpath;
            }
        }
        /// <summary>
        /// Gets the history folder
        /// </summary>
        public static string HistoryFolder {
            get {
                string dpath = Path.GetFullPath(Path.Combine(UERFolder, "History"));
                if (!Directory.Exists(dpath)) {
                    Directory.CreateDirectory(dpath);
                }
                return dpath;
            }
        }
    }
}
