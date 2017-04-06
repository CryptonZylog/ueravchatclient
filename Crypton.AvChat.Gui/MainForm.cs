using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Crypton.AvChat.Client;
using Crypton.AvChat.Gui.Media;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Reflection;
using Crypton.AvChat.Gui.Win32;
using Crypton.AvChat.Client.Events;
using Crypton.AvChat.Gui.Tabs;
using Crypton.AvChat.Gui.Properties;
using System.Deployment.Application;

namespace Crypton.AvChat.Gui
{
    internal partial class MainForm : Form
    {

        public bool activeFlashState = false;

        public MainForm()
        {
            InitializeComponent();
            this.Font = SystemFonts.DialogFont;

            // load media icons
            tsConnectDisconnect.Image = DefaultResources.connect;
            tsConnectDisconnect.Text = "Connecting";

            // bind client events
            Program.GlobalClient.ConnectionStatusChanged += new EventHandler<ConnectionStatusChangeEventArgs>(GlobalClient_ConnectionStatusChanged);

            Program.GlobalClient.Events.AddMessageEvent += new EventHandler<AvChat.Client.Events.AddMessageEventArgs>(GlobalClient_AddMessageEvent);
            Program.GlobalClient.Events.AddTextEvent += new EventHandler<AvChat.Client.Events.AddTextEventArgs>(GlobalClient_AddTextEvent);
            Program.GlobalClient.Events.AddStatusEvent += new EventHandler<AddStatusEventArgs>(GlobalClient_AddStatusEvent);
            Program.GlobalClient.Events.AlertReceived += new EventHandler<AvChat.Client.Events.AlertEventArgs>(GlobalClient_AlertReceived);
            Program.GlobalClient.Events.ChannelFlagsReceived += new EventHandler<AvChat.Client.Events.ChannelFlagsEventArgs>(GlobalClient_ChannelFlagsReceived);
            Program.GlobalClient.Events.ChannelJoined += new EventHandler<AvChat.Client.Events.ChannelJoinEventArgs>(GlobalClient_ChannelJoined);
            Program.GlobalClient.Events.ChannelKick += new EventHandler<AvChat.Client.Events.KickChannelEventArgs>(GlobalClient_ChannelKick);
            Program.GlobalClient.Events.ChannelTopicChanged += new EventHandler<AvChat.Client.Events.ChangeTopicEventArgs>(GlobalClient_ChannelTopicChanged);
            Program.GlobalClient.Events.ChannelTopicReceived += new EventHandler<AvChat.Client.Events.ChannelTopicEventArgs>(GlobalClient_ChannelTopicReceived);
            Program.GlobalClient.Events.ChannelUserFlagsChanged += new EventHandler<AvChat.Client.Events.FlagStatusEventArgs>(GlobalClient_ChannelUserFlagsChanged);
            Program.GlobalClient.Events.ChannelUserJoin += new EventHandler<AvChat.Client.Events.UserJoinEventArgs>(GlobalClient_ChannelUserJoin);
            Program.GlobalClient.Events.ExitChannel += new EventHandler<AvChat.Client.Events.ExitChannelEventArgs>(GlobalClient_ExitChannel);
            Program.GlobalClient.Events.PingEvent += new EventHandler<AvChat.Client.Events.PingEventArgs>(GlobalClient_PingEvent);
            Program.GlobalClient.Events.PlayDogSound += new EventHandler<AvChat.Client.Events.PlayDogEventArgs>(GlobalClient_PlayDogSound);
            Program.GlobalClient.Events.QuitEvent += new EventHandler(GlobalClient_QuitEvent);
            Program.GlobalClient.Events.ServerDisconnect += new EventHandler<AvChat.Client.Events.ServerDisconnectEventArgs>(GlobalClient_ServerDisconnect);
            Program.GlobalClient.Events.UserColorChanged += new EventHandler<AvChat.Client.Events.ColorChangeEventArgs>(GlobalClient_UserColorChanged);
            Program.GlobalClient.Events.UserLeave += new EventHandler<AvChat.Client.Events.UserLeaveEventArgs>(GlobalClient_UserLeave);
            Program.GlobalClient.Events.UserListReceived += new EventHandler<AvChat.Client.Events.UserListEventArgs>(GlobalClient_UserListReceived);
            Program.GlobalClient.Events.UserQuit += new EventHandler<AvChat.Client.Events.UserQuitEventArgs>(GlobalClient_UserQuit);
            Program.GlobalClient.Events.UserKicked += new EventHandler<UserKickEventArgs>(GlobalClient_UserKicked);

            var userColor = Properties.Settings.Default.userTextColor;
            Program.GlobalClient.SetColor(userColor.R, userColor.G, userColor.B);
            Program.GlobalClient.AutoAway.Enable = Properties.Settings.Default.enableAutoAway;

            // connected
            tsConnectDisconnect.Text = "Connected";
            tsConnectDisconnect.Enabled = true;

            lblConnectionStatus.Text = "Connected";
            tsConnectDisconnect.Image = DefaultResources.connect;

            if (Settings.Default.mainWindowWidth >= this.MinimumSize.Width)
            {
                this.Width = Settings.Default.mainWindowWidth;
            }
            if (Settings.Default.mainWindowHeight >= this.MinimumSize.Height)
            {
                this.Height = Settings.Default.mainWindowHeight;
            }

            // restore start position
            if (Settings.Default.mainWindowX > 0 || Settings.Default.mainWindowY > 0)
            {
                // verify it fits
                this.StartPosition = FormStartPosition.Manual;
                if (Settings.Default.mainWindowY < Screen.PrimaryScreen.WorkingArea.Height - this.Height && Settings.Default.mainWindowX < Screen.PrimaryScreen.WorkingArea.Width - this.Width)
                {
                    this.DesktopLocation = new Point(Settings.Default.mainWindowX, Settings.Default.mainWindowY);
                }
            }

            if (Settings.Default.isMainWindowMaximized)
            {
                this.WindowState = FormWindowState.Maximized;
            }

            this.tmrIdleTimeReporter.Start();
            this.Text = string.Format("{0} - Crypton AvChat Client {1} (built {2})", Program.GlobalClient.Name, Assembly.GetExecutingAssembly().GetName().Version, Program.BuildDate);

            checkForUpdatesToolStripMenuItem.Enabled = false;
            newVersionIsAvailableToolStripMenuItem.Enabled = false;


        }

        public bool IsAway
        {
            get;
            set;
        }

        public IEnumerable<ChannelTab> ChannelTabs
        {
            get
            {
                foreach (TabPage page in this.tbRooms.TabPages)
                {
                    if (page.Tag is ChannelTab)
                    {
                        yield return (ChannelTab)page.Tag;
                    }
                }
            }
        }

        public IEnumerable<PrivateTab> PrivateTabs
        {
            get
            {
                foreach (TabPage page in this.tbRooms.TabPages)
                {
                    if (page.Tag is PrivateTab)
                    {
                        yield return (PrivateTab)page.Tag;
                    }
                }
            }
        }

        #region Get Channel Tab reference by name
        /// <summary>
        /// Gets the channel tab reference by channel name. This method is thread-safe
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ChannelTab getChannelTabByName(string name)
        {
            if (this.InvokeRequired)
            {
                Func<string, ChannelTab> dg = new Func<string, ChannelTab>(getChannelTabByNameImpl);
                return (ChannelTab)this.Invoke(dg, name);
            }
            else
            {
                return this.getChannelTabByNameImpl(name);
            }
        }

        private ChannelTab getChannelTabByNameImpl(string name)
        {
            foreach (var tab in this.ChannelTabs)
            {
                if (tab.ChannelName == name.ToLowerInvariant())
                {
                    return tab;
                }
            }
            return null;
        }
        /// <summary>
        /// Gets the private message tab associated to the session. If not found, returns null
        /// </summary>
        /// <param name="nameOfUser"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public PrivateTab getPrivateTabByName(string userName)
        {
            if (this.InvokeRequired)
            {
                Func<string, PrivateTab> dg = new Func<string, PrivateTab>(getPrivateTabByNameImpl);
                return (PrivateTab)this.Invoke(dg, userName);
            }
            else
            {
                return this.getPrivateTabByNameImpl(userName);
            }
        }
        private PrivateTab getPrivateTabByNameImpl(string userName)
        {
            foreach (var tab in this.PrivateTabs)
            {
                if (tab.ChannelName == userName)
                {
                    return tab;
                }
            }
            return null;
        }
        #endregion

        #region ~~ Events ~~

        #region Global/Channel User Quit
        void GlobalClient_UserQuit(object sender, AvChat.Client.Events.UserQuitEventArgs e)
        {
            if (this.InvokeRequired)
            {
                Action<UserQuitEventArgs> dg = new Action<UserQuitEventArgs>(GlobalClient_UserQuitImpl);
                this.Invoke(dg, e);
            }
            else
            {
                this.GlobalClient_UserQuitImpl(e);
            }
        }
        private void GlobalClient_UserQuitImpl(UserQuitEventArgs e)
        {
            string username = e.UserInfo.Name;
            if (e.Name.StartsWith("#"))
            {
                var tab = this.getChannelTabByName(e.Name);
                if (tab != null)
                {
                    tab.OnUserQuit(e);
                }
            }
            else
            {
                var tab = this.getPrivateTabByName(username);
                if (tab != null)
                {
                    tab.OnUserQuit(e);
                }
            }
        }
        #endregion

        #region Channel - User List Received
        void GlobalClient_UserListReceived(object sender, AvChat.Client.Events.UserListEventArgs e)
        {
            if (this.InvokeRequired)
            {
                Action<UserListEventArgs> dg = new Action<UserListEventArgs>(GlobalClient_UserListReceivedImpl);
                this.Invoke(dg, e);
            }
            else
            {
                GlobalClient_UserListReceivedImpl(e);
            }
        }
        private void GlobalClient_UserListReceivedImpl(UserListEventArgs e)
        {
            // update away stat just in case 
            var cu = e.Users.FirstOrDefault(u => u.Name == Program.GlobalClient.Name);
            if (cu != null)
            {
                this.IsAway = (cu.Flags & UserFlags.Away) == UserFlags.Away;
            }
            var tab = this.getChannelTabByName(e.Name);
            if (tab != null)
            {
                tab.OnUserListReceived(e);
            }
        }
        #endregion

        #region Channel - User Leave
        void GlobalClient_UserLeave(object sender, AvChat.Client.Events.UserLeaveEventArgs e)
        {
            if (this.InvokeRequired)
            {
                Action<UserLeaveEventArgs> dg = new Action<UserLeaveEventArgs>(GlobalClient_UserLeaveImpl);
                this.Invoke(dg, e);
            }
            else
            {
                this.GlobalClient_UserLeaveImpl(e);
            }
        }
        private void GlobalClient_UserLeaveImpl(UserLeaveEventArgs e)
        {
            var tab = this.getChannelTabByName(e.Name);
            if (tab != null)
            {
                tab.OnUserLeave(e);
            }
        }
        #endregion

        #region Channel - User Kicked
        void GlobalClient_UserKicked(object sender, UserKickEventArgs e)
        {
            if (this.InvokeRequired)
            {
                Action<UserKickEventArgs> dg = new Action<UserKickEventArgs>(GlobalClient_UserKickedImpl);
                this.Invoke(dg, e);
            }
            else
            {
                this.GlobalClient_UserKickedImpl(e);
            }
        }
        private void GlobalClient_UserKickedImpl(UserKickEventArgs e)
        {
            var tab = this.getChannelTabByName(e.ChannelName);
            if (tab != null)
            {
                tab.OnUserKick(e);
            }
        }
        #endregion

        #region Global - User Color Changed
        void GlobalClient_UserColorChanged(object sender, AvChat.Client.Events.ColorChangeEventArgs e)
        {
            Settings.Default.userTextColor = ColorTranslator.FromHtml(e.HtmlColor);
            Settings.Default.Save();
        }
        #endregion

        #region Server disconnect
        void GlobalClient_ServerDisconnect(object sender, AvChat.Client.Events.ServerDisconnectEventArgs e)
        {
            if (this.InvokeRequired)
            {
                Action<ServerDisconnectEventArgs> dg = new Action<ServerDisconnectEventArgs>(GlobalClient_ServerDisconnectImpl);
                this.Invoke(dg, e);
            }
            else
            {
                GlobalClient_ServerDisconnectImpl(e);
            }
        }
        private void GlobalClient_ServerDisconnectImpl(ServerDisconnectEventArgs e)
        {
            foreach (var tab in this.ChannelTabs)
            {
                tab.OnServerDisconnect(e);
            }
        }
        #endregion

        #region Global - Quit
        void GlobalClient_QuitEvent(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                Action dg = new Action(GlobalClient_QuitEventImpl);
                this.Invoke(dg);
            }
            else
            {
                GlobalClient_QuitEventImpl();
            }
        }
        private void GlobalClient_QuitEventImpl()
        {
            var channels = this.ChannelTabs.ToArray();
            var privatetabs = this.PrivateTabs.ToArray();
            foreach (var tab in channels)
            {
                tbRooms.TabPages.Remove(tab.ParentTab);
            }
            foreach (var tab in privatetabs)
            {
                tbRooms.TabPages.Remove(tab.ParentTab);
            }
        }
        #endregion

        #region Global - Play Dog Sound
        void GlobalClient_PlayDogSound(object sender, AvChat.Client.Events.PlayDogEventArgs e)
        {
            string fpath = Media.MediaManager.Sounds.GetDogSound(e.Index);
            if (fpath != null && Notifications.Default.EnableDogSounds)
            {
                using (System.Media.SoundPlayer player = new System.Media.SoundPlayer(fpath))
                {
                    player.PlaySync();
                }
            }
        }
        #endregion

        #region Global - Ping Event
        void GlobalClient_PingEvent(object sender, AvChat.Client.Events.PingEventArgs e)
        {
            if (this.InvokeRequired)
            {
                Action<PingEventArgs> dg = new Action<PingEventArgs>(GlobalClient_PingEventImpl);
                this.Invoke(dg, e);
            }
            else
            {
                GlobalClient_PingEventImpl(e);
            }
        }
        private void GlobalClient_PingEventImpl(PingEventArgs e)
        {
            foreach (var tab in this.ChannelTabs)
            {
                foreach (var tluser in e.TimeList)
                {
                    var chanuser = tab.ChannelUsers.FirstOrDefault(u => u.UserName == tluser.Name);
                    if (chanuser != null)
                    {
                        chanuser.LocalTime = tluser.LocalTime ?? chanuser.LocalTime;
                        chanuser.IdleTime = tluser.IdleTime ?? chanuser.IdleTime;
                    }
                }
            }
        }
        #endregion

        #region Channel - Exit Channel
        void GlobalClient_ExitChannel(object sender, AvChat.Client.Events.ExitChannelEventArgs e)
        {
            if (this.InvokeRequired)
            {
                Action<ExitChannelEventArgs> dg = new Action<ExitChannelEventArgs>(GlobalClient_ExitChannelImpl);
                this.Invoke(dg, e);
            }
            else
            {
                GlobalClient_ExitChannelImpl(e);
            }
        }
        private void GlobalClient_ExitChannelImpl(ExitChannelEventArgs e)
        {
            var tab = this.getChannelTabByName(e.Name);
            if (tab != null)
            {
                tab.OnExitChannel(e);
            }
        }
        #endregion

        #region Channel - User Join
        void GlobalClient_ChannelUserJoin(object sender, AvChat.Client.Events.UserJoinEventArgs e)
        {
            if (this.InvokeRequired)
            {
                Action<UserJoinEventArgs> dg = new Action<UserJoinEventArgs>(GlobalClient_ChannelUserJoinImpl);
                this.Invoke(dg, e);
            }
            else
            {
                GlobalClient_ChannelUserJoinImpl(e);
            }
        }
        private void GlobalClient_ChannelUserJoinImpl(UserJoinEventArgs e)
        {
            var tab = this.getChannelTabByName(e.Name);
            if (tab != null)
            {
                tab.OnUserJoin(e);
            }
        }
        #endregion

        #region Channel - User Flags Updated
        void GlobalClient_ChannelUserFlagsChanged(object sender, AvChat.Client.Events.FlagStatusEventArgs e)
        {
            if (this.InvokeRequired)
            {
                Action<FlagStatusEventArgs> dg = new Action<FlagStatusEventArgs>(GlobalClient_ChannelUserFlagsChangedImpl);
                this.Invoke(dg, e);
            }
            else
            {
                GlobalClient_ChannelUserFlagsChangedImpl(e);
            }
        }
        private void GlobalClient_ChannelUserFlagsChangedImpl(FlagStatusEventArgs e)
        {
            if (e.UserName == Program.GlobalClient.Name)
            {
                // update away status
                this.IsAway = (e.Flags & UserFlags.Away) == UserFlags.Away;
            }
            var tab = this.getChannelTabByName(e.Name);
            if (tab != null)
            {
                tab.OnChannelUserFlagsChanged(e);
            }
        }
        #endregion

        #region Channel - Topic Info Received
        void GlobalClient_ChannelTopicReceived(object sender, AvChat.Client.Events.ChannelTopicEventArgs e)
        {
            if (this.InvokeRequired)
            {
                Action<ChannelTopicEventArgs> dg = new Action<ChannelTopicEventArgs>(GlobalClient_ChannelTopicReceivedImpl);
                this.Invoke(dg, e);
            }
            else
            {
                GlobalClient_ChannelTopicReceivedImpl(e);
            }
        }

        private void GlobalClient_ChannelTopicReceivedImpl(ChannelTopicEventArgs e)
        {
            var tab = this.getChannelTabByName(e.Name);
            if (tab != null)
            {
                tab.OnChannelTopicReceived(e);
            }
        }
        #endregion

        #region Channel - Change of Topic
        void GlobalClient_ChannelTopicChanged(object sender, AvChat.Client.Events.ChangeTopicEventArgs e)
        {
            if (this.InvokeRequired)
            {
                Action<ChangeTopicEventArgs> dg = new Action<ChangeTopicEventArgs>(GlobalClient_ChannelTopicChangedImpl);
                this.BeginInvoke(dg, e);
            }
            else
            {
                this.GlobalClient_ChannelTopicChangedImpl(e);
            }
        }
        private void GlobalClient_ChannelTopicChangedImpl(ChangeTopicEventArgs e)
        {
            var tab = this.getChannelTabByName(e.Name);
            if (tab != null)
            {
                tab.OnChannelTopicChanged(e);
            }
        }
        #endregion

        #region Channel - Kicked Out
        void GlobalClient_ChannelKick(object sender, AvChat.Client.Events.KickChannelEventArgs e)
        {
            if (this.InvokeRequired)
            {
                Action<KickChannelEventArgs> dg = new Action<KickChannelEventArgs>(GlobalClient_ChannelKickImpl);
                this.Invoke(dg, e);
            }
            else
            {
                this.GlobalClient_ChannelKickImpl(e);
            }
        }
        private void GlobalClient_ChannelKickImpl(KickChannelEventArgs e)
        {
            var tab = this.getChannelTabByName(e.Name);
            if (tab != null)
            {
                tab.OnChannelKick(e);
            }
        }
        #endregion

        #region Channel - Join
        void GlobalClient_ChannelJoined(object sender, AvChat.Client.Events.ChannelJoinEventArgs e)
        {
            if (this.InvokeRequired)
            {
                Action<string> dg = new Action<string>(GlobalClient_ChannelJoinedImpl);
                this.Invoke(dg, e.Name);
            }
            else
            {
                GlobalClient_ChannelJoinedImpl(e.Name);
            }
        }

        private void GlobalClient_ChannelJoinedImpl(string channelName)
        {

            // send color to server
            var userColor = Properties.Settings.Default.userTextColor;
            Program.GlobalClient.SetColor(userColor.R, userColor.G, userColor.B);

            var existing = this.getChannelTabByName(channelName);
            if (existing != null)
            {
                tbRooms.SelectTab(existing.ParentTab);
            }
            else
            {
                TabPage tab = new TabPage();
                tab.Padding = new Padding(10);
                tab.UseVisualStyleBackColor = true;

                Tabs.ChannelTab chanTab = new Tabs.ChannelTab(channelName, tab);
                chanTab.Dock = DockStyle.Fill;
                tab.Controls.Add(chanTab);

                tab.Tag = chanTab;

                this.tbRooms.TabPages.Add(tab);

                //tab.Select();

                tbRooms.SelectTab(tab);

                tab.ImageKey = "channel_default";

                leaveChannelToolStripMenuItem.Enabled = true;
            }
        }
        #endregion

        void GlobalClient_ChannelFlagsReceived(object sender, AvChat.Client.Events.ChannelFlagsEventArgs e)
        {
            //TODO Channel flags are not implemented on the server
        }

        #region Global - Alert
        void GlobalClient_AlertReceived(object sender, AvChat.Client.Events.AlertEventArgs e)
        {
            MediaManager.Sounds.PlaySound(Media.Sound.SoundDirectory.SoundTypes.Alert);

            // flash user window
            FLASHWINFO fInfo = new FLASHWINFO();

            fInfo.cbSize = Convert.ToUInt32(Marshal.SizeOf(fInfo));
            fInfo.hwnd = this.Handle;
            fInfo.dwFlags = FLASHW_FLAGS.FLASHW_ALL | FLASHW_FLAGS.FLASHW_TIMERNOFG;
            fInfo.uCount = 4;
            fInfo.dwTimeout = 0;

            Win32Impl.FlashWindowEx(ref fInfo);


            if (this.InvokeRequired)
            {
                Action<AlertEventArgs> dg = new Action<AlertEventArgs>(GlobalClient_AlertReceivedImpl);
                this.Invoke(dg, e);
            }
            else
            {
                GlobalClient_AlertReceivedImpl(e);
            }
        }
        private void GlobalClient_AlertReceivedImpl(AlertEventArgs e)
        {
            foreach (var tab in this.ChannelTabs)
            {
                tab.AddSystemMessage(string.Format("Alert received from {0}", e.Name));
            }
        }
        #endregion

        #region Global/Channel - Add Text (Server)
        void GlobalClient_AddTextEvent(object sender, AvChat.Client.Events.AddTextEventArgs e)
        {
            if (this.InvokeRequired)
            {
                Action<AddTextEventArgs> dg = new Action<AddTextEventArgs>(GlobalClient_AddTextEventImpl);
                this.Invoke(dg, e);
            }
            else
            {
                this.GlobalClient_AddTextEventImpl(e);
            }
        }
        private void GlobalClient_AddTextEventImpl(AddTextEventArgs e)
        {
            if (e.Name != null)
            {
                var tab = this.getChannelTabByName(e.Name);
                if (tab != null)
                {
                    tab.OnAddTextEvent(e);
                }
            }
            //TODO: Add else condition for global message
        }
        #endregion

        #region Channel - Add Status Text
        void GlobalClient_AddStatusEvent(object sender, AddStatusEventArgs e)
        {
            if (this.InvokeRequired)
            {
                Action<AddStatusEventArgs> dg = new Action<AddStatusEventArgs>(GlobalClient_AddStatusEventImpl);
                this.Invoke(dg, e);
            }
            else
            {
                this.GlobalClient_AddStatusEventImpl(e);
            }
        }
        private void GlobalClient_AddStatusEventImpl(AddStatusEventArgs e)
        {
            var tab = this.getChannelTabByName(e.Name);
            if (tab != null)
            {
                tab.OnAddStatusEvent(e);
            }
        }
        #endregion

        #region Channel - Add Message
        void GlobalClient_AddMessageEvent(object sender, AvChat.Client.Events.AddMessageEventArgs e)
        {
            if (this.InvokeRequired)
            {
                Action<AddMessageEventArgs> dg = new Action<AddMessageEventArgs>(GlobalClient_AddMessageEventImpl);
                this.Invoke(dg, e);
            }
            else
            {
                GlobalClient_AddMessageEventImpl(e);
            }
        }
        private void GlobalClient_AddMessageEventImpl(AddMessageEventArgs e)
        {
            if (e.ChannelName.StartsWith("#"))
            {
                var tab = this.getChannelTabByName(e.ChannelName);
                if (tab != null)
                {
                    tab.OnMessageReceived(e);
                }
            }
            else
            {
                var tab = this.getPrivateTabByName(e.ChannelName);
                if (tab == null)
                {
                    // create new tab
                    TabPage tabPage = new TabPage();
                    tabPage.Padding = new Padding(10);
                    tabPage.UseVisualStyleBackColor = true;

                    Crypton.AvChat.Gui.Tabs.ChannelTab.LocalUserInfo inf = new ChannelTab.LocalUserInfo();
                    inf.UserName = e.UserInfo.Name;
                    inf.UserID = e.UserInfo.UserID;

                    tab = new Tabs.PrivateTab(inf, tabPage, e.ChannelName);
                    tab.Dock = DockStyle.Fill;
                    tabPage.Controls.Add(tab);

                    tabPage.Tag = tab;

                    this.tbRooms.TabPages.Add(tabPage);

                    //tab.Select();

                    //tbRooms.SelectTab(tabPage);

                    tabPage.ImageKey = "channel_private";
                }
                tab.OnMessageReceived(e);
            }
        }
        #endregion

        #region Global - Connection Status Change
        void GlobalClient_ConnectionStatusChanged(object sender, AvChat.Client.ConnectionStatusChangeEventArgs e)
        {
            if (this.InvokeRequired)
            {
                Action<ConnectionStatusChangeEventArgs> dg = new Action<ConnectionStatusChangeEventArgs>(GlobalClient_ConnectionStatusChangedImpl);
                this.Invoke(dg, e);
            }
            else
            {
                GlobalClient_ConnectionStatusChangedImpl(e);
            }
        }

        private void GlobalClient_ConnectionStatusChangedImpl(ConnectionStatusChangeEventArgs e)
        {
            switch (e.Status)
            {
                case ConnectionStatusTypes.Connected:
                    tsConnectDisconnect.Text = "Connected";
                    tsConnectDisconnect.Enabled = true;

                    lblConnectionStatus.Text = "Connected";
                    tsConnectDisconnect.Image = DefaultResources.connect;

                    foreach (var tab in this.ChannelTabs.Where(t => t.Available))
                    {
                        Program.GlobalClient.SendCommand("/join " + tab.ChannelName);
                        tab.txtNewMessage.ReadOnly = false;
                    }

                    break;
                case ConnectionStatusTypes.Connecting:
                    tsConnectDisconnect.Text = "Connecting";
                    lblConnectionStatus.Text = "Connecting";
                    tsConnectDisconnect.Image = DefaultResources.disconnect;
                    tsConnectDisconnect.Enabled = false;
                    break;
                case ConnectionStatusTypes.Disconnected:
                    tsConnectDisconnect.Text = "Disconnected";
                    lblConnectionStatus.Text = "Disconnected";
                    tsConnectDisconnect.Image = DefaultResources.disconnect;
                    tsConnectDisconnect.Enabled = true;
                    break;
                case ConnectionStatusTypes.ConnectionLost:
                    tsConnectDisconnect.Text = "Connect";
                    tsConnectDisconnect.Enabled = true;
                    lblConnectionStatus.Text = "Connection lost";

                    foreach (var tab in this.ChannelTabs.Where(t => t.Available))
                    {
                        // readonly each tab
                        tab.txtNewMessage.ReadOnly = true;
                    }

                    break;
                case ConnectionStatusTypes.Reconnecting:
                    tsConnectDisconnect.Text = "Disconnected";
                    lblConnectionStatus.Text = "Reconnecting";

                    tsConnectDisconnect.Enabled = false;
                    break;
            }
        }
        #endregion
        #endregion

        #region Form closing / quitting
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // detach from all events and close chat client
            detachEvents();

            Settings.Default.isMainWindowMaximized = this.WindowState == FormWindowState.Maximized;

            Settings.Default.Save();
        }

        private void detachEvents()
        {
            Program.GlobalClient.ConnectionStatusChanged -= GlobalClient_ConnectionStatusChanged;
            Program.GlobalClient.Events.AddMessageEvent -= GlobalClient_AddMessageEvent;
            Program.GlobalClient.Events.AddTextEvent -= GlobalClient_AddTextEvent;
            Program.GlobalClient.Events.AddStatusEvent -= GlobalClient_AddStatusEvent;
            Program.GlobalClient.Events.AlertReceived -= GlobalClient_AlertReceived;
            Program.GlobalClient.Events.ChannelFlagsReceived -= GlobalClient_ChannelFlagsReceived;
            Program.GlobalClient.Events.ChannelJoined -= GlobalClient_ChannelJoined;
            Program.GlobalClient.Events.ChannelKick -= GlobalClient_ChannelKick;
            Program.GlobalClient.Events.ChannelTopicChanged -= GlobalClient_ChannelTopicChanged;
            Program.GlobalClient.Events.ChannelTopicReceived -= GlobalClient_ChannelTopicReceived;
            Program.GlobalClient.Events.ChannelUserFlagsChanged -= GlobalClient_ChannelUserFlagsChanged;
            Program.GlobalClient.Events.ChannelUserJoin -= GlobalClient_ChannelUserJoin;
            Program.GlobalClient.Events.ExitChannel -= GlobalClient_ExitChannel;
            Program.GlobalClient.Events.PingEvent -= GlobalClient_PingEvent;
            Program.GlobalClient.Events.PlayDogSound -= GlobalClient_PlayDogSound;
            Program.GlobalClient.Events.QuitEvent -= GlobalClient_QuitEvent;
            Program.GlobalClient.Events.ServerDisconnect -= GlobalClient_ServerDisconnect;
            Program.GlobalClient.Events.UserColorChanged -= GlobalClient_UserColorChanged;
            Program.GlobalClient.Events.UserLeave -= GlobalClient_UserLeave;
            Program.GlobalClient.Events.UserListReceived -= GlobalClient_UserListReceived;
            Program.GlobalClient.Events.UserQuit -= GlobalClient_UserQuit;
            Program.GlobalClient.Events.UserKicked -= GlobalClient_UserKicked;

            if (Program.GlobalClient.ConnectionStatus == ConnectionStatusTypes.Connected)
            {
                Program.GlobalClient.Logout();
            }
            Program.GlobalClient.Dispose();
        }
        #endregion

        #region Form resizing
        private void MainForm_ResizeBegin(object sender, EventArgs e)
        {
            this.SuspendLayout();
        }

        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            this.ResumeLayout(true);

            // save w&h
            Settings.Default.mainWindowWidth = this.Width;
            Settings.Default.mainWindowHeight = this.Height;


        }

        private void MainForm_Move(object sender, EventArgs e)
        {
            Settings.Default.mainWindowX = this.Left;
            Settings.Default.mainWindowY = this.Top;
        }
        #endregion

        #region Settings / Channel List
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SettingsForm frmSettings = new SettingsForm())
            {
                if (frmSettings.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        private void channelListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ChannelList chanlist = new ChannelList())
            {
                if (chanlist.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var tab = this.getChannelTabByName(chanlist.SelectedChannel.Name);
                    if (tab == null)
                    {
                        if (Program.GlobalClient.ConnectionStatus != ConnectionStatusTypes.Connected)
                        {
                            MessageBox.Show("You are not connected to the server!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        Program.GlobalClient.SendCommand("/join " + chanlist.SelectedChannel.Name);
                    }
                    else
                    {
                        tbRooms.SelectTab(tab.ParentTab);
                    }
                }
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Win32Impl.OpenUrl("http://www.uer.ca/forum_showthread.asp?fid=1&threadid=102140&currpage=1");
        }
        #endregion


        private void tbRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            leaveChannelToolStripMenuItem.Enabled = tbRooms.SelectedTab != null;
            if (tbRooms.SelectedTab != null)
            {
                if (tbRooms.Tag is ChannelTab)
                {
                    ChannelTab tab = (ChannelTab)tbRooms.Tag;
                    this.ActiveControl = tab.txtNewMessage;
                }
                if (tbRooms.Tag is PrivateTab)
                {
                    PrivateTab tab = (PrivateTab)tbRooms.Tag;
                    this.ActiveControl = tab.txtNewMessage;
                }
                Application.DoEvents();
            }
        }

        #region Idle Time Reporter
        private void tmrIdleTimeReporter_Tick(object sender, EventArgs e)
        {
            if (Win32Impl.GetForegroundWindow() != this.Handle)
            {
                // not active form, change reporting to just have it idleing
                Program.GlobalClient.IdleTimer.ReportMode = IdleResetModes.ResetCounter;
            }
            else
            {
                // get last input info
                Program.GlobalClient.IdleTimer.ReportMode = IdleResetModes.ReportIdleTime;

                PLASTINPUTINFO idleTime = new PLASTINPUTINFO();
                idleTime.cbSize = (uint)Marshal.SizeOf(idleTime);

                if (Win32Impl.GetLastInputInfo(out idleTime))
                {

                    long diff = (uint)Environment.TickCount - idleTime.dwTime;

                    TimeSpan time = new TimeSpan(diff * 10000);
                    Program.GlobalClient.IdleTimer.Report(time);
                }
            }
        }
        #endregion

        #region Active State of the Form
        private void MainForm_Activated(object sender, EventArgs e)
        {
            this.activeFlashState = false;
            Win32Impl.RegisterHotKey(this.Handle, this.GetHashCode(), 0, (uint)Keys.F8);
        }
        #endregion

        #region Leave Channel
        private void leaveChannelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tbRooms.SelectedTab != null && Program.GlobalClient.ConnectionStatus == ConnectionStatusTypes.Connected)
            {
                if (tbRooms.SelectedTab.Tag is ChannelTab)
                {
                    ChannelTab tab = (ChannelTab)tbRooms.SelectedTab.Tag;
                    Program.GlobalClient.SendCommand("/part " + tab.ChannelName, tab.ChannelName);

                    tbRooms.TabPages.Remove(tbRooms.SelectedTab);
                }
                else if (tbRooms.SelectedTab.Tag is PrivateTab)
                {
                    // close it
                    tbRooms.TabPages.Remove(tbRooms.SelectedTab);
                }
            }
            else
            {
                MessageBox.Show("You are not connected to the server!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Form Shown (Auto join channels)
        private void MainForm_Shown(object sender, EventArgs e)
        {
            // register boss key activation
            this.autoJoin();

            BackgroundWorker bwUpdateCheck = new BackgroundWorker();
            bwUpdateCheck.DoWork += (bwSender, bwArgs) =>
            {
                CheckForUpdates();
            };
            bwUpdateCheck.RunWorkerAsync();
        }

        private void autoJoin()
        {
            if (Program.GlobalClient.ConnectionStatus == ConnectionStatusTypes.Connected)
            {
                foreach (string chanName in Settings.Default.autoJoinList)
                {
                    if (!string.IsNullOrWhiteSpace(chanName))
                    {
                        Program.GlobalClient.SendCommand("/join " + chanName);
                    }
                }
            }
        }
        #endregion

        #region Connect Or Disconnect
        private void tsConnectDisconnect_Click(object sender, EventArgs e)
        {
            if (Program.GlobalClient.ConnectionStatus == ConnectionStatusTypes.Connected)
            {
                Program.GlobalClient.Logout();
            }
            else if (Program.GlobalClient.ConnectionStatus == ConnectionStatusTypes.Disconnected)
            {
                using (LoginForm frmLogin = new LoginForm())
                {
                    frmLogin.ShowDialog();
                }
            }
        }
        #endregion

        #region Synchronize splitter sizes
        private bool _isSplitterSync = false;
        /// <summary>
        /// Synchronizes splitter size across all tabs
        /// </summary>
        /// <param name="tab">The tab, where the splitter resizing occured</param>
        /// <param name="horizSplitterDistance"></param>
        /// <param name="vertSplitterDistance"></param>
        public void SynchronizeSplitterSizes(TabPage tab, int horizSplitterDistance, int vertSplitterDistance)
        {
            if (_isSplitterSync)
            {
                return;
            }
            _isSplitterSync = true;
            foreach (TabPage otherTab in this.tbRooms.TabPages)
            {
                if (otherTab != tab)
                {
                    // resize
                    if (otherTab.Tag is ChannelTab)
                    {
                        ChannelTab chantab = (ChannelTab)otherTab.Tag;
                        chantab.OnSplitterSync(horizSplitterDistance, vertSplitterDistance);

                        Settings.Default.hSplitterSize = horizSplitterDistance;
                        Settings.Default.vSplitterSize = vertSplitterDistance;
                    }
                    if (otherTab.Tag is PrivateTab)
                    {
                        PrivateTab privtab = (PrivateTab)otherTab.Tag;
                        privtab.OnSplitterSync(horizSplitterDistance);

                        Settings.Default.hSplitterSize = horizSplitterDistance;
                    }
                }
            }
            _isSplitterSync = false;
        }
        #endregion

        #region Tab Re-ordering, Closing
        private void tbRooms_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right && tbRooms.SelectedTab != null)
            {
                this.ctxTabMenu.Show(tbRooms, e.Location);
            }
        }

        bool tbRoomsAllowDrag = false;
        Point tbRoomsDragStartPt;
        private void tbRooms_MouseDown(object sender, MouseEventArgs e)
        {
            if (tbRooms.SelectedTab != null && e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                tbRoomsDragStartPt = e.Location;
                tbRoomsAllowDrag = true;
            }
        }

        private void tbRooms_MouseMove(object sender, MouseEventArgs e)
        {
            if (tbRoomsAllowDrag && tbRooms.SelectedTab != null)
            {
                int xdiff = e.X - tbRoomsDragStartPt.X;
                var tabrect = tbRooms.GetTabRect(tbRooms.SelectedIndex);
                if (Math.Abs(xdiff) > 15)
                {
                    // switch tabs
                    if (xdiff > 0)
                    {
                        // move right
                        if (tbRooms.SelectedIndex + 1 < tbRooms.TabPages.Count)
                        {
                            lock (tbRooms)
                            {
                                int idx = tbRooms.SelectedIndex;
                                var curr = tbRooms.SelectedTab;
                                tbRooms.TabPages.RemoveAt(idx);
                                tbRooms.TabPages.Insert(idx + 1, curr);
                                tbRoomsAllowDrag = false;
                                tbRooms.SelectedTab = curr;
                            }
                        }
                    }
                    else if (xdiff < 0)
                    {
                        // move left
                        if (tbRooms.SelectedIndex > 0)
                        {
                            lock (tbRooms)
                            {
                                int idx = tbRooms.SelectedIndex;
                                var curr = tbRooms.SelectedTab;
                                tbRooms.TabPages.RemoveAt(idx);
                                tbRooms.TabPages.Insert(idx - 1, curr);
                                tbRoomsAllowDrag = false;
                                tbRooms.SelectedTab = curr;
                            }
                        }
                    }
                }
            }
        }

        private void tbRooms_MouseUp(object sender, MouseEventArgs e)
        {
            tbRoomsAllowDrag = false;
        }

        private void closeTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tbRooms.SelectedTab != null)
            {
                if (tbRooms.SelectedTab.Tag is ChannelTab)
                {
                    ChannelTab chan = (ChannelTab)tbRooms.SelectedTab.Tag;
                    if (Program.GlobalClient.ConnectionStatus == ConnectionStatusTypes.Connected)
                    {
                        Program.GlobalClient.SendCommand("/part " + chan.ChannelName, chan.ChannelName);
                    }
                    tbRooms.TabPages.Remove(tbRooms.SelectedTab);
                }
                else if (tbRooms.SelectedTab.Tag is PrivateTab)
                {
                    tbRooms.TabPages.Remove(tbRooms.SelectedTab);
                }
            }
        }

        private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int p = 0; p < tbRooms.TabPages.Count; p++)
            {
                TabPage tab = tbRooms.TabPages[p];
                if (tbRooms.SelectedTab.Tag is ChannelTab)
                {
                    ChannelTab chan = (ChannelTab)tbRooms.SelectedTab.Tag;
                    if (Program.GlobalClient.ConnectionStatus == ConnectionStatusTypes.Connected)
                    {
                        Program.GlobalClient.SendCommand("/part " + chan.ChannelName, chan.ChannelName);
                    }
                    tbRooms.TabPages.RemoveAt(p);
                    p--;
                }
                else if (tbRooms.SelectedTab.Tag is PrivateTab)
                {
                    tbRooms.TabPages.RemoveAt(p);
                    p--;
                }
            }
        }

        private void closePrivateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var privChans = PrivateTabs;
            foreach (var priv in privChans)
            {
                tbRooms.TabPages.Remove(priv.ParentTab);
            }
        }
        #endregion

        #region Boss Key Filtering
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0x312)
            { // WM_HOTKEY
                // filter only for foreground apps
                if (Win32Impl.GetForegroundWindow() == this.Handle)
                {
                    // HIDE!
                    this.ShowInTaskbar = false;
                    this.Visible = false;
                    this.nfBossMode.Visible = true;
                }
            }
        }

        private void nfBossMode_Click(object sender, EventArgs e)
        {
            this.ShowInTaskbar = true;
            this.Visible = true;
            this.nfBossMode.Visible = false;
        }
        #endregion

        #region Update

        private bool updateAvailable = false;
        private bool updateRequired = false;

        private void CheckForUpdates()
        {
        }

        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckForUpdates();
        }

        private void CurrentDeployment_CheckForUpdateCompleted(object sender, CheckForUpdateCompletedEventArgs e)
        {
            prgUpdate.Visible = false;
            if (e.UpdateAvailable)
            {
                updateAvailable = true;
                updateRequired = e.IsUpdateRequired;
                newVersionIsAvailableToolStripMenuItem.Visible = true;
                lblConnectionStatus.Text = "Check for updates complete: new version is available!";
            }
            else
            {
                lblConnectionStatus.Text = "Check for updates complete: nothing new";
            }
        }

        void CurrentDeployment_CheckForUpdateProgressChanged(object sender, DeploymentProgressChangedEventArgs e)
        {
            prgUpdate.Visible = true;
            switch (e.State)
            {
                case DeploymentProgressState.DownloadingApplicationFiles:
                    lblConnectionStatus.Text = "Downloading app files..." + e.Group;
                    break;
                case DeploymentProgressState.DownloadingApplicationInformation:
                    lblConnectionStatus.Text = "Downloading app info...";
                    break;
                case DeploymentProgressState.DownloadingDeploymentInformation:
                    lblConnectionStatus.Text = "Downloading deployment info...";
                    break;
            }
            prgUpdate.Value = e.ProgressPercentage;
        }

        void CurrentDeployment_UpdateProgressChanged(object sender, DeploymentProgressChangedEventArgs e)
        {
            prgUpdate.Visible = true;
            switch (e.State)
            {
                case DeploymentProgressState.DownloadingApplicationFiles:
                    lblConnectionStatus.Text = "Downloading app files..." + e.Group;
                    break;
                case DeploymentProgressState.DownloadingApplicationInformation:
                    lblConnectionStatus.Text = "Downloading app info...";
                    break;
                case DeploymentProgressState.DownloadingDeploymentInformation:
                    lblConnectionStatus.Text = "Downloading deployment info...";
                    break;
            }
            prgUpdate.Value = e.ProgressPercentage;
        }

        void CurrentDeployment_UpdateCompleted(object sender, AsyncCompletedEventArgs e)
        {
            newVersionIsAvailableToolStripMenuItem.Visible = false;
            if (updateRequired)
            {
                MessageBox.Show("An urgent update has been released for the chat client. The application will now restart.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Restart();
            }
            else
            {
                if (MessageBox.Show("Updates have been applied. Would you like to restart the application?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    Application.Restart();
                }
            }
        }

        void CurrentDeployment_DownloadFileGroupProgressChanged(object sender, DeploymentProgressChangedEventArgs e)
        {
            prgUpdate.Visible = true;
            switch (e.State)
            {
                case DeploymentProgressState.DownloadingApplicationFiles:
                    lblConnectionStatus.Text = "Downloading app files..." + e.Group;
                    break;
                case DeploymentProgressState.DownloadingApplicationInformation:
                    lblConnectionStatus.Text = "Downloading app info...";
                    break;
                case DeploymentProgressState.DownloadingDeploymentInformation:
                    lblConnectionStatus.Text = "Downloading deployment info...";
                    break;
            }
            prgUpdate.Value = e.ProgressPercentage;
        }

        private void newVersionIsAvailableToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            newVersionIsAvailableToolStripMenuItem.Enabled = false;
        }
        #endregion

        private void reportBugfeatureRequestToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


    }
}
