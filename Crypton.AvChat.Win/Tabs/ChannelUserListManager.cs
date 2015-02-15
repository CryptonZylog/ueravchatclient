using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Crypton.AvChat.Client;
using Crypton.AvChat.Client.Events;

namespace Crypton.AvChat.Win.Tabs
{
    public class ChannelUserListManager
    {

        private class ItemUser
        {
            public string Username
            {
                get;
                set;
            }

            public string RealName
            {
                get;
                set;
            }

            public string ClientVersion
            {
                get;
                set;
            }

            public DateTime? LocalTime
            {
                get;
                set;
            }

            public TimeSpan? IdleTime
            {
                get;
                set;
            }

            public UserFlags Flags
            {
                get;
                set;
            }

            public int UserId
            {
                get;
                set;
            }

            public ItemUser() { }
            public ItemUser(UserListEventArgs.UserListItem source)
            {
                this.UserId = source.UserID;
                this.Username = source.Name;
                this.Flags = source.Flags;
                this.LocalTime = source.LocalTime;
                this.ClientVersion = source.ClientVersion;
            }
            public ItemUser(UserJoinEventArgs.UserData source)
            {
                this.UserId = source.UserID;
                this.Username = source.Name;
                this.Flags = source.Flags;
                this.LocalTime = source.LocalTime;
                this.ClientVersion = source.ClientVersion;
            }
        }

        private ListBox _userList;
        private IChatTab _parentTab;

        public ChannelUserListManager(ListBox userList, IChatTab parentTab)
        {
            this._userList = userList;
            this._parentTab = parentTab;
            userList.ContextMenuOpening += userList_ContextMenuOpening;
        }

        #region Menu Events
        private void usernameClicked(object sender, RoutedEventArgs e)
        {
            ItemUser user = this.getSelectedUser();
            if (user != null)
            {
                Win32.Win32Impl.Open(string.Format("http://www.uer.ca/forum_showprofile.asp?posterid=" + user.UserId));
            }
        }
        private void privateChatClicked(object sender, RoutedEventArgs e)
        {
            ItemUser user = this.getSelectedUser();
            if (user != null)
            {
                var tab = ChatDispatcher.Singleton.GetPrivateTab(user.Username);
                tab.Focus();
            }
        }
        private void sendAlertClicked(object sender, RoutedEventArgs e)
        {
            ItemUser user = this.getSelectedUser();
            if (user != null)
            {
                ChatDispatcher.Singleton.SendCommand(string.Format("/alert {0}", user.Username));
            }
        }
        private void controlOpClicked(object sender, RoutedEventArgs e)
        {
            ItemUser user = this.getSelectedUser();
            if (user != null)
            {
                ChatDispatcher.Singleton.SendCommand(string.Format("/control op {0}", user.Username), _parentTab.TabName);
            }
        }
        private void controlDeopClicked(object sender, RoutedEventArgs e)
        {
            ItemUser user = this.getSelectedUser();
            if (user != null)
            {
                ChatDispatcher.Singleton.SendCommand(string.Format("/control deop {0}", user.Username), _parentTab.TabName);
            }
        }
        private void controlKickClicked(object sender, RoutedEventArgs e)
        {
            ItemUser user = this.getSelectedUser();
            if (user != null)
            {
                ChatDispatcher.Singleton.SendCommand(string.Format("/control kick {0}", user.Username), _parentTab.TabName);
            }
        }
        #endregion

        private ItemUser getSelectedUser()
        {
            if (this._userList.SelectedItem != null)
            {
                return (ItemUser)(this._userList.SelectedItem as ListBoxItem).Tag;
            }
            return null;
        }

        private void userList_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            // generate context menu
            if (this._userList.SelectedItem != null)
            {
                ItemUser selectedUser = (ItemUser)(this._userList.SelectedItem as ListBoxItem).Tag;
                ContextMenu menu = this._userList.ContextMenu;

                menu.Items.Clear();

                MenuItem miUsername = new MenuItem()
                {
                    Header = string.Format("{0} ({1})",
                    selectedUser.Username,
                    selectedUser.ClientVersion)
                };
                MenuItem miTimeInfo = new MenuItem()
                {
                    Header = string.Format("Local time: {0} Idle for: {1}",
                    selectedUser.LocalTime != null ? selectedUser.LocalTime.Value.ToShortTimeString() : "unknown",
                    selectedUser.IdleTime != null ? selectedUser.IdleTime.Value.ToString() : "unknown")
                };
                MenuItem miPrivateChat = new MenuItem() { Header = "Private Chat" };
                MenuItem miSendAlert = new MenuItem() { Header = "Send Alert" };
                MenuItem miControlOp = new MenuItem() { Header = "Op" };
                MenuItem miControlDeop = new MenuItem() { Header = "De-op" };
                MenuItem miControlKick = new MenuItem() { Header = "Kick" };

                miUsername.Click += usernameClicked;
                miPrivateChat.Click += privateChatClicked;
                miControlOp.Click += controlOpClicked;
                miControlDeop.Click += controlDeopClicked;
                miControlKick.Click += controlKickClicked;

                menu.Items.Add(miUsername);
                menu.Items.Add(miTimeInfo);
                menu.Items.Add(new Separator());
                menu.Items.Add(miPrivateChat);
                menu.Items.Add(miSendAlert);
                menu.Items.Add(miControlOp);
                menu.Items.Add(miControlDeop);
                menu.Items.Add(miControlKick);
            }
            else
            {
                e.Handled = true;
            }
        }



        private Grid createUserListItemGrid(ItemUser itemUser)
        {
            Grid grid = new Grid();
            grid.Tag = itemUser;
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new System.Windows.GridLength(32.0) });
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.RowDefinitions.Add(new RowDefinition() { Height = new System.Windows.GridLength(32) });

            // avatar
            Image imgAvatar = new Image();
            imgAvatar.Width = 32;
            imgAvatar.Height = 32;
            imgAvatar.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            imgAvatar.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            imgAvatar.Stretch = System.Windows.Media.Stretch.Uniform;
            imgAvatar.Source = new BitmapImage(new Uri(string.Format("http://www.uer.ca/loadavatar.asp?posterid={0}", itemUser.UserId)));
            System.Windows.Media.RenderOptions.SetBitmapScalingMode(imgAvatar, System.Windows.Media.BitmapScalingMode.HighQuality);

            // username
            Label lblUsername = new Label();
            lblUsername.Content = itemUser.Username;
            lblUsername.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;
            lblUsername.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

            grid.Children.Add(imgAvatar);
            grid.Children.Add(lblUsername);

            Grid.SetColumn(imgAvatar, 0);
            Grid.SetColumn(lblUsername, 1);

            return grid;
        }

        private ListBoxItem GetUserByUsername(string username)
        {
            ListBoxItem foundItem = null;
            foreach (ListBoxItem listBoxItem in this._userList.Items)
            {
                ItemUser user = (ItemUser)listBoxItem.Tag;
                if (user.Username == username)
                    foundItem = listBoxItem;
            }
            return foundItem;
        }

        private void UpdateFlags(UserFlags flags, ListBoxItem listBoxItem)
        {
            Grid userGrid = (Grid)listBoxItem.Content;
            Label lblUsername = (Label)userGrid.Children[1];

            if ((flags & UserFlags.Away) == UserFlags.Away)
            {
                lblUsername.FontStyle = FontStyles.Italic;
            }
            else
            {
                lblUsername.FontStyle = FontStyles.Normal;
            }

            if ((flags & UserFlags.Op) == UserFlags.Op)
            {
                lblUsername.FontWeight = FontWeights.Bold;
            }
            else
            {
                lblUsername.FontWeight = FontWeights.Normal;
            }

            // NOTE! voicing is not implemented, so the flag UI is skipped

        }

        public void ProcessUserList(UserListEventArgs userList)
        {
            this._userList.Items.Clear();
            foreach (var user in userList.Users)
            {
                this.AddUser(new ItemUser(user));
            }
        }

        public void RemoveUser(string username)
        {
            ListBoxItem foundItem = this.GetUserByUsername(username);
            if (foundItem != null)
            {
                this._userList.Items.Remove(foundItem);
            }
        }

        private void AddUser(ItemUser itemUser)
        {
            ListBoxItem listBoxItem = new ListBoxItem();
            Grid grid = this.createUserListItemGrid(itemUser);
            listBoxItem.Margin = new System.Windows.Thickness(0, 2, 0, 2);
            listBoxItem.Content = grid;
            listBoxItem.Tag = grid.Tag;
            this.UpdateFlags(itemUser.Flags, listBoxItem);
            this._userList.Items.Add(listBoxItem);
        }

        public void AddUser(Crypton.AvChat.Client.Events.UserJoinEventArgs.UserData user)
        {
            this.AddUser(new ItemUser(user));
            // find me and if I am op of this channel, auto-op this user
            var myUserItem = this.GetUserByUsername(ChatDispatcher.Singleton.Username);
            if (myUserItem != null)
            {
                ItemUser itemUser = (ItemUser)myUserItem.Tag;
                if ((itemUser.Flags & UserFlags.Op) == UserFlags.Op)
                {
                    if (OpsWhitelistService.Match(this._parentTab.TabName, user.Name))
                    {
                        ChatDispatcher.Singleton.SendCommand("/control op " + user.Name, this._parentTab.TabName);
                    }
                }
            }
        }

        public void TimeListUpdate(IEnumerable<Crypton.AvChat.Client.Events.PingEventArgs.TimeListUserBlock> timeList)
        {
            foreach (var timeListEntry in timeList)
            {
                ListBoxItem listBoxItem = this.GetUserByUsername(timeListEntry.Name);
                if (listBoxItem != null)
                {
                    ItemUser itemUser = (ItemUser)listBoxItem.Tag;
                    itemUser.IdleTime = timeListEntry.IdleTime;
                    itemUser.LocalTime = timeListEntry.LocalTime;
                }
            }
        }

        public void UpdateFlags(string username, UserFlags flags)
        {
            ListBoxItem item = this.GetUserByUsername(username);
            if (item != null)
                this.UpdateFlags(flags, item);
        }

    }
}
