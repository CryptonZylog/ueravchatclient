using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Crypton.AvChat.Win.SettingTabs
{
    /// <summary>
    /// Interaction logic for WatchwordSettings.xaml
    /// </summary>
    public partial class WatchwordSettings : UserControl, ISettingsTab
    {
        public WatchwordSettings()
        {
            InitializeComponent();
        }

        public string TabName
        {
            get { return "Watchwords"; }
        }

        public void LoadSettings()
        {
            EnableWatchwordCheckbox.IsChecked = Properties.Settings.Default.WatchwordsEnabled;
            if (Properties.Settings.Default.Watchwords != null)
            {
                using (StringWriter sw = new StringWriter())
                {
                    foreach (string word in Properties.Settings.Default.Watchwords)
                        sw.WriteLine(word);
                    WatchwordListBox.Text = sw.ToString();
                }
            }
        }

        public void SaveSettings()
        {
            Properties.Settings.Default.Watchwords = new System.Collections.Specialized.StringCollection();
            Properties.Settings.Default.WatchwordsEnabled = EnableWatchwordCheckbox.IsChecked ?? false;
            using (StringReader sr = new StringReader(WatchwordListBox.Text))
            {
                string line = null;
                while ((line = sr.ReadLine()) != null)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        Properties.Settings.Default.Watchwords.Add(line);
                    }
                }
            }
        }
    }
}
