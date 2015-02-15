using Crypton.AvChat.Client.Net;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

using System.Text;

namespace Crypton.AvChat.Client.Events
{

    /// <summary>
    /// Handles dispatching of chat events coming from the server
    /// </summary>
    public sealed class ChatEvents
    {

        internal ChatEvents() { }

        internal void DispatchPacketEvent(NameValueCollection packet) {
            string action = packet["action"];
            switch (action)
            {
                case "setcol":
                    this.handleColorChangeEvent(packet);
                    break;
                case "dog":
                    this.handleDogEvent(packet);
                    break;
                case "alertbeep":
                    this.handleAlertEvent(packet);
                    break;
                case "topic":
                    this.handleChannelTopicEvent(packet);
                    break;
                case "inchannel":
                    this.handleChannelJoinEvent(packet);
                    break;
                case "chanflags":
                    this.handleChannelFlagsEvent(packet);
                    break;
                case "userlist":
                    this.handleChannelUserList(packet);
                    break;
                case "addtext":
                    this.handleAddTextEvent(packet);
                    break;
                case "addmore":
                    this.handleAddMessageEvent(packet);
                    break;
                case "exitchannel":
                    this.handleExitChannelEvent(packet);
                    break;
                case "userjoin":
                    this.handleUserJoinEvent(packet);
                    break;
                case "userquit":
                    this.handleUserQuitEvent(packet);
                    break;
                case "userpart":
                    this.handleUserLeaveEvent(packet);
                    break;
                case "kickchannel":
                    this.handleKickChannelEvent(packet);
                    break;
                case "quit":
                    this.handleQuitEvent();
                    break;
                case "changetopic":
                    this.handleChannelTopicChangedEvent(packet);
                    break;
                case "flagstatus":
                    this.handleChannelUserFlagsChangedEvent(packet);
                    break;
                case "chanlist":
                    this.handleChannelListEvent(packet);
                    break;
                case "addstat":
                    this.handleAddStatusEvent(packet);
                    break;
                case "userkick":
                    this.handleUserKickEvent(packet);
                    break;
                case "ping":
                    this.handlePingEvent(packet);
                    break;
                default:
                    break;
            }
        }


        #region Actual Dispatching
        private void handlePingEvent(NameValueCollection packet)
        {
            if (this.PingEvent != null)
            {
                PingEventArgs args = new PingEventArgs(packet);
                foreach (EventHandler<PingEventArgs> dg in this.PingEvent.GetInvocationList())
                {
                    dg.BeginInvoke(this, args, null, null);
                }

            }
        }

        private void handleColorChangeEvent(NameValueCollection packet)
        {
            ColorChangeEventArgs args = new ColorChangeEventArgs(packet);
            if (this.UserColorChanged != null)
            {
                foreach (EventHandler<ColorChangeEventArgs> dg in this.UserColorChanged.GetInvocationList())
                {
                    dg.BeginInvoke(this, args, null, null);
                }
            }
        }

        private void handleDogEvent(NameValueCollection packet)
        {
            if (this.PlayDogSound != null)
            {
                PlayDogEventArgs args = new PlayDogEventArgs(packet);
                foreach (EventHandler<PlayDogEventArgs> dg in this.PlayDogSound.GetInvocationList())
                {
                    dg.BeginInvoke(this, args, null, null);
                }
            }
        }

        private void handleDisconnectEvent(NameValueCollection packet)
        {
            if (this.ServerDisconnect != null)
            {
                ServerDisconnectEventArgs args = new ServerDisconnectEventArgs(packet);
                foreach (EventHandler<ServerDisconnectEventArgs> dg in this.ServerDisconnect.GetInvocationList())
                {
                    dg.BeginInvoke(this, args, null, null);
                }
            }
        }

        /// <summary>
        /// Alert has been received
        /// </summary>
        /// <param name="packet"></param>
        private void handleAlertEvent(NameValueCollection packet)
        {
            if (this.AlertReceived != null)
            {
                AlertEventArgs args = new AlertEventArgs(packet);
                foreach (EventHandler<AlertEventArgs> dg in this.AlertReceived.GetInvocationList())
                {
                    dg.BeginInvoke(this, args, null, null);
                }
            }
        }
        /// <summary>
        /// List of channels has been received
        /// </summary>
        /// <param name="packet"></param>
        private void handleChannelListEvent(NameValueCollection packet)
        {
            if (this.ChannelListReceived != null)
            {
                ChannelListEventArgs args = new ChannelListEventArgs(packet);
                foreach (EventHandler<ChannelListEventArgs> dg in this.ChannelListReceived.GetInvocationList())
                {
                    dg.BeginInvoke(this, args, null, null);
                }
            }
        }
        /// <summary>
        /// Occurs when inchannel packet is received
        /// </summary>
        /// <param name="packet"></param>
        private void handleChannelJoinEvent(NameValueCollection packet)
        {
            if (this.ChannelJoined != null)
            {
                ChannelJoinEventArgs args = new ChannelJoinEventArgs(packet);
                foreach (EventHandler<ChannelJoinEventArgs> dg in this.ChannelJoined.GetInvocationList())
                {
                    dg.BeginInvoke(this, args, null, null);
                }
            }
        }
        /// <summary>
        /// Channel topic info has been received
        /// </summary>
        /// <param name="packet"></param>
        private void handleChannelTopicEvent(NameValueCollection packet)
        {
            if (this.ChannelTopicReceived != null)
            {
                ChannelTopicEventArgs args = new ChannelTopicEventArgs(packet);
                foreach (EventHandler<ChannelTopicEventArgs> dg in this.ChannelTopicReceived.GetInvocationList())
                {
                    dg.BeginInvoke(this, args, null, null);
                }
            }
        }
        /// <summary>
        /// Channel Flags Received
        /// </summary>
        /// <param name="packet"></param>
        private void handleChannelFlagsEvent(NameValueCollection packet)
        {
            if (this.ChannelFlagsReceived != null)
            {
                ChannelFlagsEventArgs args = new ChannelFlagsEventArgs(packet);
                foreach (EventHandler<ChannelFlagsEventArgs> dg in this.ChannelFlagsReceived.GetInvocationList())
                {
                    dg.BeginInvoke(this, args, null, null);
                }
            }
        }
        /// <summary>
        /// User list has been received (for channel)
        /// </summary>
        /// <param name="packet"></param>
        private void handleChannelUserList(NameValueCollection packet)
        {
            if (this.UserListReceived != null)
            {
                UserListEventArgs args = new UserListEventArgs(packet);
                foreach (EventHandler<UserListEventArgs> dg in this.UserListReceived.GetInvocationList())
                {
                    dg.BeginInvoke(this, args, null, null);
                }
            }
        }
        /// <summary>
        /// addtext packet received
        /// </summary>
        /// <param name="packet"></param>
        private void handleAddTextEvent(NameValueCollection packet)
        {
            if (this.AddTextEvent != null)
            {
                AddTextEventArgs args = new AddTextEventArgs(packet);
                foreach (EventHandler<AddTextEventArgs> dg in this.AddTextEvent.GetInvocationList())
                {
                    dg.BeginInvoke(this, args, null, null);
                }
            }
        }
        /// <summary>
        /// User message received
        /// </summary>
        /// <param name="packet"></param>
        private void handleAddMessageEvent(NameValueCollection packet)
        {
            if (this.AddMessageEvent != null)
            {
                AddMessageEventArgs args = new AddMessageEventArgs(packet);
                foreach (EventHandler<AddMessageEventArgs> dg in this.AddMessageEvent.GetInvocationList())
                {
                    dg.BeginInvoke(this, args, null, null);
                }
            }
        }
        /// <summary>
        /// exitchannel packet
        /// </summary>
        /// <param name="packet"></param>
        private void handleExitChannelEvent(NameValueCollection packet)
        {
            if (this.ExitChannel != null)
            {
                ExitChannelEventArgs args = new ExitChannelEventArgs(packet);
                foreach (EventHandler<ExitChannelEventArgs> dg in this.ExitChannel.GetInvocationList())
                {
                    dg.BeginInvoke(this, args, null, null);
                }
            }
        }

        /// <summary>
        /// userjoin - join channel
        /// </summary>
        /// <param name="packet"></param>
        private void handleUserJoinEvent(NameValueCollection packet)
        {
            if (this.ChannelUserJoin != null)
            {
                UserJoinEventArgs args = new UserJoinEventArgs(packet);
                foreach (EventHandler<UserJoinEventArgs> dg in this.ChannelUserJoin.GetInvocationList())
                {
                    dg.BeginInvoke(this, args, null, null);
                }
            }
        }
        /// <summary>
        /// userquit
        /// </summary>
        /// <param name="packet"></param>
        private void handleUserQuitEvent(NameValueCollection packet)
        {
            if (this.UserQuit != null)
            {
                UserQuitEventArgs args = new UserQuitEventArgs(packet);
                foreach (EventHandler<UserQuitEventArgs> dg in this.UserQuit.GetInvocationList())
                {
                    dg.BeginInvoke(this, args, null, null);
                }
            }
        }
        /// <summary>
        /// User has left the channel
        /// </summary>
        /// <param name="packet"></param>
        private void handleUserLeaveEvent(NameValueCollection packet)
        {
            if (this.UserLeave != null)
            {
                UserLeaveEventArgs args = new UserLeaveEventArgs(packet);
                foreach (EventHandler<UserLeaveEventArgs> dg in this.UserLeave.GetInvocationList())
                {
                    dg.BeginInvoke(this, args, null, null);
                }
            }
        }
        /// <summary>
        /// kickchannel
        /// </summary>
        /// <param name="packet"></param>
        private void handleKickChannelEvent(NameValueCollection packet)
        {
            if (this.ChannelKick != null)
            {
                KickChannelEventArgs args = new KickChannelEventArgs(packet);
                foreach (EventHandler<KickChannelEventArgs> dg in this.ChannelKick.GetInvocationList())
                {
                    dg.BeginInvoke(this, args, null, null);
                }
            }
        }
        /// <summary>
        /// quit server
        /// </summary>
        private void handleQuitEvent()
        {
            if (this.QuitEvent != null)
            {
                foreach (EventHandler dg in this.QuitEvent.GetInvocationList())
                {
                    dg.BeginInvoke(this, null, null, null);
                }
            }
        }

        /// <summary>
        /// Topic changed
        /// </summary>
        /// <param name="packet"></param>
        private void handleChannelTopicChangedEvent(NameValueCollection packet)
        {
            if (this.ChannelTopicChanged != null)
            {
                ChangeTopicEventArgs args = new ChangeTopicEventArgs(packet);
                foreach (EventHandler<ChangeTopicEventArgs> dg in this.ChannelTopicChanged.GetInvocationList())
                {
                    dg.BeginInvoke(this, args, null, null);
                }
            }
        }
        /// <summary>
        /// User flags update
        /// </summary>
        /// <param name="packet"></param>
        private void handleChannelUserFlagsChangedEvent(NameValueCollection packet)
        {
            if (this.ChannelUserFlagsChanged != null)
            {
                FlagStatusEventArgs args = new FlagStatusEventArgs(packet);
                foreach (EventHandler<FlagStatusEventArgs> dg in this.ChannelUserFlagsChanged.GetInvocationList())
                {
                    dg.BeginInvoke(this, args, null, null);
                }
            }
        }
        /// <summary>
        /// Status update
        /// </summary>
        /// <param name="packet"></param>
        private void handleAddStatusEvent(NameValueCollection packet)
        {
            if (this.AddStatusEvent != null)
            {
                AddStatusEventArgs args = new AddStatusEventArgs(packet);
                foreach (EventHandler<AddStatusEventArgs> dg in this.AddStatusEvent.GetInvocationList())
                {
                    dg.BeginInvoke(this, args, null, null);
                }
            }
        }
        /// <summary>
        /// userkick
        /// </summary>
        /// <param name="packet"></param>
        private void handleUserKickEvent(NameValueCollection packet)
        {
            if (this.UserKicked != null)
            {
                UserKickEventArgs args = new UserKickEventArgs(packet);
                foreach (EventHandler<UserKickEventArgs> dg in this.UserKicked.GetInvocationList())
                {
                    dg.BeginInvoke(this, args, null, null);
                }
            }
        }
        #endregion

        #region Public Events
        /// <summary>
        /// Fires when a dog sound should be played
        /// </summary>
        public event EventHandler<PlayDogEventArgs> PlayDogSound;
        /// <summary>
        /// Fires when you are disconnected from the server by an administrator
        /// </summary>
        public event EventHandler<ServerDisconnectEventArgs> ServerDisconnect;
        /// <summary>
        /// Fires when an alert has been received from someone else
        /// </summary>
        public event EventHandler<AlertEventArgs> AlertReceived;
        /// <summary>
        /// Fires when a list of channels has been received
        /// </summary>
        public event EventHandler<ChannelListEventArgs> ChannelListReceived;
        /// <summary>
        /// Fires when the server changes user's color
        /// </summary>
        public event EventHandler<ColorChangeEventArgs> UserColorChanged;
        /// <summary>
        /// Fires when a channel topic information has been received
        /// </summary>
        public event EventHandler<ChannelTopicEventArgs> ChannelTopicReceived;
        /// <summary>
        /// Fires when a channel join has been successful
        /// </summary>
        public event EventHandler<ChannelJoinEventArgs> ChannelJoined;
        /// <summary>
        /// Fires when channel flags have been received
        /// </summary>
        public event EventHandler<ChannelFlagsEventArgs> ChannelFlagsReceived;
        /// <summary>
        /// Fires when a channel user list has been received
        /// </summary>
        public event EventHandler<UserListEventArgs> UserListReceived;
        /// <summary>
        /// Fires when the server sends text to the client (can be server messages)
        /// </summary>
        public event EventHandler<AddTextEventArgs> AddTextEvent;
        /// <summary>
        /// Fires when a user message has been received
        /// </summary>
        public event EventHandler<AddMessageEventArgs> AddMessageEvent;
        /// <summary>
        /// Fires when the user exists a channel
        /// </summary>
        public event EventHandler<ExitChannelEventArgs> ExitChannel;
        /// <summary>
        /// Fires when the server pings the client, also bringing time list of all users
        /// </summary>
        public event EventHandler<PingEventArgs> PingEvent;
        /// <summary>
        /// Fires when a user has joined a channel
        /// </summary>
        public event EventHandler<UserJoinEventArgs> ChannelUserJoin;
        /// <summary>
        /// Fires when a user has left chat
        /// </summary>
        public event EventHandler<UserQuitEventArgs> UserQuit;
        /// <summary>
        /// Fires when a user left the channel
        /// </summary>
        public event EventHandler<UserLeaveEventArgs> UserLeave;
        /// <summary>
        /// Fires when you have been kicked from a channel
        /// </summary>
        public event EventHandler<KickChannelEventArgs> ChannelKick;
        /// <summary>
        /// Fires after a successful quit command. At this point, the connection is closed
        /// </summary>
        public event EventHandler QuitEvent;
        /// <summary>
        /// Fires when a channel topic changes
        /// </summary>
        public event EventHandler<ChangeTopicEventArgs> ChannelTopicChanged;
        /// <summary>
        /// Fires when user flags have been changed in the channel
        /// </summary>
        public event EventHandler<FlagStatusEventArgs> ChannelUserFlagsChanged;
        /// <summary>
        /// Fires when server sends a status message
        /// </summary>
        public event EventHandler<AddStatusEventArgs> AddStatusEvent;
        /// <summary>
        /// Fires when someone kicks a user
        /// </summary>
        public event EventHandler<UserKickEventArgs> UserKicked;
        #endregion

    }
}
