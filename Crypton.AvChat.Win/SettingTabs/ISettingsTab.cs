using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crypton.AvChat.Win.SettingTabs
{
    interface ISettingsTab
    {

        string TabName
        {
            get;
        }
        void LoadSettings();
        void SaveSettings();

    }
}
