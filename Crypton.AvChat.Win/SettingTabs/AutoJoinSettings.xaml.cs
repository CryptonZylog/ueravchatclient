using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
    /// Interaction logic for AutoJoinSettings.xaml
    /// </summary>
    public partial class AutoJoinSettings : UserControl, ISettingsTab
    {
        public AutoJoinSettings()
        {
            InitializeComponent();
        }

        public string TabName
        {
            get { return "Auto Join"; }
        }

        public void LoadSettings()
        {
            if (Properties.Settings.Default.AutoJoinList != null)
            {
                StringBuilder list = new StringBuilder();
                foreach (string name in Properties.Settings.Default.AutoJoinList)
                {
                    list.AppendLine(name);
                }
                txtAutoJoinList.Text = list.ToString();
            }
        }

        public void SaveSettings()
        {
            var list = new StringCollection();
            using (StringReader sr = new StringReader(txtAutoJoinList.Text.Trim()))
            {
                string line = null;
                while ((line = sr.ReadLine()) != null)
                {
                    if (!string.IsNullOrEmpty(line))
                        list.Add(line);
                }
            }

            Properties.Settings.Default.AutoJoinList = list;
            Properties.Settings.Default.Save();
        }
    }
}
