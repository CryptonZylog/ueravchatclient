// -----------------------------------------------------------------------
// <copyright file="HistoryManager.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Crypton.AvChat.Gui.History {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal static class HistoryManager {

        [Obsolete]
        public static string UERFolder {
            get {
                return DirectoryService.UERFolder;
            }
        }

        [Obsolete]
        public static string HistoryFolder {
            get {
                return DirectoryService.HistoryFolder;
            }
        }

        public static string CurrentYearFolder {
            get {
                string dpath = Path.GetFullPath(Path.Combine(HistoryFolder, DateTime.Now.Year.ToString()));
                if (!Directory.Exists(dpath)) {
                    Directory.CreateDirectory(dpath);
                }
                return dpath;
            }
        }

        public static StreamWriter CreateChannelLog(string channelName, DateTime date) {
            string dpath = Path.GetFullPath(Path.Combine(CurrentYearFolder, channelName));
            if (!Directory.Exists(dpath)) {
                Directory.CreateDirectory(dpath);
            }

            string fpath = Path.GetFullPath(Path.Combine(dpath, date.ToString("yyyyMMdd_HHmmss") + ".html"));

            StreamWriter sw = new StreamWriter(fpath);
            sw.AutoFlush = true;
            return sw;
        }

    }
}
