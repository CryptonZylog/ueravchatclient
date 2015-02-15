using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Crypton.AvChat.Client.Events;
using System.IO;
using Crypton.AvChat.Gui.Properties;
using Crypton.AvChat.Gui.Media;
using System.Globalization;
using Crypton.AvChat.Client;
using Crypton.AvChat.Gui.Win32;
using System.Runtime.InteropServices;
using mshtml;
using System.Diagnostics;
using System.Web;
using Microsoft.Win32;
using System.Threading;

namespace Crypton.AvChat.Gui.Tabs {
    public partial class PrivateTab : UserControl {
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
        /// Gets the user we are talking to
        /// </summary>
        public Crypton.AvChat.Gui.Tabs.ChannelTab.LocalUserInfo User {
            get;
            private set;
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
        }


        private bool tabIsActive = false;
        private bool tabMessageReceived = false;
        private bool formReady = false;
        private StreamWriter logHistoryWriter = null;

        public PrivateTab(Crypton.AvChat.Gui.Tabs.ChannelTab.LocalUserInfo user, TabPage parentTab, string channelName) {
            InitializeComponent();
            this.ParentTab = parentTab;
            this.User = user;
            this.ChannelName = channelName;
            this.Font = SystemFonts.DialogFont;
            this.ParentTab.Text = channelName;

            // tab events
            parentTab.Enter += new EventHandler(parentTab_Enter);
            parentTab.Leave += new EventHandler(parentTab_Leave);

            this.lblTopic.Text = channelName;

            wbChat.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(wbChat_DocumentCompleted);
            wbChat.DocumentText = generateBrowserBody();
            wbChat.Document.Encoding = "utf-8";
            wbChat.ObjectForScripting = new BrowserProxy(this);

            tmrUnreadMsgFlash.Start();

            if (Settings.Default.enableHistory)
            {
                logHistoryWriter = History.HistoryManager.CreateChannelLog(this.ChannelName, DateTime.Now);
            }
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

        ////////////////////////////////////////

        #region Notification Thread-Safe Methods
        /// <summary>
        /// User message received
        /// </summary>
        /// <param name="e"></param>
        public void OnMessageReceived(AddMessageEventArgs e) {
            this.waitForDocumentReady();
            if (this.tabIsActive == false) {
                this.tabMessageReceived = true;
            }
            bool isWatchword = false, isOwn = false;
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
                        if (Watchwords.Default.List != null && Watchwords.Default.Enabled && Watchwords.Default.List.Cast<string>().Any(l => e.Text.IndexOf(l, StringComparison.InvariantCultureIgnoreCase) > -1)) {
                            isWatchword = true;
                            hw.AddStyleAttribute("font-weight", "bold");
                        }
                        hw.RenderBeginTag("span");
                        hw.Write(e.Text.Replace("&apos;", "&#39;").Replace("&#x9;", "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"));
                        hw.RenderEndTag();
                    }
                    
                }

                // Append to the Document
                this.appendMessageNodeHtml(sw.ToString());

                NotificationHelper.NotifyMessageReceived(tabIsActive, true, isWatchword, isOwn);
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
        
        private void appendMessageNodeHtml(string html) {
            this.waitForDocumentReady();
            bool isVScrollAtBottom = IsVScrollAtBottom;
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

            if (elem != null && isVScrollAtBottom) {
                elem.ScrollIntoView(true);
            }
        }

        void link_Click(object sender, HtmlElementEventArgs e) {
            e.BubbleEvent = false;
            e.ReturnValue = false;

            string url = (sender as HtmlElement).GetAttribute("HREF");

            Win32Impl.OpenUrl(url);
        }

        private void addSystemMessage(string text) {
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
                    hw.WriteEncodedText("*** " + text);
                    hw.RenderEndTag();

                }

                // Append to the Document
                this.appendMessageNodeHtml(sw.ToString());
            }
        }

        #endregion

        ////////////////////////////////////////

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
            this.addSystemMessage("now talking with " + this.ChannelName);
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
            this.ActiveControl = txtNewMessage;
            ParentTab.ImageKey = "channel_private";
        }
        #endregion

        #region Unread Message Monitor
        private void tmrUnreadMsgFlash_Tick(object sender, EventArgs e) {
            TabPage tab = this.ParentTab;
            if (tabIsActive == false && tabMessageReceived) {
                if (tab.ImageKey == "channel_private") {
                    tab.ImageKey = "channel_newmessage";
                }
                else {
                    tab.ImageKey = "channel_private";
                }
            }
        }
        #endregion

        #region Send Message
        private void txtNewMessage_KeyUp(object sender, KeyEventArgs e) {
            if (e.Control && e.KeyCode == Keys.Enter) {
                // CTRL+Enter, allow to go to next line
            }
            else if (e.KeyCode == Keys.Enter) {
                e.Handled = true;
                // send msg 
                if (Program.GlobalClient.ConnectionStatus != ConnectionStatusTypes.Connected) {
                    MessageBox.Show("You are not connected to the server!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string msg = txtNewMessage.Text.TrimEnd();
                if (msg.Length > 0) {
                    // strip out last CRLF
                    sendFilteredMessage(msg);
                    txtNewMessage.Text = string.Empty;
                    txtNewMessage.Focus();
                }
            }
        }

        private void sendFilteredMessage(string message) {
            // filter for any 'commands' starting with /
            string lowered = message.ToLowerInvariant();
            if (lowered.StartsWith("/")) {
                // command
                string[] rawsegments = lowered.Substring(1).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (rawsegments.Length > 0) {
                    string command = rawsegments[0];
                    string[] arguments = rawsegments.Skip(1).ToArray();
                    switch (command) {
                        // filter LOCAL commands
                        default:
                            // bubble to server if neither command
                            Program.GlobalClient.SendCommand(message, this.ChannelName);
                            break;
                    }
                }
            }
            else {
                // send text
                Program.GlobalClient.SendCommand(string.Format("/msg {0} {1}", this.ChannelName, message));
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

        private void wbChat_NewWindow(object sender, CancelEventArgs e) {
            e.Cancel = true;
        }

        #region Splitter resizing

        public void OnSplitterSync(int horizontalPosition) {
            splitMain.SplitterDistance = horizontalPosition;
        }

        private void splitMain_SplitterMoved(object sender, SplitterEventArgs e) {
            // splitmain is horizontal
            if (formReady) {
                MainForm frmMain = (MainForm)this.ParentForm;
                if (frmMain != null && this.ParentTab != null) {
                    frmMain.SynchronizeSplitterSizes(this.ParentTab, splitMain.SplitterDistance, -1);
                }
            }
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e) {
            if (formReady) {
                MainForm frmMain = (MainForm)this.ParentForm;
                if (frmMain != null && this.ParentTab != null) {
                    frmMain.SynchronizeSplitterSizes(this.ParentTab, splitMain.SplitterDistance, -1);
                }
            }
        }
        #endregion


        internal void OnUserQuit(UserQuitEventArgs e) {
            this.addSystemMessage(string.Format("{0} has quit AvChat ({1})", e.UserInfo.Name, e.UserInfo.Message));
        }
    }

}
