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
using System.Windows.Shapes;
using Crypton.AvChat.Client.Events;

namespace Crypton.AvChat.Win.AppWindows
{
    /// <summary>
    /// Interaction logic for ChannelListWindow.xaml
    /// </summary>
    public partial class ChannelListWindow : Window
    {
        public ChannelListWindow()
        {
            InitializeComponent();
        }
        
        public static ChannelListWindow Singleton
        {
            get;
            private set;
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Singleton = null;
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            Singleton = this;
        }

        public void ProcessList(ChannelListEventArgs eventArgs)
        {
            ChannelListView.Items.Clear();
            foreach (var chan in eventArgs.Channels)
            {
                ChannelListView.Items.Add(chan);
            }
        }

        private void ChannelListMouseDoubleClicked(object sender, MouseButtonEventArgs e)
        {
            if (ChannelListView.SelectedItem != null)
            {
                ChannelListItem listItem = (ChannelListItem)ChannelListView.SelectedItem;
                ChatDispatcher.Singleton.SendCommand("/join " + listItem.Name);
                this.Close();
            }
        }

        private void JoinButtonClick(object sender, RoutedEventArgs e)
        {
            this.ChannelListMouseDoubleClicked(null, null);
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
