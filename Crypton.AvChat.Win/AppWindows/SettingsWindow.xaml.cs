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
using System.Windows.Shapes;
using Crypton.AvChat.Win.SettingTabs;

namespace Crypton.AvChat.Win.AppWindows
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            this.LoadSettingTabs();
        }

        private void LoadSettingTabs()
        {
            List<ISettingsTab> tabs = new List<ISettingsTab>();

            // add tabs here
            tabs.Add(new GeneralSettings());
            tabs.Add(new AutoJoinSettings());
            tabs.Add(new AwaySettings());
            tabs.Add(new HistorySettings());
            tabs.Add(new NotificationSettings());
            tabs.Add(new OpWhitelistSettings());
            tabs.Add(new WatchwordSettings());

            foreach (var tab in tabs)
            {
                TabItem item = new TabItem();
                item.Content = tab;
                item.Header = tab.TabName;
                item.Tag = tab;
                SettingsTabs.Items.Add(item);

                try
                {
                    tab.LoadSettings();
                }
                catch { }
            }
        }

        public static SettingsWindow Singleton
        {
            get;
            private set;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            SettingsWindow.Singleton = null;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SettingsWindow.Singleton = this;
        }

        private void CancelWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveAllSettings(object sender, RoutedEventArgs e)
        {
            foreach (TabItem tab in SettingsTabs.Items)
            {
                ISettingsTab settingsTab = (ISettingsTab)tab.Tag;
                try
                {
                    settingsTab.SaveSettings();
                }
                catch { }
            }
            this.Close();
        }
        
    }
}
