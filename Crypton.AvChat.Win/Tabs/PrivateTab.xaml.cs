using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Media;
using Crypton.AvChat.Win.ChatModel;

namespace Crypton.AvChat.Win.Tabs
{
    /// <summary>
    /// Interaction logic for PrivateTab.xaml
    /// </summary>
    public partial class PrivateTab : UserControl, IChatTab
    {

        private string _channelName = null;
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
        /// <summary>
        /// Manages channel history
        /// </summary>
        private History.IHistoryProvider historyProvider = null;
        /// <summary>
        /// Timer that swaps notification image if new messages are received
        /// </summary>
        private Timer tmrNotificationSwapImage = null;


        public PrivateTab(string userName)
        {
            this._channelName = userName;
            InitializeComponent();
            this.browserController = new BrowserController(browser, chatTextBuffer);
            this.textEntryProcessor = new TextEntryProcessor(this.entryText, this._channelName);

            if (Properties.Settings.Default.EnableHistory)
            {
                this.historyProvider = History.HistoryManager.Create<History.XmlHistoryProvider>(this.ChatDocument, this._channelName);
                this.historyProvider.BeginHistoryLog();
            }

            this.ChatDocument.Nodes.Add(new ChatStatusMessageNode() { Text = string.Format("*** now talking with {0}", this._channelName) });

            this.Icon = App.Current.CurrentTheme.Images.GetImage(Crypton.AvChat.Win.Themes.ThemeImage.KEY_TAB_PRIVATE, Themes.ThemeImage.ImageTypes.Normal);
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
            get;
            private set;
        }

        public void CloseTab()
        {
        }
        
        public string TabName
        {
            get
            {
                return this._channelName;
            }
        }


        public ChatModel.ChatDocument ChatDocument
        {
            get { return this.chatTextBuffer; }
        }

        #region Not implemented in private chan
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
        #endregion
        
        public AppWindows.ChatWindow ParentChatWindow
        {
            get;
            set;
        }

        public void Activated()
        {
            // assume the image is invalid
            if (this.tmrNotificationSwapImage != null)
            {
                this.tmrNotificationSwapImage.Dispose();
                this.tmrNotificationSwapImage = null;
            }
            _isIconNotification = true;
            swapTabIconNotification(null);
        }

        private bool _isIconNotification = false;
        private void swapTabIconNotification(object state)
        {
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                if (_isIconNotification)
                {
                    _isIconNotification = false;
                    this.Icon = App.Current.CurrentTheme.Images.GetImage(Crypton.AvChat.Win.Themes.ThemeImage.KEY_TAB_PRIVATE, Themes.ThemeImage.ImageTypes.Normal);
                }
                else
                {
                    _isIconNotification = true;
                    this.Icon = App.Current.CurrentTheme.Images.GetImage(Crypton.AvChat.Win.Themes.ThemeImage.KEY_TAB_PRIVATE, Themes.ThemeImage.ImageTypes.Notification);
                }
                AppWindows.ChatWindowTabHelper.RefreshTabIconState(this);
            });
        }

        public void OnMessageReceived(Client.Events.AddMessageEventArgs e)
        {
            bool tabActive = this.ParentChatWindow.IsActiveTab(this);
            bool windowActive = this.ParentChatWindow.IsActiveWindow;

            if (!tabActive)
            {
                this.tmrNotificationSwapImage = new Timer(swapTabIconNotification, null, 0, 500);
            }

            NotificationSourceTypes source = NotificationSourceTypes.Private;
            if (WatchwordService.Match(e.Text))
            {
                source |= NotificationSourceTypes.Watchword;
            }
            NotificationService.NotifyNewMessage(source, this, e.UserInfo.IsMyself);
        }
    }
}
