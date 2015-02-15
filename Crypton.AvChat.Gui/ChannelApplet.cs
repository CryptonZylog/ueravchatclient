using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Crypton.AvChatClient;
using System.Diagnostics;
using System.IO;
using Crypton.AvChat.Gui.Properties;
using System.Globalization;
using System.Runtime.InteropServices;
using Crypton.AvChat.Gui.Media;
using System.Threading;
using mshtml;

namespace Crypton.AvChat.Gui {
    public partial class ChannelApplet : UserControl {

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool FlashWindowEx(ref FLASHWINFO pwfi);



        [StructLayout(LayoutKind.Sequential)]
        public struct FLASHWINFO {
            public UInt32 cbSize;
            public IntPtr hwnd;
            public FLASHW_FLAGS dwFlags;
            public UInt32 uCount;
            public UInt32 dwTimeout;
        }


        public enum FLASHW_FLAGS : uint {
            FLASHW_ALL = 0x00000003,
            FLASHW_CAPTION = 0x00000001,
            FLASHW_STOP = 0,
            FLASHW_TIMER = 0x00000004,
            FLASHW_TIMERNOFG = 0x0000000C,
            FLASHW_TRAY = 0x00000002
        }


        public ChannelInfo Channel {
            get;
            private set;
        }



        [ComVisible(true)]
        public class BrowserProxy {

            private ChannelApplet AppletForm {
                get;
                set;
            }

            public void quoteText(string text) {
                AppletForm.txtNewMessage.Text += ">" + text + Environment.NewLine;
                AppletForm.txtNewMessage.Focus();
                AppletForm.txtNewMessage.SelectionLength = 0;
                AppletForm.txtNewMessage.SelectionStart = AppletForm.txtNewMessage.Text.Length;
            }

            public BrowserProxy(ChannelApplet applet) {
                this.AppletForm = applet;
            }

        }

        private bool tabIsActive = false;
        private bool tabMessageReceived = false;
        private bool enableAutoScroll = true;
        private StreamWriter logHistoryWriter = null;

        public ChannelApplet(ChannelInfo channel, TabPage parentTab) {
            InitializeComponent();

        }

        #region Adding user to list

        /// <summary>
        /// Adds User info to the current list
        /// </summary>
        /// <param name="user"></param>
        public void addUserInfo(ChannelUser user) {
            if (this.InvokeRequired) {
                Action<ChannelUser> dg = new Action<ChannelUser>(addUserInfoImpl);
                this.Invoke(dg, user);
            }
            else {
                this.addUserInfoImpl(user);
            }
        }

        private void addUserInfoImpl(ChannelUser user) {
            lock (dgvUserList.Rows) {
                int rowIndex = dgvUserList.Rows.Add(new DataGridViewRow());

                DataGridViewRow row = dgvUserList.Rows[rowIndex];

                row.Height = 30;
                row.Tag = user;

                DataGridViewImageCell imgAvatar = (DataGridViewImageCell)row.Cells[0];
                imgAvatar.Value = DefaultResources.hourglass;
                downloadAvatarAsync(imgAvatar, user.UserID);

                DataGridViewTextBoxCell tbUsername = (DataGridViewTextBoxCell)row.Cells[1];
                tbUsername.Value = user.Name;

                setUserDisplayStyle(tbUsername, user);
            }
#if(TRACE)
            Trace.TraceInformation("addUserInfoImpl: {0}", user.Name);
#endif
        }

        #endregion
        
        void channel_UserQuit(object sender, UserQuitEventArgs e) {
#if(TRACE)
            Trace.TraceInformation("channel_UserQuit: {0}", e.Name);
#endif
            this.removeUserInfo(e.Name);
            if (e.Disconnected) {
                this.addSystemMessage(string.Format("{0} has quit AvChat ({1})", e.Name, e.Message));
            }
            else {
                if (!string.IsNullOrWhiteSpace(e.Message)) {
                    this.addSystemMessage(string.Format("{0} has left {1} ({2})", e.Name, this.Channel.Name, e.Message));
                }
                else {
                    this.addSystemMessage(string.Format("{0} has left {1}", e.Name, this.Channel.Name));
                }
            }
        }

        #region Adding message to chat window - html implementations
        private string renderMessageHtml(MessageReceivedEventArgs msg) {
            using (StringWriter sw = new StringWriter()) {

                using (System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw)) {

                    // date
                    hw.AddAttribute("title", "Click to quote this message");
                    hw.AddAttribute("onclick", "return quoteText(this);");
                    hw.AddAttribute("class", "date");
                    if (msg is UserMessageReceivedEventArgs && (msg as UserMessageReceivedEventArgs).IsMyMessage) {
                        // pink it
                        hw.AddStyleAttribute("color", "#FF71FF");
                    }
                    hw.RenderBeginTag("span");
                    hw.WriteEncodedText(msg.DateReceived.ToString("HH:mm:ss", new CultureInfo("en-US")));
                    hw.RenderEndTag();

                    hw.Write("&nbsp;");

                    if (msg is UserMessageReceivedEventArgs) {
                        UserMessageReceivedEventArgs usermsg = (UserMessageReceivedEventArgs)msg;
                        // user message

                        if (usermsg.IsUsingMe) {
                            hw.AddAttribute("class", "message");
                            if (usermsg.User != null && !string.IsNullOrEmpty(usermsg.User.Color)) {
                                hw.AddStyleAttribute("color", usermsg.User.Color);
                            }
                            hw.RenderBeginTag("span");
                            hw.Write("*&nbsp;{0}&nbsp;{1}", usermsg.Name, usermsg.Text);
                            hw.RenderEndTag();
                        }
                        else {
                            hw.Write("&lt;");
                            hw.AddAttribute("class", "username");
                            if (usermsg.User != null) {
                                switch (usermsg.User.Gender) {
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
                            }
                            hw.RenderBeginTag("span");
                            hw.WriteEncodedText(usermsg.Name);
                            hw.RenderEndTag();
                            hw.Write("&gt;");

                            hw.Write("&nbsp;");
                            hw.AddAttribute("class", "message");
                            if (usermsg.User != null && !string.IsNullOrEmpty(usermsg.User.Color)) {
                                hw.AddStyleAttribute("color", usermsg.User.Color);
                            }
                            hw.RenderBeginTag("span");
                            hw.Write(usermsg.Text.Replace("&apos;", "&#39;"));
                            hw.RenderEndTag();
                        }
                    }
                    else {
                        // server message
                        // display as green text
                        hw.AddAttribute("class", "message");
                        hw.AddStyleAttribute("color", "#74FF65");
                        hw.RenderBeginTag("span");
                        hw.Write(msg.Text);
                        hw.RenderEndTag();
                    }

                }

                return sw.ToString();
            }
        }

        private void addSystemMessage(string message) {
            if (this.InvokeRequired) {
                Action<string> dg = new Action<string>(addSystemMessageImpl);
                this.Invoke(dg, message);
            }
            else {
                addSystemMessageImpl(message);
            }
        }

        private void addSystemMessageImpl(string message) {
            HtmlElement ulMessageNode = wbChat.Document.GetElementById("messageList");
            if (ulMessageNode != null) {
                HtmlElement elem = wbChat.Document.CreateElement("li");
                elem.SetAttribute("class", "userMessage");

                using (StringWriter sw = new StringWriter()) {
                    using (System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw)) {
                        // date
                        hw.AddAttribute("class", "date");
                        hw.RenderBeginTag("span");
                        hw.WriteEncodedText(DateTime.Now.ToString("HH:mm:ss", new CultureInfo("en-US")));
                        hw.RenderEndTag();

                        hw.Write("&nbsp;");

                        hw.AddAttribute("class", "message");
                        hw.AddStyleAttribute("color", "#74FF65");
                        hw.RenderBeginTag("span");
                        hw.Write("***&nbsp;" + message);
                        hw.RenderEndTag();
                    }
                    elem.InnerHtml = sw.ToString();
                }

                ulMessageNode.AppendChild(elem);
            }

            if (Settings.Default.enableHistory && this.logHistoryWriter != null) {
                lock (this.logHistoryWriter) {
                    var html = wbChat.Document.GetElementById("html");
                    this.logHistoryWriter.BaseStream.Position = 0;
                    this.logHistoryWriter.Write(html.InnerHtml);
                    this.logHistoryWriter.Flush();
                }
            }
        }

        private void addUserMessageReceivedImpl(MessageReceivedEventArgs e) {
            if (tabIsActive == false) {
                tabMessageReceived = true;
            }

            HtmlElement ulMessageNode = wbChat.Document.GetElementById("messageList");
            HtmlElement elem = wbChat.Document.CreateElement("li");
            if (ulMessageNode != null) {
                elem.SetAttribute("class", "userMessage");
                elem.InnerHtml = renderMessageHtml(e);
                ulMessageNode.AppendChild(elem);
            }

            if (Settings.Default.enableHistory) {
                if (this.logHistoryWriter == null) {
                    this.logHistoryWriter = History.HistoryManager.CreateChannelLog(this.Channel.Name, DateTime.Now);
                }
                lock (this.logHistoryWriter) {
                    var html = wbChat.Document.GetElementById("html");
                    this.logHistoryWriter.BaseStream.Position = 0;
                    this.logHistoryWriter.Write(html.InnerHtml);
                    this.logHistoryWriter.Flush();
                }
            }

            if (enableAutoScroll) {
                elem.ScrollIntoView(true);
            }

            if (GetForegroundWindow() != this.ParentForm.Handle) {
                // flash taskbar
                FLASHWINFO fInfo = new FLASHWINFO();

                fInfo.cbSize = Convert.ToUInt32(Marshal.SizeOf(fInfo));
                fInfo.hwnd = this.ParentForm.Handle;
                fInfo.dwFlags = FLASHW_FLAGS.FLASHW_ALL | FLASHW_FLAGS.FLASHW_TIMERNOFG;
                fInfo.uCount = 4;
                fInfo.dwTimeout = 0;

                FlashWindowEx(ref fInfo);
            }
        }
        #endregion

        void channel_UserJoin(object sender, UserJoinEventArgs e) {
#if(TRACE)
            Trace.TraceInformation("channel_UserJoin: {0}", e.User.Name);
#endif
            this.addUserInfo(e.User);
            this.addSystemMessage(string.Format("{0} has joined {1}", e.User.Name, this.Channel.Name));
        }


        

    }
}
