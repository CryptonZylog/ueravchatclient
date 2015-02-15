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
using Crypton.AvChat.Client;
using Crypton.AvChat.Win.Tabs;
using Crypton.AvChat.Win.Themes;
using System.Windows.Interop;
using Crypton.AvChat.Win.Win32;
using System.Windows.Media.Animation;

namespace Crypton.AvChat.Win.AppWindows
{
    /// <summary>
    /// Interaction logic for ChatWindow.xaml
    /// </summary>
    public partial class ChatWindow : Window
    {

        ChatWindowTabHelper tabHelper = null;

        public ChatWindow()
        {
            InitializeComponent();
            this.tabHelper = new ChatWindowTabHelper(ChannelTabs, this);
        }

        private void moveToWindowHandler(object sender, RoutedEventArgs e)
        {
            MenuItem mnuClickWindow = (e.Source as MenuItem);
            ChatWindow wnd = mnuClickWindow.Tag as ChatWindow;
        }

        /// <summary>
        /// Registers a tab in this window, adding it to the list and optionally selecting it
        /// </summary>
        /// <param name="chatTab"></param>
        /// <param name="selectTab"></param>
        public void RegisterTab(IChatTab chatTab, bool selectTab = false)
        {
            TabItem tab = this.tabHelper.CreateTab(chatTab);

            ChannelTabs.Items.Add(tab);

            chatTab.ParentChatWindow = this;
            if (selectTab || ChannelTabs.Items.Count == 1)
            {
                ChannelTabs.SelectedItem = tab;
            }
        }

        /// <summary>
        /// Calls CloseTab method on the tab and removes it from the list. Will close window if there are no more tabs left
        /// </summary>
        /// <param name="chatTab"></param>
        public void CloseTab(IChatTab chatTab)
        {
            chatTab.CloseTab();
            chatTab.ParentChatWindow = null;
            TabItem tabItem = this.tabHelper.FindTab(chatTab);
            if (tabItem != null)
            {
                ChannelTabs.Items.Remove(tabItem);
                if (ChannelTabs.Items.Count == 0)
                {
                    this.Close();
                }
            }
        }

        /// <summary>
        /// Detaches (removes) tab from the window. Will close window if there are no more tabs left
        /// </summary>
        /// <param name="chatTab"></param>
        public void DetachTab(IChatTab chatTab)
        {
            var tabItem = this.tabHelper.FindTab(chatTab);
            if (tabItem != null)
            {
                ChannelTabs.Items.Remove(tabItem);
                if (ChannelTabs.Items.Count == 0)
                {
                    this.Close(); // don't need an empty window
                }
            }
        }

        /// <summary>
        /// Gets a value indicating that currently selected tab is this one
        /// </summary>
        /// <param name="tab"></param>
        /// <returns></returns>
        public bool IsActiveTab(IChatTab tab)
        {
            if (this.ChannelTabs.SelectedItem != null)
            {
                TabItem tabItem = (TabItem)this.ChannelTabs.SelectedItem;
                return tabItem.Tag == tab;
            }
            return false;
        }

        /// <summary>
        /// Gets value indicating that this chat window is currently in focus
        /// </summary>
        public bool IsActiveWindow
        {
            get
            {
                WindowInteropHelper helper = new WindowInteropHelper(this);
                return Win32Impl.GetForegroundWindow() == helper.Handle;
            }
        }

        /// <summary>
        /// Searches through the current tabs in this window to see if this specific tab is here
        /// </summary>
        /// <typeparam name="TChatTab"></typeparam>
        /// <param name="name">The name of the chat tab</param>
        /// <returns></returns>
        public TChatTab FindTab<TChatTab>(string name) where TChatTab : class, IChatTab
        {
            TabItem tabItem = this.GetTab(name);
            if (tabItem != null)
                return (TChatTab)tabItem.Tag;
            return null;
        }

        private TabItem GetTab(string name)
        {
            if (name == "--status--")
                name = "Console";
            foreach (TabItem tabItem in this.ChannelTabs.Items)
            {
                IChatTab chatTab = (IChatTab)tabItem.Tag;
                if (chatTab.TabName == name)
                    return tabItem;
            }
            return null;
        }
        
        public void OnConnectionStatusChange(ConnectionStatusTypes status)
        {
            //this.Title = status.ToString();
        }

        /// <summary>
        /// Gets all tabs in this window
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IChatTab> GetAllTabs()
        {
            foreach (TabItem tabItem in this.ChannelTabs.Items)
            {
                if (tabItem.Tag is IChatTab)
                    yield return tabItem.Tag as IChatTab;
            }
        }

        private void ChatWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var tabs = this.GetAllTabs();
            // detect if this is the last window (count=1) and if it is, it's a /quit event
            var openChatWindows = App.GetOpenChatWindows();
            // notify each tab
            foreach (var tab in tabs)
            {
                try
                {
                    if (tab is ChannelTab && openChatWindows.Count() == 1)
                        (tab as ChannelTab).CloseTab(true);
                    else
                        tab.CloseTab();
                }
                catch (NotImplementedException) { }
            }
        }

        private void ChannelTabSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl && ChannelTabs.SelectedItem != null)
            {
                // update window text with currently selected tab
                IChatTab tab = ((e.Source as TabControl).SelectedItem as TabItem).Tag as IChatTab;
                this.Title = string.Format("{0} - {1}", ChatDispatcher.Singleton.Username, tab.TabName);
                tab.Activated();
            }
        }

        private void CloseMenuClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        private void OpenPreferences(object sender, RoutedEventArgs e)
        {
            App.Current.OpenSettings();
        }

        private void OpenChannelList(object sender, RoutedEventArgs e)
        {
            ChatDispatcher.Singleton.SendCommand("/list");
        }

        private void OpenLogin(object sender, RoutedEventArgs e)
        {
            App.Current.OpenLogin();
        }


    }
}
