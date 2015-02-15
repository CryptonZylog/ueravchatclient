
//#define ALPHA // Alpha version
#define BETA

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading;
using System.Windows.Forms;
using Crypton.AvChat.Client;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;


namespace Crypton.AvChat.Gui {

    [Serializable]
    internal struct SavedUsernamePassword {
        public string Username;
        public string Password;

        public SavedUsernamePassword(string username, string password) {
            this.Username = username;
            this.Password = password;
        }
    }

    static class Program {

        /// <summary>
        /// Gets the chat client (global)
        /// </summary>
        internal static ChatClient GlobalClient {
            get;
            private set;
        }

        public static string Username = string.Empty;
        public static string Password = string.Empty;

        public static DateTime BuildDate {
            get {
                DateTime result = new DateTime(2000, 1, 1);
                Version vers = Assembly.GetExecutingAssembly().GetName().Version;
                result = result.AddDays(vers.Build);
                result = result.AddSeconds(vers.Revision * 2);
                if (TimeZone.IsDaylightSavingTime(DateTime.Now, TimeZone.CurrentTimeZone.GetDaylightChanges(DateTime.Now.Year))) {
                    result = result.AddHours(1);
                }
                return result;
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            // reset current directory
            Environment.CurrentDirectory = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory);

            setupDebugging();
            
            // required for winforms
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!Debugger.IsAttached) {
                Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
                Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
            }

            Program.GlobalClient = new ChatClient();
            ChatClient.ClientVersion = Assembly.GetExecutingAssembly().GetName().Version;
            Program.GlobalClient.IdleTimer.ReportMode = IdleResetModes.ReportIdleTime;
            
            if (File.Exists(Path.Combine(Environment.CurrentDirectory, "changelog.txt"))) {
                using (ChangeLogView changelog = new ChangeLogView()) {
                    changelog.ShowDialog();
                    File.Delete(Path.Combine(Environment.CurrentDirectory, "changelog.txt"));
                }
            }

            // login to chat
            bool appStart = false;

            using (LoginForm frmLogin = new LoginForm()) {
                if (frmLogin.ShowDialog() == DialogResult.OK) {
                    // login success
                    appStart = true;
                    Trace.TraceInformation("Login success");
                }
            }

            if (appStart) {
                // calling constructor will bind the form and its events to the chat
                MainForm frmMain = new MainForm();
                ApplicationContext appContext = new ApplicationContext(frmMain);
                Application.Run(appContext);
            }

            // nothing else to do
            Environment.Exit(0);
        }

        private static void setupDebugging()
        {
            var listener = new DelimitedListTraceListener(Path.Combine(Environment.CurrentDirectory, "trace.csv"));
            listener.TraceOutputOptions = TraceOptions.DateTime;
            Trace.Listeners.Add(listener);
        }
      
        static void Application_ApplicationExit(object sender, EventArgs e) {
#if(TRACE)
            Trace.TraceInformation("Triggered ApplicationExit");
#endif
            Properties.Settings.Default.Save();
            Program.GlobalClient.Dispose();
            Cache.CacheManager.Clear();
        }

        #region Error Handling

        private static void handleError(string message) {
            ConsoleColor curColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[Error] {0}", message);
            Console.ForegroundColor = curColor;
        }

        private static void handleErrorCritical(string message) {
            ConsoleColor curColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[Critical] {0}", message);
            Console.ForegroundColor = curColor;
            Console.Write("Press any key to exit...");
            Console.ReadKey();
            Environment.Exit(1);
        }

        private static void ExceptionWindow(Exception src) {
            handleError("Application Exception: " + src.Message);
            using (ErrorHandler hdl = new ErrorHandler(src)) {
                hdl.ShowDialog();
                Environment.Exit(2);
            }
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
            ExceptionWindow(e.ExceptionObject as Exception);
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e) {
            ExceptionWindow(e.Exception);
        }
        #endregion

        #region User Credentials Storage
        private static byte[] getEncryptionKey() {
            // key based on current assembly version and some random bits
            SHA256 sha256 = null;
            try {
                sha256 = new SHA256Cng();
            }
            catch {
                try {
                    sha256 = new SHA256CryptoServiceProvider();
                }
                catch {
                    sha256 = new SHA256Managed();
                }
            }

            byte[] saltA = BitConverter.GetBytes(new DateTime(1991, 2, 25).ToBinary());
            byte[] saltB = BitConverter.GetBytes(new DateTime(2010, 9, 14).ToBinary());

            var both = saltA.ToList();
            both.AddRange(saltB);

            byte[] saltFinal = both.ToArray();
            byte[] encd = Encoding.Unicode.GetBytes(/*Assembly.GetExecutingAssembly().GetName().Version + */ Environment.MachineName + Environment.UserName);

            byte[] saltHashed = sha256.ComputeHash(saltFinal);
            byte[] encHashed = sha256.ComputeHash(encd);

            PasswordDeriveBytes pdb = new PasswordDeriveBytes(encHashed, saltHashed);

            sha256.Dispose();

            return pdb.GetBytes(256 / 8);
        }

        public static SavedUsernamePassword LoadSavedCredentials() {
            byte[] credentialsRaw = new byte[0];

            try {
                credentialsRaw = Convert.FromBase64String(Properties.Settings.Default.userSavedCredentials);
            }
            catch { return new SavedUsernamePassword(); }

            // attempt to decrypt
            Aes aes = null;
            try {
                aes = new AesCryptoServiceProvider();
            }
            catch {
                aes = new AesManaged();
            }

            aes.Mode = CipherMode.ECB;
            aes.Key = getEncryptionKey();

            using (ICryptoTransform ct = aes.CreateDecryptor()) {
                try {
                    using (CryptoStream cs = new CryptoStream(new MemoryStream(credentialsRaw), ct, CryptoStreamMode.Read)) {
                        BinaryFormatter bf = new BinaryFormatter();
                        SavedUsernamePassword unpw = (SavedUsernamePassword)bf.Deserialize(cs);
                        aes.Dispose();
                        return unpw;
                    }
                }
                catch {
                    aes.Dispose();
                    return new SavedUsernamePassword();
                }
            }
        }

        public static void SaveCredentials(SavedUsernamePassword unpw) {

            MemoryStream ms = new MemoryStream();

            Aes aes = null;
            try {
                aes = new AesCryptoServiceProvider();
            }
            catch {
                aes = new AesManaged();
            }

            aes.Mode = CipherMode.ECB;
            aes.Key = getEncryptionKey();

            using (ICryptoTransform ct = aes.CreateEncryptor()) {
                using (CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Write)) {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(cs, unpw);
                    cs.Flush();
                    cs.FlushFinalBlock();
                }
            }

            aes.Dispose();

            byte[] creds = ms.ToArray();
            Properties.Settings.Default.userSavedCredentials = Convert.ToBase64String(creds);

            ms.Dispose();
        }
        #endregion
    }
}
