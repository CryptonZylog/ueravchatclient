using Crypton.AvChat.Gui.Media;
using Crypton.AvChat.Gui.Properties;
using Crypton.AvChat.Gui.Win32;
using Crypton.AvChat.Client;
using Crypton.AvChat.Client.Events;
using mshtml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Web;
using System.Windows.Forms;

namespace Crypton.AvChat.Gui.Tabs {
    public partial class ChannelTab : UserControl {

        public class LocalUserInfo {
            public int UserID {
                get;
                set;
            }
            public string UserName {
                get;
                set;
            }
            public UserFlags Flags {
                get;
                set;
            }
            public DateTime? LocalTime {
                get;
                set;
            }
            public TimeSpan? IdleTime {
                get;
                set;
            }
            public string ClientInfo {
                get;
                set;
            }
        }

        /// <summary>
        /// Gets the channel name
        /// </summary>
        public string ChannelName {
            get;
            private set;
        }
        /// <summary>
        /// Gets the parent tab
        /// </summary>
        public TabPage ParentTab {
            get;
            private set;
        }

        /// <summary>
        /// Gets the selected user in the user list
        /// </summary>
        public LocalUserInfo SelectedUser {
            get {
                var row = lvUsers.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
                if (row == null) {
                    return null;
                }
                return (LocalUserInfo)row.Tag;
            }
        }
        /// <summary>
        /// Gets the list of users
        /// </summary>
        public IEnumerable<LocalUserInfo> ChannelUsers {
            get {
                return lvUsers.Items.Cast<ListViewItem>().Select(r => r.Tag as LocalUserInfo);
            }
        }

        private HtmlElement HTMLTag {
            get {
                HtmlElement html = (HtmlElement)wbChat.Document.GetElementsByTagName("HTML").Cast<HtmlElement>().FirstOrDefault();
                return html;
            }
        }

        private bool IsVScrollAtBottom {
            get {
                var html = HTMLTag;
                if (html != null) {
                    int offset = html.ScrollRectangle.Height - html.ClientRectangle.Height;
                    if (offset < 0)
                        return true;
                    return offset == html.ScrollTop;
                }
                return false;
            }
            set {
                var html = HTMLTag;
                if (html != null) {
                    int offset = html.ScrollRectangle.Height - html.ClientRectangle.Height;
                    if (offset > 0)
                        html.ScrollTop = offset;
                }
            }
        }


        /// <summary>
        /// Gets the available channel state. This is used as a flag upon reconnection
        /// </summary>
        public bool Available {
            get;
            private set;
        }

        /// <summary>
        /// Gets our standing within the channel
        /// </summary>
        public LocalUserInfo CurrentUser {
            get {
                return this.ChannelUsers.FirstOrDefault(u => u.UserName == Program.GlobalClient.Name);
            }
        }

        private bool tabIsActive = true;
        private bool tabMessageReceived = false;
        private bool formReady = false;
        private StreamWriter logHistoryWriter = null;

        private List<string> trackedSentMessages = new List<string>();
        private int trackedSentMessageIndex = 0;

        private ChannelTopicEventArgs ChannelTopicInfo = null;

        public ChannelTab(string channelName, TabPage parentTab) {
            InitializeComponent();
            this.ParentTab = parentTab;
            this.ChannelName = channelName;
            this.Font = SystemFonts.DialogFont;
            this.ParentTab.Text = channelName;

            // tab events
            parentTab.GotFocus += new EventHandler(parentTab_GotFocus);
            parentTab.Enter += new EventHandler(parentTab_Enter);
            parentTab.Leave += new EventHandler(parentTab_Leave);

            this.lblTopic.Text = channelName;

            wbChat.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(wbChat_DocumentCompleted);
            wbChat.DocumentText = generateBrowserBody();
            wbChat.Document.Encoding = "utf-8";
            wbChat.ObjectForScripting = new BrowserProxy(this);

            if (Settings.Default.enableHistory) {
                logHistoryWriter = History.HistoryManager.CreateChannelLog(this.ChannelName, DateTime.Now);
            }

            tmrUnreadMsgFlash.Start();

            this.Available = true;

            if (Settings.Default.hSplitterSize > -1) {
                splitMain.SplitterDistance = Settings.Default.hSplitterSize;
            }
            if (Settings.Default.vSplitterSize > -1) {
                splitContainer1.SplitterDistance = Settings.Default.vSplitterSize;
            }

        }


        ////////////////////////////////////////

        #region Notification Thread-Safe Methods
        /// <summary>
        /// Channel topic information
        /// </summary>
        /// <param name="e"></param>
        public void OnChannelTopicReceived(ChannelTopicEventArgs e) {
            this.ChannelTopicInfo = e;
            if (!string.IsNullOrEmpty(e.Topic)) {
                this.setTopicInfo(e.Topic);
                //this.lblTopic.Text = HttpUtility.HtmlDecode(e.Topic);
                this.addSystemMessageImpl(string.Format("topic is '{0}'", HttpUtility.HtmlDecode(this.ChannelTopicInfo.Topic)));
                this.addSystemMessageImpl(string.Format("set by {0} on {1}", this.ChannelTopicInfo.SetBy, this.ChannelTopicInfo.Date));
            }
        }
        /// <summary>
        /// Channel topic changed
        /// </summary>
        /// <param name="e"></param>
        public void OnChannelTopicChanged(ChangeTopicEventArgs e) {
            this.setTopicInfo(e.Topic.Topic);
            //this.lblTopic.Text = HttpUtility.HtmlDecode(e.Topic.Topic);
            this.addSystemMessageImpl(string.Format("{0} changes topic to '{1}'", e.Topic.UserName, e.Topic.Topic));
        }
        /// <summary>
        /// Kicked out of the channel
        /// </summary>
        /// <param name="e"></param>
        public void OnChannelKick(KickChannelEventArgs e) {
            //addSystemMessage(e.Text);
            this.addSystemMessageImpl(e.Message);
            txtNewMessage.ReadOnly = true;
            lvUsers.Enabled = false;
            this.Available = false;
        }
        /// <summary>
        /// User list received for channel
        /// </summary>
        /// <param name="e"></param>
        public void OnUserListReceived(UserListEventArgs e) {
            this.lvUsers.Clear();
            if (Notifications.Default.Mode != NotificationTypes.Disabled && Notifications.Default.EnableSounds) {
                MediaManager.Sounds.PlaySound(Media.Sound.SoundDirectory.SoundTypes.Join);
            }
            foreach (var user in e.Users) {
                LocalUserInfo inf = new LocalUserInfo();
                inf.Flags = user.Flags;
                inf.ClientInfo = user.ClientVersion;
                inf.UserName = user.Name;
                inf.LocalTime = user.LocalTime;
                inf.UserID = user.UserID;
                this.addUserToList(inf);
            }
        }
        /// <summary>
        /// User joined the channel
        /// </summary>
        /// <param name="e"></param>
        public void OnUserJoin(UserJoinEventArgs e) {
            LocalUserInfo info = new LocalUserInfo();
            info.UserName = e.User.Name;
            info.ClientInfo = e.User.ClientVersion;
            info.Flags = e.User.Flags;
            info.LocalTime = e.User.LocalTime;
            info.UserID = e.User.UserID;
            this.addUserToList(info);

            this.addSystemMessageImpl(string.Format("{0} has joined {1}", e.User.Name, this.ChannelName));

            if (Notifications.Default.Mode != NotificationTypes.Disabled && Notifications.Default.EnableSounds) {
                MediaManager.Sounds.PlaySound(Media.Sound.SoundDirectory.SoundTypes.Join);
            }

            // check ops white list and if we have ops            
            var currentUser = this.ChannelUsers.FirstOrDefault(u => u.UserName == Program.GlobalClient.Name);
            if (currentUser != null && (currentUser.Flags & UserFlags.Op) == UserFlags.Op) {
                var wlChan = Crypton.AvChat.Gui.Power.OpWhitelist.WhitelistManager.List.FirstOrDefault(l => l.ChannelName == this.ChannelName);
                if (wlChan != null) {
                    if (wlChan.Users.Any(u => e.User.Name.StartsWith(u.Name, StringComparison.InvariantCultureIgnoreCase))) {
                        // op
                        if (Program.GlobalClient.ConnectionStatus == ConnectionStatusTypes.Connected) {
                            Program.GlobalClient.SendCommand("/control op " + e.User.Name, this.ChannelName);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// User flags updated
        /// </summary>
        /// <param name="e"></param>
        public void OnChannelUserFlagsChanged(FlagStatusEventArgs e) {
            var row = lvUsers.Items.Cast<ListViewItem>().FirstOrDefault(r => (r.Tag as LocalUserInfo).UserName == e.UserName);
            if (row != null) {
                LocalUserInfo inf = (LocalUserInfo)row.Tag;
                inf.Flags = e.Flags;
                setUserDisplayStyle(row, inf);
            }
        }
        /// <summary>
        /// User left channel
        /// </summary>
        /// <param name="e"></param>
        public void OnUserLeave(UserLeaveEventArgs e) {
            var row = lvUsers.Items.Cast<ListViewItem>().FirstOrDefault(r => (r.Tag as LocalUserInfo).UserName == e.UserInfo.Name);
            if (row != null) {
                lvUsers.Items.Remove(row);
            }
            if (!string.IsNullOrWhiteSpace(e.UserInfo.Message)) {
                this.addSystemMessageImpl(string.Format("{0} has left {1} ({2})", e.UserInfo.Name, e.Name, e.UserInfo.Message));
            }
            else {
                this.addSystemMessageImpl(string.Format("{0} has left {1}", e.UserInfo.Name, e.Name));
            }
        }
        /// <summary>
        /// User kicked
        /// </summary>
        /// <param name="e"></param>
        public void OnUserKick(UserKickEventArgs e) {
            var row = lvUsers.Items.Cast<ListViewItem>().FirstOrDefault(r => (r.Tag as LocalUserInfo).UserName == e.KickedUser);
            if (row != null) {
                lvUsers.Items.Remove(row);
            }
            this.addSystemMessageImpl(string.Format("{0} has been kicked by {1} ({2})", e.KickedUser, e.WhoKicked, e.Reason));
        }
        /// <summary>
        /// User quit chat
        /// </summary>
        /// <param name="e"></param>
        public void OnUserQuit(UserQuitEventArgs e) {
            var row = lvUsers.Items.Cast<ListViewItem>().FirstOrDefault(r => (r.Tag as LocalUserInfo).UserName == e.UserInfo.Name);
            if (row != null) {
                lvUsers.Items.Remove(row);
            }
            this.addSystemMessageImpl(string.Format("{0} has quit AvChat ({1})", e.UserInfo.Name, e.UserInfo.Message));
        }
        /// <summary>
        /// Server disconnect
        /// </summary>
        /// <param name="e"></param>
        public void OnServerDisconnect(ServerDisconnectEventArgs e) {
            this.addSystemMessageImpl(e.Text);
        }
        /// <summary>
        /// Exit channel (/part)
        /// </summary>
        /// <param name="e"></param>
        public void OnExitChannel(ExitChannelEventArgs e) {
            txtNewMessage.ReadOnly = true;
            lvUsers.Enabled = false;
            this.Available = false;
        }

        /// <summary>
        /// Loops current thread until the web browser document becomes readily-available
        /// </summary>
        private void waitForDocumentReady() {
            while (wbChat.Document.Body == null || this.wbChat.Document.GetElementsByTagName("BODY").Count == 0) {
                Thread.Sleep(2);
                Application.DoEvents();
            }
        }

#if(EXCLUDED)

        *****
            FUTURE 


        /// <summary>
        /// Returns a new user message node
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private HtmlElement createUserMessageNode(AddMessageEventArgs e) {
            this.waitForDocumentReady();
            HtmlElement liMsgContainer = wbChat.Document.CreateElement("li");
            liMsgContainer.SetAttribute("class", "userMessage");

            // Date/Time received
            HtmlElement liMsgDate = wbChat.Document.CreateElement("span");
            liMsgDate.SetAttribute("title", "Click to quote this message");
            liMsgDate.SetAttribute("class", "date");
            if (e.UserInfo.IsMyself) {
                // pink it
                liMsgDate.Style = "color:#FF71FF";
            }
            liMsgDate.Click += new HtmlElementEventHandler(liMsgContainer_Click);
            liMsgDate.InnerText = DateTime.Now.ToString("HH:mm:ss", new CultureInfo("en-US"));
            liMsgContainer.AppendChild(liMsgDate);

            if (e.UserInfo.IsMe) {
                HtmlElement spanMsg = wbChat.Document.CreateElement("span");
                spanMsg.SetAttribute("class", "message");
                if (!string.IsNullOrEmpty(e.UserInfo.Color)) {
                    spanMsg.Style = "color:" + e.UserInfo.Color;
                }

                spanMsg.InnerHtml = string.Format("*&nbsp;{0}&nbsp;{1}", e.UserInfo.Name, e.Text);

                liMsgContainer.AppendChild(spanMsg);
            }
            else {



                hw.Write("&lt;");
                hw.AddAttribute("class", "username");


                switch (e.UserInfo.Gender) {
                    case UserGenders.Male:
                        hw.AddStyleAttribute("color", "#80A4BC");
                        break;
                    case UserGenders.Female:
                        hw.AddStyleAttribute("color", "#D23EA6");
                        break;
                    case UserGenders.Unknown:
                        hw.AddStyleAttribute("color", "#FFF654");
                        break;
                }

                hw.RenderBeginTag("span");
                hw.WriteEncodedText(e.UserInfo.Name);
                hw.RenderEndTag();
                hw.Write("&gt;");

                hw.Write("&nbsp;");
                hw.AddAttribute("class", "message");
                if (!string.IsNullOrEmpty(e.UserInfo.Color)) {
                    hw.AddStyleAttribute("color", e.UserInfo.Color);
                }
                hw.RenderBeginTag("span");
                hw.Write(e.Text.Replace("&apos;", "&#39;"));
                hw.RenderEndTag();
            }

            // fix the dreaded apostrophe
            liMsgContainer.InnerHtml = liMsgContainer.InnerHtml.Replace("&apos;", "&#39;");

            return liMsgContainer;
        }

        /// <summary>
        /// Handles clicking on message date
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void liMsgContainer_Click(object sender, HtmlElementEventArgs e) {
            //TODO: quote message
        }
#endif

        /// <summary>
        /// User message received
        /// </summary>
        /// <param name="e"></param>
        public void OnMessageReceived(AddMessageEventArgs e) {
            if (!this.tabIsActive) {
                this.tabMessageReceived = true;
            }
            bool isOwn = false;
            bool isWatchword = false;
            using (StringWriter sw = new StringWriter()) {
                using (System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw)) {
                    // date
                    hw.AddAttribute("title", "Click to quote this message");
                    hw.AddAttribute("onclick", "return quoteText(this);");
                    hw.AddAttribute("class", "date");
                    if (e.UserInfo.IsMyself) {
                        // pink it
                        hw.AddStyleAttribute("color", "#FF71FF");
                    }
                    hw.RenderBeginTag("span");
                    hw.WriteEncodedText(DateTime.Now.ToString("HH:mm:ss", new CultureInfo("en-US")));
                    hw.RenderEndTag();

                    hw.Write("&nbsp;");

                    // user message

                    isOwn = e.UserInfo.Name == Program.GlobalClient.Name;
                    isWatchword = Watchwords.Default.List != null && Watchwords.Default.Enabled ? Watchwords.Default.List.Cast<string>().Any(f => e.Text.IndexOf(f, StringComparison.InvariantCultureIgnoreCase) > -1) : false;

                    if (e.UserInfo.IsMe) {
                        hw.AddAttribute("class", "message");
                        if (!string.IsNullOrEmpty(e.UserInfo.Color)) {
                            hw.AddStyleAttribute("color", e.UserInfo.Color);
                        }
                        hw.RenderBeginTag("span");
                        hw.Write("*&nbsp;{0}&nbsp;{1}", e.UserInfo.Name, e.Text);
                        hw.RenderEndTag();
                    }
                    else {
                        hw.Write("&lt;");
                        hw.AddAttribute("onclick", string.Format("window.external.open('http://www.uer.ca/forum_showprofile.asp?posterid={0}')", e.UserInfo.UserID));
                        hw.AddAttribute("class", "username");


                        switch (e.UserInfo.Gender) {
                            case UserGenders.Male:
                                hw.AddStyleAttribute("color", "#80A4BC");
                                break;
                            case UserGenders.Female:
                                hw.AddStyleAttribute("color", "#D23EA6");
                                break;
                            case UserGenders.Unknown:
                                hw.AddStyleAttribute("color", "#FFF654");
                                break;
                        }

                        hw.RenderBeginTag("span");
                        hw.WriteEncodedText(e.UserInfo.Name);
                        hw.RenderEndTag();
                        hw.Write("&gt;");

                        hw.Write("&nbsp;");
                        hw.AddAttribute("class", "message");
                        if (!string.IsNullOrEmpty(e.UserInfo.Color)) {
                            hw.AddStyleAttribute("color", e.UserInfo.Color);
                        }
                        if (isWatchword) {
                            hw.AddStyleAttribute("font-weight", "bold");
                        }
                        hw.AddAttribute("class", "messageBody");
                        hw.RenderBeginTag("span");
                        hw.Write(e.Text.Replace("&apos;", "&#39;").Replace("&#x9;", "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"));
                        hw.RenderEndTag();
                    }

                    /*else {
                        // server message
                        // display as green text
                        hw.AddAttribute("class", "message");
                        hw.AddStyleAttribute("color", "#74FF65");
                        hw.RenderBeginTag("span");
                        hw.Write(msg.Text);
                        hw.RenderEndTag();
                    }*/

                }

                // Append to the Document
                this.appendMessageNodeHtml(sw.ToString(), true);
                // add notification
                NotificationHelper.NotifyMessageReceived(tabIsActive, false, isWatchword, isOwn);
            }
        }
        /// <summary>
        /// Server message received
        /// </summary>
        /// <param name="e"></param>
        public void OnAddTextEvent(AddTextEventArgs e) {
            waitForDocumentReady();
            using (StringWriter sw = new StringWriter()) {
                using (System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw)) {
                    // date
                    hw.AddAttribute("title", "Click to quote this message");
                    hw.AddAttribute("onclick", "return quoteText(this);");
                    hw.AddAttribute("class", "date");
                    hw.RenderBeginTag("span");
                    hw.WriteEncodedText(DateTime.Now.ToString("HH:mm:ss", new CultureInfo("en-US")));
                    hw.RenderEndTag();

                    hw.Write("&nbsp;");

                    // server message
                    // display as green text
                    hw.AddAttribute("class", "message");
                    hw.AddStyleAttribute("color", "#74FF65");
                    hw.RenderBeginTag("span");
                    hw.Write(e.Text);
                    hw.RenderEndTag();

                }

                // Append to the Document
                this.appendMessageNodeHtml(sw.ToString());
            }
        }
        /// <summary>
        /// Add status text
        /// </summary>
        /// <param name="e"></param>
        public void OnAddStatusEvent(AddStatusEventArgs e) {
            using (StringWriter sw = new StringWriter()) {
                using (System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw)) {
                    // date
                    hw.AddAttribute("title", "Click to quote this message");
                    hw.AddAttribute("onclick", "return quoteText(this);");
                    hw.AddAttribute("class", "date");
                    hw.RenderBeginTag("span");
                    hw.WriteEncodedText(DateTime.Now.ToString("HH:mm:ss", new CultureInfo("en-US")));
                    hw.RenderEndTag();

                    hw.Write("&nbsp;");

                    // server message
                    // display as green text
                    hw.AddAttribute("class", "message");
                    hw.AddStyleAttribute("color", "#74FF65");
                    hw.RenderBeginTag("span");
                    hw.Write(e.Text);
                    hw.RenderEndTag();

                }

                // Append to the Document
                this.appendMessageNodeHtml(sw.ToString());
            }
        }

        private void setTopicInfo(string topicHtml) {
            while (wbChat.Document.Body == null) {
                Thread.Sleep(2);
                Application.DoEvents();
            }
            HtmlElement elem = wbChat.Document.GetElementById("topic");
            if (elem != null) {
                elem.InnerHtml = topicHtml.Replace("&apos;", "&#39;");
                this.lblTopic.Text = elem.InnerText;
            }
        }

        private void appendMessageNodeHtml(string html, bool playNotifySound = true) {
            while (wbChat.Document.Body == null) {
                Thread.Sleep(2);
                Application.DoEvents();
            }
            bool isScrollAtBottom = IsVScrollAtBottom;
            HtmlElement ulMessageNode = wbChat.Document.GetElementById("messageList");
            HtmlElement elem = null;
            if (ulMessageNode != null) {
                elem = wbChat.Document.CreateElement("li");
                elem.SetAttribute("class", "userMessage");
                elem.InnerHtml = html.Replace("&apos;", "&#39;");
                ulMessageNode.AppendChild(elem);

                HtmlElementCollection links = elem.GetElementsByTagName("A");
                foreach (HtmlElement link in links) {
                    link.Click += new HtmlElementEventHandler(link_Click);
                }
            }

            if (Settings.Default.enableHistory && this.logHistoryWriter != null) {
                if (this.logHistoryWriter == null) {
                    this.logHistoryWriter = History.HistoryManager.CreateChannelLog(this.ChannelName, DateTime.Now);
                }
                lock (this.logHistoryWriter) {
                    var htmlBody = wbChat.Document.GetElementById("html");
                    this.logHistoryWriter.BaseStream.Position = 0;
                    this.logHistoryWriter.Write(htmlBody.InnerHtml);
                    this.logHistoryWriter.Flush();
                }
            }

            if (elem != null && isScrollAtBottom) {
                HtmlElement push = wbChat.Document.CreateElement("SPAN");
                IsVScrollAtBottom = true;
            }
        }

        void link_Click(object sender, HtmlElementEventArgs e) {
            e.BubbleEvent = false;
            e.ReturnValue = false;

            string url = (sender as HtmlElement).GetAttribute("HREF");

            try {
                Process.Start(url);
            }
            catch (Exception ex) {
                MessageBox.Show("Unable to launch the browser. This may be caused by a lag in the system. Try again, it should work. Also, details are in trace.csv", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Trace.TraceError(ex.ToString());
            }
        }
        /// <summary>
        /// Adds a system message
        /// </summary>
        /// <param name="text"></param>
        public void AddSystemMessage(string text) {
            if (this.InvokeRequired) {
                Action<string> dg = new Action<string>(addSystemMessageImpl);
                this.Invoke(dg, text);
            }
            else {
                this.addSystemMessageImpl(text);
            }
        }

        private void addSystemMessageImpl(string text) {
            using (StringWriter sw = new StringWriter()) {
                using (System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw)) {
                    // date
                    hw.AddAttribute("title", "Click to quote this message");
                    hw.AddAttribute("onclick", "return quoteText(this);");
                    hw.AddAttribute("class", "date");
                    hw.RenderBeginTag("span");
                    hw.WriteEncodedText(DateTime.Now.ToString("HH:mm:ss", new CultureInfo("en-US")));
                    hw.RenderEndTag();

                    hw.Write("&nbsp;");

                    // server message
                    // display as green text
                    hw.AddAttribute("class", "message");
                    hw.AddStyleAttribute("color", "#74FF65");
                    hw.RenderBeginTag("span");
                    hw.Write("***&nbsp;" + text);
                    hw.RenderEndTag();

                }

                // Append to the Document
                this.appendMessageNodeHtml(sw.ToString(), false);
            }
        }

        #endregion

        ////////////////////////////////////////

        #region UserList Management
        private void addUserToList(LocalUserInfo item) {
            ListViewItem row = new ListViewItem();
            row.Tag = item;
            lvUsers.Items.Add(row);
            lvUsers.Sort();
            setUserDisplayStyle(row, item);
            using (Graphics gf = Graphics.FromHwnd(this.ParentForm.Handle)) {
                var textSize = gf.MeasureString(row.Text, row.Font);
                var imageBounds = row.Bounds;

                int finalWidth = (int)textSize.Width + imageBounds.Width + 4; // 4px padding
                if (finalWidth > lvUsers.TileSize.Width) {
                    lvUsers.TileSize = new Size(finalWidth, lvUsers.TileSize.Height);
                }
            }
        }

        private void setUserDisplayStyle(ListViewItem row, LocalUserInfo user) {
            row.Text = user.UserName;
            if (user.UserID > 0) {
                downloadAvatarAsync(user.UserID);
            }

            FontStyle userDisplayStyle = FontStyle.Regular;
            Font displayFont = row.Font ?? lvUsers.Font;
            if ((user.Flags & UserFlags.Op) == UserFlags.Op) {
                userDisplayStyle |= FontStyle.Bold;
            }
            if ((user.Flags & UserFlags.Voice) == UserFlags.Voice) {
                userDisplayStyle |= FontStyle.Underline;
            }
            if ((user.Flags & UserFlags.Away) == UserFlags.Away) {
                userDisplayStyle |= FontStyle.Italic;
            }
            row.Font = new Font(displayFont, userDisplayStyle);
        }

        private void downloadAvatarAsync(int userId) {
            Action<int> dgDownloadImage = new Action<int>(downloadAvatarImpl);
            dgDownloadImage.BeginInvoke(userId, null, null);
        }

        private void downloadAvatarImpl(int userId) {
            Image img = Cache.CacheManager.DownloadAvatar(userId);
            if (img != null) {
                if (this.InvokeRequired) {
                    Action<int, Image> dg = new Action<int, Image>(updateGridViewImageImpl);
                    this.Invoke(dg, userId, img);
                }
                else {
                    updateGridViewImageImpl(userId, img);
                }
            }
        }

        private void updateGridViewImageImpl(int userid, Image img) {
            lock (imgUserAvatars) {
                if (!imgUserAvatars.Images.ContainsKey(userid.ToString())) {
                    try {
                        imgUserAvatars.Images.Add(userid.ToString(), img);
                    }
                    catch (Exception ex) {
                        Trace.TraceError("updateGridViewImageImpl: " + ex.ToString());
                    }
                }
            }
            var rows = lvUsers.Items.Cast<ListViewItem>().Where(i => (i.Tag as LocalUserInfo).UserID == userid);
            foreach (var row in rows) {
                row.ImageKey = userid.ToString();
                Trace.TraceInformation("UserAvatar: " + row.ImageKey);
            }
        }
        #endregion

        #region Web Browser - HTML
        private string generateBrowserBody() {
            using (StringWriter sw = new StringWriter()) {

                using (System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw)) {
                    hw.WriteLine("<!DOCTYPE html>");
                    hw.AddAttribute("id", "html");
                    hw.RenderBeginTag("html");

                    hw.RenderBeginTag("head");


                    hw.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");

                    // user css
                    hw.RenderBeginTag("style");
                    buildUserCSS(hw);
                    hw.RenderEndTag();

                    // jquery
                    hw.AddAttribute("type", "text/javascript");
                    hw.RenderBeginTag("script");
                    hw.Write(DefaultResources.jquery_min);
                    hw.RenderEndTag();

                    // app js
                    hw.AddAttribute("type", "text/javascript");
                    hw.RenderBeginTag("script");
                    hw.Write(DefaultResources.window);
                    hw.RenderEndTag();

                    hw.RenderEndTag();

                    hw.RenderBeginTag("body");

                    hw.AddAttribute("id", "topic");
                    hw.AddStyleAttribute("display", "none");
                    hw.RenderBeginTag("div");
                    hw.RenderEndTag();

                    hw.AddAttribute("id", "messageList");
                    hw.RenderBeginTag("ul");
                    hw.RenderEndTag();
                    hw.RenderEndTag();

                    hw.RenderEndTag();
                }

                return sw.ToString();
            }
        }

        private void buildUserCSS(TextWriter output) {
            {
                // base css
                output.WriteLine(DefaultResources.base_css);
            }
            {   // body, html
                output.WriteLine("body, html {");
                output.WriteLine("padding: 0; margin: 0;");
                output.WriteLine("font-family: '{0}';", Settings.Default.userFont.Name);
                output.WriteLine("font-size: {0}pt;", Settings.Default.userFont.SizeInPoints.ToString(CultureInfo.InvariantCulture));
                output.WriteLine("background-color: {0} !important;", ColorTranslator.ToHtml(Settings.Default.userBackColor));
                output.WriteLine("}");
            }

        }
        #endregion

        #region Web Browser Events
        void wbChat_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e) {
            this.addSystemMessageImpl("now talking in " + this.ChannelName);
            this.formReady = true;
        }
        #endregion

        #region Tab Leave/Enter events
        void parentTab_Leave(object sender, EventArgs e) {
            tabIsActive = false;
        }

        void parentTab_Enter(object sender, EventArgs e) {
            tabIsActive = true;
            tabMessageReceived = false;
            ParentTab.ImageKey = "channel_default";
            if (this.ParentForm != null) {
                this.ParentForm.ActiveControl = txtNewMessage;
            }
        }

        void parentTab_GotFocus(object sender, EventArgs e) {
            if (this.ParentForm != null) {
                this.ParentForm.ActiveControl = txtNewMessage;
            }
        }
        #endregion

        #region Unread Message Monitor
        private void tmrUnreadMsgFlash_Tick(object sender, EventArgs e) {
            TabPage tab = this.ParentTab;
            if (tabIsActive == false && tabMessageReceived) {
                if (tab.ImageKey == "channel_default") {
                    tab.ImageKey = "channel_newmessage";
                }
                else {
                    tab.ImageKey = "channel_default";
                }
            }
        }
        #endregion

        #region Send Message
        private void txtNewMessage_KeyDown(object sender, KeyEventArgs e) {
            if (e.Control && e.KeyCode == Keys.Up) {
                // fetch previous sent message
                if (trackedSentMessageIndex - 1 >= 0 && trackedSentMessages.Count > 0) {
                    txtNewMessage.Text = trackedSentMessages[--trackedSentMessageIndex];
                    txtNewMessage.SelectionStart = txtNewMessage.Text.Length;
                    e.Handled = true;
                    return;
                }
                else {
                    // nothing available
                    System.Media.SystemSounds.Beep.Play();
                }
            }
            if (e.Control && e.KeyCode == Keys.Down) {
                // fetch next available message
                if (trackedSentMessageIndex + 1 < trackedSentMessages.Count) {
                    txtNewMessage.Text = trackedSentMessages[++trackedSentMessageIndex];
                    txtNewMessage.SelectionStart = txtNewMessage.Text.Length;
                    e.Handled = true;
                    return;
                }
                else {
                    // nothing available
                    System.Media.SystemSounds.Beep.Play();
                }
            }

            if (e.Control && e.KeyCode == Keys.Enter) {
                // CTRL+Enter, allow to go to next line
            }
            else if (e.KeyCode == Keys.Enter) {
                e.Handled = true;
                e.SuppressKeyPress = true;
                // send msg 
                if (Program.GlobalClient.ConnectionStatus != ConnectionStatusTypes.Connected) {
                    MessageBox.Show("You are not connected to the server!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string msg = txtNewMessage.Text.TrimEnd();
                if (msg.Length > 0) {
                    // strip out last CRLF
                    sendFilteredMessage(msg);
                    trackedSentMessages.Add(msg);
                    trackedSentMessageIndex = trackedSentMessages.Count - 1;
                    txtNewMessage.Text = string.Empty;
                    txtNewMessage.Focus();
                }
            }

            if (e.Control && e.KeyCode == Keys.A) {
                //Ctrl+A
                txtNewMessage.SelectAll();
            }
        }

        private void sendFilteredMessage(string message) {
            // filter for any 'commands' starting with /
            string lowered = message.ToLowerInvariant();
            var currentUser = this.CurrentUser;
            if (lowered.StartsWith("/")) {
                // command
                string[] rawsegments = lowered.Substring(1).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (rawsegments.Length > 0) {
                    string command = rawsegments[0];
                    string[] arguments = rawsegments.Skip(1).ToArray();
                    switch (command) {
                        // filter LOCAL commands
                        case "slap":
                            if (arguments.Length > 0) {
                                Program.GlobalClient.SendCommand(string.Format("/me slaps {0} with a large trout", arguments[0]), this.ChannelName);
                            }
                            else {
                                this.addSystemMessageImpl("/slap <username>");
                            }
                            break;
                        case "takeover":
                            if ((currentUser.Flags & UserFlags.Op) == UserFlags.Op && MessageBox.Show("Are you sure you want to takeover the ops of this channel?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes) {
                                // loop any users except us
                                foreach (var usr in this.ChannelUsers) {
                                    if (usr.UserName == currentUser.UserName)
                                        continue;
                                    Program.GlobalClient.SendCommand("/control deop " + usr.UserName, this.ChannelName);
                                }
                            }
                            break;

                        case "cuddle": // lol
                        case "democracy":
                        case "love":
                            if ((currentUser.Flags & UserFlags.Op) == UserFlags.Op && MessageBox.Show("Are you sure you want to distribute the ops to this channel?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes) {
                                // loop any users except us
                                foreach (var usr in this.ChannelUsers) {
                                    if (usr.UserName == currentUser.UserName)
                                        continue;
                                    Program.GlobalClient.SendCommand("/control op " + usr.UserName, this.ChannelName);
                                }
                            }
                            break;
                        default:
                            // bubble to server if neither command
                            Program.GlobalClient.SendCommand(message, this.ChannelName);
                            break;
                    }
                }
            }
            else {
                // send text
                Program.GlobalClient.SendCommand(string.Format("/msg {0} {1}", this.ChannelName, message), this.ChannelName);
            }

        }
        #endregion

        #region Browser Menu handlers
        private void copyToolStripMenuItem_Click(object sender, EventArgs e) {
            IHTMLDocument2 htmlDocument = wbChat.Document.DomDocument as IHTMLDocument2;

            IHTMLSelectionObject currentSelection = htmlDocument.selection;

            if (currentSelection != null) {
                IHTMLTxtRange range = currentSelection.createRange() as IHTMLTxtRange;

                if (range != null && !string.IsNullOrEmpty(range.text)) {
                    Clipboard.SetDataObject(range.text, true, 10, 100);
                }
            }
        }

        private void quoteToolStripMenuItem_Click(object sender, EventArgs e) {
            IHTMLDocument2 htmlDocument = wbChat.Document.DomDocument as IHTMLDocument2;

            IHTMLSelectionObject currentSelection = htmlDocument.selection;

            if (currentSelection != null) {
                IHTMLTxtRange range = currentSelection.createRange() as IHTMLTxtRange;

                if (range != null && !string.IsNullOrEmpty(range.text)) {
                    txtNewMessage.Text += ">" + range.text + Environment.NewLine;
                    txtNewMessage.Focus();
                    txtNewMessage.SelectionLength = 0;
                    txtNewMessage.SelectionStart = txtNewMessage.Text.Length;
                }
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e) {
            wbChat.Document.ExecCommand("SelectAll", true, null);
        }

        private void clearHistoryToolStripMenuItem_Click(object sender, EventArgs e) {
            HtmlElement messageList = wbChat.Document.GetElementById("messageList");
            if (messageList != null) {
                messageList.InnerHtml = "";
            }
        }

        private void ctxBrowserMenu_Opening(object sender, CancelEventArgs e) {
            IHTMLDocument2 htmlDocument = wbChat.Document.DomDocument as IHTMLDocument2;

            copyToolStripMenuItem.Enabled = htmlDocument.selection != null && htmlDocument.selection.createRange() != null && !string.IsNullOrEmpty(htmlDocument.selection.createRange().text);
            quoteToolStripMenuItem.Enabled = copyToolStripMenuItem.Enabled;
        }

        #endregion

        #region User Context Menu
        private void ctxUserMenu_Opening(object sender, CancelEventArgs e) {
            var selectedUser = this.SelectedUser;
            var currentUser = this.ChannelUsers.FirstOrDefault(u => u.UserName == Program.GlobalClient.Name);

            if (selectedUser == null) {
                e.Cancel = true;
                return;
            }

            // set some stuff
            usernameChatVersionToolStripMenuItem.Text = string.Format("{0} ({1})", selectedUser.UserName, selectedUser.ClientInfo);
            timeToolStripMenuItem.Text = string.Format("Local time: {0} Idle for: {1}", selectedUser.LocalTime != null ? selectedUser.LocalTime.Value.ToShortTimeString() : "unknown", selectedUser.IdleTime != null ? selectedUser.IdleTime.Value.ToString() : "unknown");
            timeToolStripMenuItem.Visible = true;

            // if op, display additional menu options
            if ((currentUser.Flags & UserFlags.Op) == UserFlags.Op) {
                toolStripSeparatorOp.Visible = true;
                opDeOpToolStripMenuItem.Visible = true;
                opKickToolStripMenuItem.Visible = true;
                opOpToolStripMenuItem.Visible = true;
            }
            else {
                toolStripSeparatorOp.Visible = false;
                opDeOpToolStripMenuItem.Visible = false;
                opKickToolStripMenuItem.Visible = false;
                opOpToolStripMenuItem.Visible = false;
            }

        }

        private void opKickToolStripMenuItem_Click(object sender, EventArgs e) {
            var selectedUser = this.SelectedUser;
            var currentUser = this.ChannelUsers.FirstOrDefault(u => u.UserName == Program.GlobalClient.Name);
            if (selectedUser == null || currentUser == null)
                return;
            if (Program.GlobalClient.ConnectionStatus == ConnectionStatusTypes.Connected) {
                Program.GlobalClient.SendCommand("/control kick " + selectedUser.UserName, this.ChannelName);
            }
            else {
                MessageBox.Show("You are not connected to the server!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void opDeOpToolStripMenuItem_Click(object sender, EventArgs e) {
            var selectedUser = this.SelectedUser;
            var currentUser = this.ChannelUsers.FirstOrDefault(u => u.UserName == Program.GlobalClient.Name);
            if (selectedUser == null || currentUser == null)
                return;
            if (Program.GlobalClient.ConnectionStatus == ConnectionStatusTypes.Connected) {
                Program.GlobalClient.SendCommand("/control deop " + selectedUser.UserName, this.ChannelName);
            }
            else {
                MessageBox.Show("You are not connected to the server!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void opOpToolStripMenuItem_Click(object sender, EventArgs e) {
            var selectedUser = this.SelectedUser;
            var currentUser = this.ChannelUsers.FirstOrDefault(u => u.UserName == Program.GlobalClient.Name);
            if (selectedUser == null || currentUser == null)
                return;
            if (Program.GlobalClient.ConnectionStatus == ConnectionStatusTypes.Connected) {
                Program.GlobalClient.SendCommand("/control op " + selectedUser.UserName, this.ChannelName);
            }
            else {
                MessageBox.Show("You are not connected to the server!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void doPrivateChat(LocalUserInfo selectedUser) {
            if (Program.GlobalClient.ConnectionStatus == ConnectionStatusTypes.Connected) {
                MainForm frmMain = (MainForm)this.ParentForm;
                var tab = frmMain.getPrivateTabByName(selectedUser.UserName);
                if (tab == null) {
                    // create new tab
                    TabPage tabPage = new TabPage();
                    tabPage.Padding = new Padding(10);
                    tabPage.UseVisualStyleBackColor = true;

                    Crypton.AvChat.Gui.Tabs.ChannelTab.LocalUserInfo inf = new ChannelTab.LocalUserInfo();
                    inf.UserName = selectedUser.UserName;
                    inf.UserID = selectedUser.UserID;

                    tab = new Tabs.PrivateTab(inf, tabPage, selectedUser.UserName);
                    tab.Dock = DockStyle.Fill;
                    tabPage.Controls.Add(tab);

                    tabPage.Tag = tab;

                    frmMain.tbRooms.TabPages.Add(tabPage);

                    //tab.Select();

                    frmMain.tbRooms.SelectTab(tabPage);

                    tabPage.ImageKey = "channel_private";
                }
            }
        }

        private void privateChatToolStripMenuItem_Click(object sender, EventArgs e) {
            var selectedUser = this.SelectedUser;
            if (selectedUser == null)
                return;
            doPrivateChat(selectedUser);
        }

        private void sendAlertToolStripItem_Click(object sender, EventArgs e) {
            var selectedUser = this.SelectedUser;
            var currentUser = this.ChannelUsers.FirstOrDefault(u => u.UserName == Program.GlobalClient.Name);
            if (selectedUser == null || currentUser == null)
                return;
            if (Program.GlobalClient.ConnectionStatus == ConnectionStatusTypes.Connected) {
                Program.GlobalClient.SendCommand("/alert " + selectedUser.UserName, this.ChannelName);
            }
            else {
                MessageBox.Show("You are not connected to the server!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void usernameChatVersionToolStripMenuItem_Click(object sender, EventArgs e) {
            var selectedUser = this.SelectedUser;
            var currentUser = this.ChannelUsers.FirstOrDefault(u => u.UserName == Program.GlobalClient.Name);
            if (selectedUser == null || currentUser == null)
                return;

            Win32Impl.OpenUrl(string.Format("http://www.uer.ca/forum_showprofile.asp?posterid={0}", selectedUser.UserID));
        }


        private void lvUsers_MouseDoubleClick(object sender, MouseEventArgs e) {
            // do private chat
            var selectedUser = this.SelectedUser;
            if (selectedUser != null) {
                doPrivateChat(selectedUser);
            }
        }

        #endregion

        private void wbChat_NewWindow(object sender, CancelEventArgs e) {
            e.Cancel = true;
        }

        #region Splitter resizing

        public void OnSplitterSync(int horizontalPosition, int verticalPosition) {
            splitMain.SplitterDistance = horizontalPosition;
            if (verticalPosition > -1) {
                splitContainer1.SplitterDistance = verticalPosition;
            }
        }

        private void splitMain_SplitterMoved(object sender, SplitterEventArgs e) {
            // splitmain is horizontal
            if (formReady) {
                MainForm frmMain = (MainForm)this.ParentForm;
                if (frmMain != null && this.ParentTab != null) {
                    frmMain.SynchronizeSplitterSizes(this.ParentTab, splitMain.SplitterDistance, splitContainer1.SplitterDistance);
                }
            }
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e) {
            if (formReady) {
                MainForm frmMain = (MainForm)this.ParentForm;
                if (frmMain != null && this.ParentTab != null) {
                    frmMain.SynchronizeSplitterSizes(this.ParentTab, splitMain.SplitterDistance, splitContainer1.SplitterDistance);
                }
            }
        }
        #endregion

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e) {
            try {
                Clipboard.SetText(lblTopic.Text);
            }
            catch { }
        }




    }

}
