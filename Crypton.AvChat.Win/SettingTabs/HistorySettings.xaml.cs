using System;
using System.Collections.Generic;
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
    /// Interaction logic for HistorySettings.xaml
    /// </summary>
    public partial class HistorySettings : UserControl, ISettingsTab
    {
        public HistorySettings()
        {
            InitializeComponent();
        }

        public string TabName
        {
            get { return "History";  }
        }

        public void LoadSettings()
        {
            this.EnableHistoryCheckbox.IsChecked = Properties.Settings.Default.EnableHistory;
        }

        public void SaveSettings()
        {
            Properties.Settings.Default.EnableHistory = this.EnableHistoryCheckbox.IsChecked ?? true;
        }

        private void OpenHistoryFolder(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", History.HistoryManager.HistoryDirectory);
        }
    }
}
