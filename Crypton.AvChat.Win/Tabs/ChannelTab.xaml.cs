using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Crypton.AvChat.Win.ChatModel;

namespace Crypton.AvChat.Win.Tabs
{
    /// <summary>
    /// Interaction logic for ChannelTab.xaml
    /// </summary>
    public partial class ChannelTab : UserControl, IChatTab
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

        public ChannelTab(string channelName)
        {
            this._channelName = channelName;
            InitializeComponent();
            this.channelTopicLabel.Content = channelName;
            this.browserController = new BrowserController(browser, chatTextBuffer);
            this.browserController.OnDocumentLoadCompleted = chatDocumentLoadCompleted;
            this.textEntryProcessor = new TextEntryProcessor(this.entryText, channelName);
            this.UserListManager = new ChannelUserListManager(this.UserList, this);

            if (Properties.Settings.Default.EnableHistory)
            {
                this.historyProvider = History.HistoryManager.Create<History.XmlHistoryProvider>(this.ChatDocument, this._channelName);
                this.historyProvider.BeginHistoryLog();
            }

            this.ChatDocument.Nodes.Add(new ChatStatusMessageNode() { Text = string.Format("*** now talking in {0}", this._channelName) });

            this.Icon = App.Current.CurrentTheme.Images.GetImage(Crypton.AvChat.Win.Themes.ThemeImage.KEY_TAB_CHANNEL, Themes.ThemeImage.ImageTypes.Normal);
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
            if (this.historyProvider != null)
            {
                this.historyProvider.Flush();
                this.historyProvider.EndHistoryLog();
            }
            ChatDispatcher.Singleton.SendCommand("/part " + this._channelName, this._channelName);
        }

        public void CloseTab(bool shuttingDown = false)
        {
            if (this.historyProvider != null)
            {
                this.historyProvider.Flush();
                this.historyProvider.EndHistoryLog();
            }
            if (!shuttingDown)
            {
                ChatDispatcher.Singleton.SendCommand("/part " + this._channelName, this._channelName);
            }
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

        public ChannelUserListManager UserListManager
        {
            get;
            private set;
        }

        public AppWindows.ChatWindow ParentChatWindow
        {
            get;
            set;
        }

        private bool _isIconNotification = false;
        private void swapTabIconNotification(object state)
        {
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                if (_isIconNotification)
                {
                    _isIconNotification = false;
                    this.Icon = App.Current.CurrentTheme.Images.GetImage(Crypton.AvChat.Win.Themes.ThemeImage.KEY_TAB_CHANNEL, Themes.ThemeImage.ImageTypes.Normal);
                }
                else
                {
                    _isIconNotification = true;
                    this.Icon = App.Current.CurrentTheme.Images.GetImage(Crypton.AvChat.Win.Themes.ThemeImage.KEY_TAB_CHANNEL, Themes.ThemeImage.ImageTypes.Notification);
                }
                AppWindows.ChatWindowTabHelper.RefreshTabIconState(this);
            });
        }

        private void chatDocumentLoadCompleted()
        {
            this.channelTopicLabel.Content = this.browserController.TopicText ?? this._channelName;
        }

        public void OnTopicReceived(Client.Events.ChannelTopicEventArgs ev)
        {
            if (!string.IsNullOrEmpty(ev.Topic))
            {
                this.ChatDocument.Nodes.Add(new ChatStatusMessageNode() { Html = string.Format("*** topic is '{0}'", ev.Topic) });
                this.ChatDocument.Nodes.Add(new ChatStatusMessageNode() { Html = string.Format("*** set by {0} on {1}", ev.SetBy, ev.Date) });
            }
            this.ChatDocument.Topic = ev.Topic;
            this.channelTopicLabel.Content = this.browserController.TopicText;
            NotificationService.NotifyUserJoined(this);
        }

        public void OnTopicChanged(Client.Events.ChangeTopicEventArgs ev)
        {
            this.ChatDocument.Nodes.Add(new ChatStatusMessageNode() { Html = string.Format("** {0} changes topic to '{1}'", ev.Topic.UserName, ev.Topic.Topic) });
            this.ChatDocument.Topic = ev.Topic.Topic;
            this.channelTopicLabel.Content = this.browserController.TopicText;
        }

        private void ChannelTabLoaded(object sender, RoutedEventArgs e)
        {
        }

        #region Events from Dispatcher
        public void OnUserQuit(Client.Events.UserQuitEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.UserInfo.Message))
            {
                this.ChatDocument.Nodes.Add(new ChatStatusMessageNode() { Html = string.Format("** {0} has quit AvChat ({1})", e.UserInfo.Name, e.UserInfo.Message) });
            }
            else
            {
                this.ChatDocument.Nodes.Add(new ChatStatusMessageNode() { Html = string.Format("** {0} has quit AvChat", e.UserInfo.Name) });
            }
            this.UserListManager.RemoveUser(e.UserInfo.Name);
        }

        public void OnUserListReceived(Client.Events.UserListEventArgs e)
        {
            this.UserListManager.ProcessUserList(e);
        }

        public void OnUserLeave(Client.Events.UserLeaveEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.UserInfo.Message))
            {
                this.ChatDocument.Nodes.Add(new ChatStatusMessageNode() { Html = string.Format("** {0} has left {1} ({2})", e.UserInfo.Name, e.Name, e.UserInfo.Message) });
            }
            else
            {
                this.ChatDocument.Nodes.Add(new ChatStatusMessageNode() { Html = string.Format("** {0} has left {1}", e.UserInfo.Name, e.Name) });
            }
            this.UserListManager.RemoveUser(e.UserInfo.Name);
        }

        public void OnUserKicked(Client.Events.UserKickEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Reason))
            {
                this.ChatDocument.Nodes.Add(new ChatStatusMessageNode() { Html = string.Format("** {0} has kicked {1} ({2})", e.WhoKicked, e.KickedUser, e.Reason) });
            }
            else
            {
                this.ChatDocument.Nodes.Add(new ChatStatusMessageNode() { Html = string.Format("** {0} has kicked {1}", e.WhoKicked, e.KickedUser) });
            }
            this.UserListManager.RemoveUser(e.KickedUser);
        }

        public void OnUserJoined(Client.Events.UserJoinEventArgs e)
        {
            this.UserListManager.AddUser(e.User);
            NotificationService.NotifyUserJoined(this);
        }

        public void OnUserFlagsChanged(Client.Events.FlagStatusEventArgs e)
        {
            this.UserListManager.UpdateFlags(e.UserName, e.Flags);
        }

        public void OnKickedFromChannel(Client.Events.KickChannelEventArgs e)
        {
            this.ChatDocument.Nodes.Add(new ChatStatusMessageNode() { Html = e.Message });
        }

        public void OnChannelJoined(Client.Events.ChannelJoinEventArgs e)
        {            
        }

        public void OnTimeListUpdate(IEnumerable<Client.Events.PingEventArgs.TimeListUserBlock> timeList)
        {
            this.UserListManager.TimeListUpdate(timeList);
        }

        public void OnMessageReceived(Client.Events.AddMessageEventArgs e)
        {
            bool tabActive = this.ParentChatWindow.IsActiveTab(this);
            bool windowActive = this.ParentChatWindow.IsActiveWindow;

            if (!tabActive)
            {
                this.tmrNotificationSwapImage = new Timer(swapTabIconNotification, null, 0, 500);
            }

            NotificationSourceTypes source = NotificationSourceTypes.Channel;
            if (WatchwordService.Match(e.Text))
            {
                source |= NotificationSourceTypes.Watchword;
            }
            NotificationService.NotifyNewMessage(source, this, e.UserInfo.IsMyself);
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
        #endregion


    }
}
