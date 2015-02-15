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
    /// Interaction logic for NotificationSettings.xaml
    /// </summary>
    public partial class NotificationSettings : UserControl, ISettingsTab
    {
        public NotificationSettings()
        {
            InitializeComponent();
        }

        public string TabName
        {
            get { return "Notifications"; }
        }

        public void LoadSettings()
        {
            throw new NotImplementedException();
        }

        public void SaveSettings()
        {
            throw new NotImplementedException();
        }
    }
}
