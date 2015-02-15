using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Crypton.AvChat.Win.AppWindows
{
    /// <summary>
    /// Interaction logic for ErrorReportingWindow.xaml
    /// </summary>
    public partial class ErrorReportingWindow : Window
    {
        public ErrorReportingWindow(Exception exception)
        {
            InitializeComponent();
            this.exceptionDetails.Text = exception.ToString();

            string platform = Environment.OSVersion.Platform.ToString();
            string osName = "";
            switch (Environment.OSVersion.Version.Major)
            {
                case 5:
                    osName = "Windows XP";
                    break;
                case 6:
                    osName = "Windows 7";
                    break;
                case 7:
                    osName = "Windows 8";
                    break;
                default:
                    osName = Environment.OSVersion.VersionString;
                    break;
            }
            string osVersion = Environment.OSVersion.Version.ToString();

            string summary = string.Format("Exception: {0} - {1}", exception.GetType().Name, exception.Message);
            string description = exception.ToString();
            string productVersion = string.Format("{0}.{1}", Assembly.GetExecutingAssembly().GetName().Version.Major, Assembly.GetExecutingAssembly().GetName().Version.Minor);

            errorReportingLink.NavigateUri = new Uri(string.Format("http://bugs.crypton-technologies.net/bug_report_page.php?project_id=1&severity=70&category_id=4&platform={0}&os={1}&os_build={2}&summary={3}&description={4}&product_version={5}", platform, osName, osVersion, summary, description, productVersion));

        }

        private void exitAppButtonClick(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void openBugReportPage(object sender, RoutedEventArgs e)
        {
            
        }

        private void openBugReportPage(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Win32.Win32Impl.Open(e.Uri.ToString());
            e.Handled = true;
        }
    }
}
