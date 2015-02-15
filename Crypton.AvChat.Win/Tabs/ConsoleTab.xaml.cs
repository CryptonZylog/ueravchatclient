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
using Crypton.AvChat.Win.ChatModel;
using Crypton.AvChat.Win.Themes;

namespace Crypton.AvChat.Win.Tabs
{
    /// <summary>
    /// Interaction logic for ConsoleTab.xaml
    /// </summary>
    public partial class ConsoleTab : UserControl, IChatTab
    {

        /// <summary>
        /// Controls rendering of messages in the browser
        /// </summary>
        private BrowserController browserController = null;
        /// <summary>
        /// Contains current textbuffer for the channel text
        /// </summary>
        private ChatDocument chatTextBuffer = new ChatDocument();
        /// <summary>
        /// Handles text input and processing from the user
        /// </summary>
        private TextEntryProcessor textEntryProcessor = null;

        public ConsoleTab()
        {
            InitializeComponent();
            this.browserController = new BrowserController(browser, chatTextBuffer);
            this.textEntryProcessor = new TextEntryProcessor(this.entryText, "--status--");

            this.chatTextBuffer.Nodes.Add(new ChatStatusMessageNode() { Text = "*** Welcome to AvChat" });
        }

        public UserControl InstanceOfControl
        {
            get
            {
                return this;
            }
        }


        public ImageSource Icon
        {
            get
            {
                return App.Current.CurrentTheme.Images.GetImage(ThemeImage.KEY_TAB_CONSOLE, ThemeImage.ImageTypes.Normal);
            }
        }

        public void CloseTab()
        {
            //throw new NotImplementedException();
        }


        public string TabName
        {
            get
            {
                return "Console";
            }
        }

        private void ConsoleTabLoaded(object sender, RoutedEventArgs e)
        {
        }

        private void dumpHtml(object sender, RoutedEventArgs e)
        {
            string html = this.browserController.WindowHtml;
            System.IO.File.WriteAllText("dump.html", html);
            MessageBox.Show("Done");
        }

        public ChatDocument ChatDocument
        {
            get { return this.chatTextBuffer; }
        }



        public void OnTopicReceived(Client.Events.ChannelTopicEventArgs ev)
        {
            throw new NotImplementedException();
        }

        public void OnTopicChanged(Client.Events.ChangeTopicEventArgs ev)
        {
            throw new NotImplementedException();
        }


        public void OnUserQuit(Client.Events.UserQuitEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void OnUserListReceived(Client.Events.UserListEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void OnUserLeave(Client.Events.UserLeaveEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void OnUserKicked(Client.Events.UserKickEventArgs e)
        {
            throw new NotImplementedException();
        }


        public void OnUserJoined(Client.Events.UserJoinEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void OnUserFlagsChanged(Client.Events.FlagStatusEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void OnKickedFromChannel(Client.Events.KickChannelEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void OnChannelJoined(Client.Events.ChannelJoinEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void OnTimeListUpdate(IEnumerable<Client.Events.PingEventArgs.TimeListUserBlock> timeList)
        {
            throw new NotImplementedException();
        }


        public AppWindows.ChatWindow ParentChatWindow
        {
            get;
            set;
        }

        public void Activated()
        {
        }

        public void OnMessageReceived(Client.Events.AddMessageEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
