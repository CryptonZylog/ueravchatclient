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

namespace Crypton.AvChat.Win.AppWindows
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {

        private System.Threading.Timer tmrAutoLoginButtonText = null;
        private System.Threading.Timer tmrAutoLoginTimeout = null;
        private int autoLoginTimerCount = 5;

        public LoginWindow()
        {
            InitializeComponent();

            chkAutoLogin.IsChecked = Properties.Settings.Default.AutoLogin;
            chkRememberLogin.IsChecked = Properties.Settings.Default.RememberLogin;
            if (Properties.Settings.Default.RememberLogin)
            {
                txtUsername.Text = Properties.Settings.Default.LoginUsername;
                txtPassword.Password = Properties.Settings.Default.LoginPassword;
            }
        }

        private void AutoLoginBegin()
        {
            this.tmrAutoLoginTimeout = new System.Threading.Timer(AutoLoginTimeout, null, autoLoginTimerCount * 1000, System.Threading.Timeout.Infinite);
            this.tmrAutoLoginButtonText = new System.Threading.Timer(AutoLoginUpdateText, null, 100, 1000);
        }

        private void AutoLoginUpdateText(object state)
        {
            this.Dispatcher.Invoke((Action)delegate
            {
                if (autoLoginTimerCount == 0)
                {
                    btnLogin.Content = "Login";
                    this.tmrAutoLoginButtonText.Dispose();
                }
                else
                {
                    btnLogin.Content = string.Format("Login ({0})", autoLoginTimerCount--);
                }
            });
        }

        private void AutoLoginCancel()
        {
            if (this.tmrAutoLoginTimeout != null)
                this.tmrAutoLoginTimeout.Dispose();
            if (this.tmrAutoLoginButtonText != null)
                this.tmrAutoLoginButtonText.Dispose();
            btnLogin.Content = "Login";
        }

        private void AutoLoginTimeout(object state)
        {
            this.Dispatcher.Invoke((Action)delegate
            {
                tmrAutoLoginTimeout.Dispose();
                btnLogin_Click(null, null);
            });
        }

        private void OnAnyTextChanged(object sender, TextChangedEventArgs e)
        {
            AutoLoginCancel();
        }

        private void OnAnyTextChanged(object sender, RoutedEventArgs e)
        {
            AutoLoginCancel();
        }

        public static LoginWindow Singleton
        {
            get;
            private set;
        }

        public bool LoginResult
        {
            get;
            private set;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            AutoLoginCancel();
            btnLogin.IsEnabled = false;
            string username = txtUsername.Text;
            string password = txtPassword.Password;
            txtUsername.IsEnabled = false;
            txtPassword.IsEnabled = false;

            ChatDispatcher.Singleton.BeginLogin(username, password, delegate(bool result, string errMsg)
            {
                txtUsername.IsEnabled = true;
                txtPassword.IsEnabled = true;
                btnLogin.IsEnabled = true;
                if (result)
                {
                    Properties.Settings.Default.AutoLogin = chkAutoLogin.IsChecked ?? false;
                    Properties.Settings.Default.RememberLogin = chkRememberLogin.IsChecked ?? false;
                    if (chkRememberLogin.IsChecked ?? false)
                    {
                        Properties.Settings.Default.LoginUsername = txtUsername.Text;
                        Properties.Settings.Default.LoginPassword = txtPassword.Password;
                    }
                    Properties.Settings.Default.Save();
                    this.LoginResult = true;
                    this.Close();
                }
                else
                {
                    ChatDispatcher.Singleton.LogToConsole("Login failed: " + errMsg);
                    MessageBox.Show(errMsg, App.ProductName, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            AutoLoginCancel();
            App.Current.Shutdown();
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            AutoLoginCancel();
            App.Current.OpenSettings();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            LoginWindow.Singleton = null;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoginWindow.Singleton = this;
            if (Properties.Settings.Default.AutoLogin && ChatDispatcher.Singleton.ConnectionStatus == Client.ConnectionStatusTypes.Disconnected)
            {
                this.AutoLoginBegin();
            }
        }

    }
}
