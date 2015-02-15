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
    /// Interaction logic for AwaySettings.xaml
    /// </summary>
    public partial class AwaySettings : UserControl, ISettingsTab
    {
        public AwaySettings()
        {
            InitializeComponent();
        }

        public string TabName
        {
            get { return "Auto Away"; }
        }

        public void LoadSettings()
        {
            this.CheckboxEnableAutoAway.IsChecked = Properties.Settings.Default.AwayEnable;
            this.upDownAwayTimeoutMinutes.Value = Properties.Settings.Default.AwayTimeout;
            this.textAwayMessage.Text = Properties.Settings.Default.AwayMessage;
        }

        public void SaveSettings()
        {
            Properties.Settings.Default.AwayEnable = this.CheckboxEnableAutoAway.IsChecked ?? false;
            Properties.Settings.Default.AwayTimeout = this.upDownAwayTimeoutMinutes.Value ?? Crypton.AvChat.Client.AutoAwayResponder.DefaultAwayThreshold.Minutes;
            Properties.Settings.Default.AwayMessage = this.textAwayMessage.Text.Trim();
            Properties.Settings.Default.Save();

            // apply to chat client
            ChatDispatcher.Singleton.AwayResponder.AwayMessage = Properties.Settings.Default.AwayMessage;
            ChatDispatcher.Singleton.AwayResponder.AwayThreshold = new TimeSpan(0, Properties.Settings.Default.AwayTimeout, 0);
            ChatDispatcher.Singleton.AwayResponder.Enable = Properties.Settings.Default.AwayEnable;
        }
    }
}
