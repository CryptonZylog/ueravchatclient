using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crypton.AvChat.Win
{
    static class WatchwordService
    {

        /// <summary>
        /// Checks to see if any matches exist in the text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool Match(string text)
        {
            if (Properties.Settings.Default.WatchwordsEnabled)
            {
                foreach (string word in Properties.Settings.Default.Watchwords)
                {
                    if (text.Contains(word))
                        return true;
                }
            }
            return false;
        }

    }
}
