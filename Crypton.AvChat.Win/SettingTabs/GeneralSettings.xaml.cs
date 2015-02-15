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
    /// Interaction logic for GeneralSettings.xaml
    /// </summary>
    public partial class GeneralSettings : UserControl, ISettingsTab
    {
        public GeneralSettings()
        {
            InitializeComponent();
        }

        public string TabName
        {
            get { return "General"; }
        }

        public void LoadSettings()
        {
            if (!string.IsNullOrEmpty(Properties.Settings.Default.UserColor))
            {
                Color color = (Color)ColorConverter.ConvertFromString(Properties.Settings.Default.UserColor);
                CanvasCurrentColor.Background = new SolidColorBrush(color);
            }
        }

        public void SaveSettings()
        {
            if (CanvasCurrentColor.Background is SolidColorBrush)
            {
                SolidColorBrush brush = (SolidColorBrush)CanvasCurrentColor.Background;
                Properties.Settings.Default.UserColor = string.Format("#{0:X2}{1:X2}{2:X2}", brush.Color.R, brush.Color.G, brush.Color.B);
                ChatDispatcher.Singleton.SetColor(Properties.Settings.Default.UserColor);
            }

            Properties.Settings.Default.Save();
        }

        private void PickUserColor(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.ColorDialog colorDialog = new System.Windows.Forms.ColorDialog();
            if (CanvasCurrentColor.Background is SolidColorBrush)
            {
                SolidColorBrush brush = (SolidColorBrush)CanvasCurrentColor.Background;
                colorDialog.Color = System.Drawing.Color.FromArgb(brush.Color.A, brush.Color.R, brush.Color.G, brush.Color.B);
            }
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Color wpfColor = Color.FromArgb(colorDialog.Color.A, colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B);
                CanvasCurrentColor.Background = new SolidColorBrush(wpfColor);
            }
        } 
    }
}
