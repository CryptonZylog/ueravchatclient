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

namespace Crypton.AvChat.Win.SettingTabs
{
    /// <summary>
    /// Interaction logic for OpWhitelistSettings.xaml
    /// </summary>
    public partial class OpWhitelistSettings : UserControl, ISettingsTab
    {
        public OpWhitelistSettings()
        {
            InitializeComponent();
        }

        public string TabName
        {
            get { return "Op Whitelist"; }
        }

        public void LoadSettings()
        {
            var channelList = OpsWhitelistService.GetChannelList();
            foreach (var chan in channelList)
            {
                ListBoxItem listItem = new ListBoxItem();
                listItem.Content = chan.ChannelName;
                listItem.Tag = chan;
                ChannelsListBox.Items.Add(listItem);
            }
        }

        public void SaveSettings()
        {
            List<OpsWhitelistService.Channel> channels = new List<OpsWhitelistService.Channel>();
            foreach (ListBoxItem listItem in ChannelsListBox.Items)
            {
                channels.Add(listItem.Tag as OpsWhitelistService.Channel);
            }

            OpsWhitelistService.Save(channels);
        }

        private void UpdateChannelSelection(object sender, SelectionChangedEventArgs e)
        {
            if (ChannelsListBox.SelectedItem != null)
            {
                OpsWhitelistService.Channel channel = (OpsWhitelistService.Channel)(ChannelsListBox.SelectedItem as ListBoxItem).Tag;
                UsersListBox.Items.Clear();
                foreach (var user in channel.Users.OrderBy(u => u.Name))
                {
                    ListBoxItem listItem = new ListBoxItem();
                    listItem.Content = user.Name;
                    listItem.Tag = user;
                    UsersListBox.Items.Add(listItem);
                }
            }
        }

        private void AddChannel(object sender, RoutedEventArgs e)
        {
            string channelName = null;
            channelName = Microsoft.VisualBasic.Interaction.InputBox("Enter channel name:", "Add channel");
            if (!string.IsNullOrEmpty(channelName))
            {
                if (OpsWhitelistService.GetChannelList().Any(c => c.ChannelName.Contains(channelName)))
                {
                    MessageBox.Show("Channel with the same name has already been added", "Add Channel", MessageBoxButton.OK, MessageBoxImage.Hand);
                }
                else
                {
                    OpsWhitelistService.Channel channel = new OpsWhitelistService.Channel();
                    channel.ChannelName = channelName.Trim();

                    ListBoxItem listItem = new ListBoxItem();
                    listItem.Content = channel.ChannelName;
                    listItem.Tag = channel;
                    ChannelsListBox.Items.Add(listItem);
                }
            }
        }

        private void RemoveChannel(object sender, RoutedEventArgs e)
        {
            if (ChannelsListBox.SelectedItem != null)
            {
                ListBoxItem listItem = (ListBoxItem)ChannelsListBox.SelectedItem;
                OpsWhitelistService.Channel channel = (OpsWhitelistService.Channel)listItem.Tag;
                if (MessageBox.Show("Are you sure you want to remove channel " + channel.ChannelName + "?", "Remove Channel", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    ChannelsListBox.Items.Remove(listItem);
                }
            }
        }

        private void AddUser(object sender, RoutedEventArgs e)
        {
            if (ChannelsListBox.SelectedItem != null)
            {
                ListBoxItem listItem = (ListBoxItem)ChannelsListBox.SelectedItem;
                OpsWhitelistService.Channel channel = (OpsWhitelistService.Channel)listItem.Tag;

                string userName = null;
                userName = Microsoft.VisualBasic.Interaction.InputBox("Enter username:", "Add user");

                if (!string.IsNullOrEmpty(userName))
                {
                    if (channel.Users.Any(u => u.Name.Contains(userName)))
                    {
                        MessageBox.Show("User with the same name has already been added", "Add User", MessageBoxButton.OK, MessageBoxImage.Hand);
                    }
                    else
                    {
                        channel.Users.Add(new OpsWhitelistService.User() { Name = userName });
                        UpdateChannelSelection(null, null);
                    }
                }
            }
        }

        private void RemoveUser(object sender, RoutedEventArgs e)
        {
            if (ChannelsListBox.SelectedItem != null)
            {
                ListBoxItem channelListItem = (ListBoxItem)ChannelsListBox.SelectedItem;
                OpsWhitelistService.Channel channel = (OpsWhitelistService.Channel)channelListItem.Tag;
                if (UsersListBox.SelectedItem != null)
                {
                    ListBoxItem userListItem = (ListBoxItem)UsersListBox.SelectedItem;
                    OpsWhitelistService.User user = (OpsWhitelistService.User)userListItem.Tag;
                    channel.Users.Remove(user);
                    UsersListBox.Items.Remove(userListItem);
                }
            }
        }
    }
}
