using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using Crypton.AvChat.Client.Events;
using Crypton.AvChat.Win.ChatModel;
using Crypton.AvChat.Win.AppWindows;

namespace Crypton.AvChat.Win.Tabs
{
    public interface IChatTab
    {

        /// <summary>
        /// Gets the instance of UserControl, typically just return "this"
        /// </summary>
        UserControl InstanceOfControl
        {
            get;
        }

        /// <summary>
        /// Gets the tab name that is displayed in header
        /// </summary>
        string TabName
        {
            get;
        }

        /// <summary>
        /// Gets the tab icon
        /// </summary>
        ImageSource Icon
        {
            get;
        }

        /// <summary>
        /// Called when the tab is closing
        /// </summary>
        void CloseTab();

        /// <summary>
        /// Gets the chat document
        /// </summary>
        ChatDocument ChatDocument
        {
            get;
        }

        ChatWindow ParentChatWindow
        {
            get;
            set;
        }

        /// <summary>
        /// Called when tab is now active
        /// </summary>
        void Activated();

        void OnTopicReceived(ChannelTopicEventArgs ev);
        void OnTopicChanged(ChangeTopicEventArgs ev);

        void OnUserQuit(UserQuitEventArgs e);
        void OnUserListReceived(UserListEventArgs e);
        void OnUserLeave(UserLeaveEventArgs e);
        void OnUserKicked(UserKickEventArgs e);
        void OnUserJoined(UserJoinEventArgs e);
        void OnUserFlagsChanged(FlagStatusEventArgs e);
        void OnKickedFromChannel(KickChannelEventArgs e);
        void OnChannelJoined(ChannelJoinEventArgs e);
        void OnTimeListUpdate(IEnumerable<Crypton.AvChat.Client.Events.PingEventArgs.TimeListUserBlock> timeList);

        void OnMessageReceived(AddMessageEventArgs e);
    }
}
