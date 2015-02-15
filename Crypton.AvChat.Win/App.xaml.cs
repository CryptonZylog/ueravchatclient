using Crypton.AvChat.Win.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace Crypton.AvChat.Win
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private Timer tmrIdleTimeWatcher = null;
        private bool isInForeground = true;

        public App()
        {
            this.Startup += App_Startup;
            this.Exit += App_Exit;
            this.Activated += App_Activated;
            this.Deactivated += App_Deactivated;
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
            this.ShutdownMode = System.Windows.ShutdownMode.OnLastWindowClose;
        }

        void App_Deactivated(object sender, EventArgs e)
        {
            // app is in background
            ChatDispatcher.Singleton.IdleTimer.ReportMode = Client.IdleResetModes.ResetCounter;
            ChatDispatcher.Singleton.IdleTimer.Reset();
            this.isInForeground = false;
        }

        void App_Activated(object sender, EventArgs e)
        {
            // app is in foreground
            ChatDispatcher.Singleton.IdleTimer.ReportMode = Client.IdleResetModes.ReportIdleTime;
            this.isInForeground = true;
        }

        public static new App Current
        {
            get
            {
                return (App)Application.Current;
            }
        }

        public Themes.Theme CurrentTheme
        {
            get;
            private set;
        }

        public static string ProductName
        {
            get
            {
                var attrib = (AssemblyProductAttribute)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), true).Cast<AssemblyProductAttribute>().FirstOrDefault();
                if (attrib != null)
                {
                    return attrib.Product;
                }
                return null;
            }
        }

        #region Start/Shutdown events
        void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {            
            e.Handled = true;
            AppWindows.ErrorReportingWindow errorWindow = new AppWindows.ErrorReportingWindow(e.Exception);
            errorWindow.Show();
        }

        void App_Startup(object sender, StartupEventArgs e)
        {
            // begin dispatcher
            ChatDispatcher.Singleton = new ChatDispatcher(this);

            // load theme
            this.LoadTheme();

            Trace.Listeners.Add(new ConsoleWindowTraceListener());

            // display login window initially
            this.OpenLogin();

            AppWindows.ChatWindow wnd = new AppWindows.ChatWindow();
            wnd.Show();
            wnd.RegisterTab(new Tabs.ConsoleTab());

            this.tmrIdleTimeWatcher = new Timer(updateIdleTime, null, 1000, 5000);
        }

        private void LoadTheme()
        {
            string themeName = Win.Properties.Settings.Default.ThemeName;
            if (!string.IsNullOrEmpty(themeName))
            {
                try
                {
                    this.CurrentTheme = Themes.ThemeProvider.Load(themeName);
                }
                catch (Exception any)
                {
                    Trace.TraceWarning("Theme load exception trying '" + themeName + "', using default: " + any.Message);
                    this.CurrentTheme = new Themes.Default.DefaultTheme();
                }
            }
            else
            {
                // use default
                this.CurrentTheme = new Themes.Default.DefaultTheme();
                Win.Properties.Settings.Default.ThemeName = "default";
            }
        }

        private void updateIdleTime(object state)
        {
            this.Dispatcher.Invoke((Action)delegate
            {
                if (this.isInForeground)
                {
                    PLASTINPUTINFO idleTime = new PLASTINPUTINFO();
                    idleTime.cbSize = (uint)Marshal.SizeOf(idleTime);
                    if (Win32Impl.GetLastInputInfo(out idleTime))
                    {
                        long diff = (uint)Environment.TickCount - idleTime.dwTime;
                        TimeSpan time = new TimeSpan(diff * 10000);
                        ChatDispatcher.Singleton.IdleTimer.Report(time);
                    }
                }
            });
        }

        void App_Exit(object sender, ExitEventArgs e)
        {
            ChatDispatcher.Singleton.Dispose();
        }
        #endregion

        #region Shared Windows Calling
        public void OpenSettings()
        {
            var wnd = AppWindows.SettingsWindow.Singleton;
            if (wnd != null)
            {
                wnd.Activate();
            }
            else
            {
                wnd = new AppWindows.SettingsWindow();
                wnd.Show();
            }
        }

        public void OpenLogin()
        {
            var wnd = AppWindows.LoginWindow.Singleton;
            if (wnd != null)
            {
                wnd.Activate();
            }
            else
            {
                wnd = new AppWindows.LoginWindow();
                wnd.Show();
            }
        }

        public void OpenChannelList(Crypton.AvChat.Client.Events.ChannelListEventArgs eventArgs)
        {
            var wnd = AppWindows.ChannelListWindow.Singleton;
            if (wnd != null)
            {
                wnd.Activate();
            }
            else
            {
                wnd = new AppWindows.ChannelListWindow();
                wnd.Show();
            }
            wnd.ProcessList(eventArgs);
        }
        #endregion

        public static IEnumerable<Crypton.AvChat.Win.AppWindows.ChatWindow> GetOpenChatWindows()
        {
            foreach (var wnd in App.Current.Windows)
            {
                if (wnd is AppWindows.ChatWindow)
                    yield return (AppWindows.ChatWindow)wnd;
            }
        }
    }
}
