using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Crypton.AvChat.Win.AppWindows;
using Crypton.AvChat.Win.Tabs;
using Crypton.AvChat.Win.Themes;
using System.Windows.Media.Animation;

namespace Crypton.AvChat.Win.AppWindows
{
    class ChatWindowTabHelper
    {

        private TabControl _tabControl = null;
        private ChatWindow _parentWindow = null;

        private class MoveWindowWrapper
        {
            public ChatWindow TargetWindow
            {
                get;
                private set;
            }

            public IChatTab TargetTab
            {
                get;
                private set;
            }

            public MoveWindowWrapper(ChatWindow targetWindow, IChatTab targetTab)
            {
                this.TargetTab = targetTab;
                this.TargetWindow = targetWindow;
            }
        }

        public ChatWindowTabHelper(TabControl tabControl, ChatWindow parentWindow)
        {
            this._tabControl = tabControl;
            this._parentWindow = parentWindow;
        }

        private void moveToWindowHandler(object sender, RoutedEventArgs e)
        {
            MoveWindowWrapper wrapper = (e.Source as MenuItem).Tag as MoveWindowWrapper;
            if (wrapper.TargetWindow != null)
            {
                this._parentWindow.DetachTab(wrapper.TargetTab);
                wrapper.TargetWindow.RegisterTab(wrapper.TargetTab);
            }
        }

        private void headerContextMenuOpened(object sender, RoutedEventArgs e)
        {
            // populate current list of windows, except for this one
            ContextMenu menuSource = (ContextMenu)e.Source;
            MenuItem moveToWindow = menuSource.Items.Cast<object>().Where(o => o is MenuItem).Cast<MenuItem>().FirstOrDefault(mi => mi.Name == "moveToWindow");
            if (moveToWindow != null)
            {
                moveToWindow.Items.Clear();
                foreach (ChatWindow wnd in App.Current.Windows.Cast<Window>().Where(w => w is ChatWindow && w != this._parentWindow))
                {
                    MenuItem mnuClickWindow = new MenuItem();
                    mnuClickWindow.Tag = new MoveWindowWrapper(wnd, menuSource.Tag as IChatTab);
                    mnuClickWindow.Header = wnd.Title;
                    mnuClickWindow.Click += moveToWindowHandler;
                    moveToWindow.Items.Add(mnuClickWindow);
                }
                if (moveToWindow.Items.Count == 0)
                {
                    moveToWindow.IsEnabled = false;
                }
                else
                {
                    moveToWindow.IsEnabled = true;
                }
            }
        }

        private void headerNewWindowClicked(object sender, RoutedEventArgs e)
        {
            // move to new window
            MenuItem mi = (MenuItem)e.Source;
            IChatTab tab = (IChatTab)mi.Tag;

            ChatWindow newWnd = new ChatWindow();
            newWnd.Show();
            newWnd.RegisterTab(tab);
            this._parentWindow.DetachTab(tab);
        }

        private void headerCloseOtherTabsClicked(object sender, RoutedEventArgs e)
        {

        }

        private void headerCloseTabClicked(object sender, RoutedEventArgs e)
        {

        }

        private ContextMenu createHeaderContextMenu(IChatTab chatTab)
        {
            ContextMenu menu = new ContextMenu();
            menu.Tag = chatTab;

            MenuItem miNewWindow = new MenuItem() { Header = "Move to New Window", Tag = chatTab };
            MenuItem miMoveToWindow = new MenuItem() { Header = "Move to window...", Tag = chatTab, Name = "moveToWindow" };
            MenuItem miCloseOtherTabs = new MenuItem() { Header = "Close other tabs", Tag = chatTab };
            MenuItem miCloseTab = new MenuItem() { Header = "Close tab", Tag = chatTab };

            // handlers
            menu.Opened += this.headerContextMenuOpened;
            miNewWindow.Click += this.headerNewWindowClicked;
            miCloseOtherTabs.Click += this.headerCloseOtherTabsClicked;
            miCloseTab.Click += this.headerCloseTabClicked;

            menu.Items.Add(miNewWindow);
            menu.Items.Add(miMoveToWindow);
            menu.Items.Add(new Separator());
            menu.Items.Add(miCloseOtherTabs);
            menu.Items.Add(miCloseTab);
            return menu;
        }

        #region Tab Close icon image handlers
        private void closeButtonMouseEnter(object sender, MouseEventArgs e)
        {
            ImageSource replaceImage = App.Current.CurrentTheme.Images.GetImage(ThemeImage.KEY_TAB_CLOSE, ThemeImage.ImageTypes.MouseOver);
            if (replaceImage != null)
            {
                Image img = sender as Image;
                img.Width = replaceImage.Width;
                img.Height = replaceImage.Height;
                img.Source = replaceImage;
            }
        }

        private void closeButtonMouseLeave(object sender, MouseEventArgs e)
        {
            ImageSource replaceImage = App.Current.CurrentTheme.Images.GetImage(ThemeImage.KEY_TAB_CLOSE, ThemeImage.ImageTypes.Normal);
            if (replaceImage != null)
            {
                Image img = sender as Image;
                img.Width = replaceImage.Width;
                img.Height = replaceImage.Height;
                img.Source = replaceImage;
            }
        }

        private void closeButtonMouseUp(object sender, MouseButtonEventArgs e)
        {
            ImageSource replaceImage = App.Current.CurrentTheme.Images.GetImage(ThemeImage.KEY_TAB_CLOSE, ThemeImage.ImageTypes.Normal);
            if (replaceImage != null)
            {
                Image img = sender as Image;
                img.Width = replaceImage.Width;
                img.Height = replaceImage.Height;
                img.Source = replaceImage;
            }
        }

        private void closeTabHandler(object sender, MouseButtonEventArgs e)
        {
            ImageSource replaceImage = App.Current.CurrentTheme.Images.GetImage(ThemeImage.KEY_TAB_CLOSE, ThemeImage.ImageTypes.MouseDown);
            if (replaceImage != null)
            {
                Image img = sender as Image;
                img.Width = replaceImage.Width;
                img.Height = replaceImage.Height;
                img.Source = replaceImage;
            }
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // close tab
                IChatTab selfChatTab = (sender as Image).Tag as IChatTab;
                this._parentWindow.CloseTab(selfChatTab);
            }
        }
        #endregion

        private Grid createTabHeader(IChatTab chatTab)
        {
            Grid headerGrid = new Grid();
            headerGrid.ColumnDefinitions.Add(new ColumnDefinition()
            { // channel icon
                Width = new GridLength(20)
            });
            headerGrid.ColumnDefinitions.Add(new ColumnDefinition()); // auto for label
            headerGrid.ColumnDefinitions.Add(new ColumnDefinition()
            { // close button
                Width = new GridLength(20)
            });

            // header items
            Image imgTabIcon = new Image();
            imgTabIcon.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            imgTabIcon.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            ImageSource chatTabIcon = null;
            try
            {
                chatTabIcon = chatTab.Icon;
            }
            catch
            {
                // TODO: log to console
            }
            if (chatTabIcon != null)
            {
                imgTabIcon.Width = chatTabIcon.Width;
                imgTabIcon.Height = chatTabIcon.Height;
                imgTabIcon.Source = chatTabIcon;
            }

            // name of tab
            Label lblTabName = new Label();
            lblTabName.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            lblTabName.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            lblTabName.Content = chatTab.TabName;

            // close button 
            Image imgCloseButton = new Image();
            imgCloseButton.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            imgCloseButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            imgCloseButton.ToolTip = "Close tab";
            imgCloseButton.Tag = chatTab;
            ImageSource closeButtonImage = App.Current.CurrentTheme.Images.GetImage(ThemeImage.KEY_TAB_CLOSE, ThemeImage.ImageTypes.Normal);
            if (closeButtonImage != null)
            {
                imgCloseButton.Width = closeButtonImage.Width;
                imgCloseButton.Height = closeButtonImage.Height;
                imgCloseButton.Source = closeButtonImage;
            }
            imgCloseButton.MouseEnter += closeButtonMouseEnter;
            imgCloseButton.MouseLeave += closeButtonMouseLeave;
            imgCloseButton.MouseDown += closeTabHandler;
            imgCloseButton.MouseUp += closeButtonMouseUp;

            headerGrid.Children.Add(imgTabIcon);
            headerGrid.Children.Add(lblTabName);
            headerGrid.Children.Add(imgCloseButton);
            Grid.SetColumn(imgTabIcon, 0);
            Grid.SetColumn(lblTabName, 1);
            Grid.SetColumn(imgCloseButton, 2);

            headerGrid.ContextMenu = this.createHeaderContextMenu(chatTab);

            return headerGrid;
        }

        public TabItem FindTab(IChatTab chatTab)
        {
            return ChatWindowTabHelper.FindTab(chatTab, this._tabControl);
        }

        public static TabItem FindTab(IChatTab chatTab, TabControl owner)
        {
            foreach (TabItem tabItem in owner.Items)
            {
                if (tabItem.Tag == chatTab)
                {
                    return tabItem;
                }
            }
            return null;
        }

        public TabItem CreateTab(IChatTab chatTab)
        {
            TabItem containerTabItem = new TabItem();
            // do the header
            var header = this.createTabHeader(chatTab);
            containerTabItem.Header = header;
            containerTabItem.Content = chatTab.InstanceOfControl;
            containerTabItem.Tag = chatTab;

            return containerTabItem;
        }

        public static void RefreshTabIconState(IChatTab chatTab)
        {
            TabItem tabItem = FindTab(chatTab, chatTab.ParentChatWindow.ChannelTabs);
            if (tabItem != null)
            {
                Grid headerGrid = (Grid)tabItem.Header;
                Image imgIcon = (Image)headerGrid.Children[0];
                try
                {
                    imgIcon.Source = chatTab.Icon;
                }
                catch { }
            }
        }

    }
}
