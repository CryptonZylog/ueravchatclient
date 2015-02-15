using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Crypton.AvChat.Win.ChatModel;

namespace Crypton.AvChat.Win.History
{
    public static class HistoryManager
    {

        public static string HistoryDirectory
        {
            get
            {
                // start with dir in my docs
                string uerDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UER");
                if (!Directory.Exists(uerDirectory))
                    Directory.CreateDirectory(uerDirectory);
                // history
                string historyDirectory = Path.Combine(uerDirectory, "History");
                if (!Directory.Exists(historyDirectory))
                    Directory.CreateDirectory(historyDirectory);

                return historyDirectory;
            }
        }


        public static IHistoryProvider Create<THistoryProvider>(ChatDocument documentSource, string name) where THistoryProvider : class, IHistoryProvider, new()
        {
            IHistoryProvider historyProvider = new THistoryProvider();

            historyProvider.Document = documentSource;
            historyProvider.Name = name;

            return historyProvider;
        }

    }
}
