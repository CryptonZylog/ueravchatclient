using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Crypton.AvChat.Client;
using System.Diagnostics;
using Crypton.AvChat.Client.Events;

namespace Crypton.AvChat.Gui {
    public partial class ChannelList : Form {
        public ChannelList() {
            InitializeComponent();

            this.Font = SystemFonts.DialogFont;

            Program.GlobalClient.Events.ChannelListReceived += new EventHandler<ChannelListEventArgs>(GlobalClient_ChannelListReceived);
        }

        void GlobalClient_ChannelListReceived(object sender, ChannelListEventArgs e) {
            // process channel list
            foreach (var item in e.Channels) {
                this.addChannelListItem(item);
            }
            this.finishLoad();
        }

        private void ChannelList_Shown(object sender, EventArgs e) {
            if (Program.GlobalClient.ConnectionStatus == ConnectionStatusTypes.Connected) {
                this.beginChannelListLoad();
            }
            else {
                MessageBox.Show("You are not connected to the server!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
        }

        private void beginChannelListLoad() {
            this.UseWaitCursor = true;
            lvChannels.Enabled = false;
            Program.GlobalClient.SendCommand("/list");
        }

        private void finishLoad() {
            if (this.InvokeRequired) {
                Action dg = new Action(finaliseListImpl);
                this.Invoke(dg);
            }
            else {
                finaliseListImpl();
            }
        }

        private void finaliseListImpl() {
            lvChannels.Refresh();

            this.UseWaitCursor = false;
            lvChannels.Enabled = true;
        }

        private void addChannelListItem(ChannelListItem item) {
            if (this.InvokeRequired) {
                Action<ChannelListItem> dg = new Action<ChannelListItem>(addChannelListItemImpl);
                this.Invoke(dg, item);
            }
            else {
                addChannelListItemImpl(item);
            }
        }

        private void addChannelListItemImpl(ChannelListItem item) {
            ListViewItem lvItem = new ListViewItem();

            lvItem.Tag = item;

            lvItem.Text = item.Name;

            lvItem.SubItems.Add(item.UserCount.ToString());
            lvItem.SubItems.Add(item.Topic);

            lvChannels.Items.Add(lvItem);
        }

        private void ctxMenuChannelListItem_Opening(object sender, CancelEventArgs e) {
            // check to make sure an item is selected
            ListViewItem item = lvChannels.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
            if (item != null) {
                joinChannelToolStripMenuItem.Enabled = true;
                copyToolStripMenuItem.Enabled = true;
            }
            else {
                joinChannelToolStripMenuItem.Enabled = false;
                copyToolStripMenuItem.Enabled = false;
            }
        }

        private void lvChannels_MouseDoubleClick(object sender, MouseEventArgs e) {
            // find item at X,Y and if successful, join the channel
            ListViewItem item = lvChannels.GetItemAt(e.X, e.Y);
            if (item != null) {
                joinChannelImpl((ChannelListItem)item.Tag);
            }
        }

        /// <summary>
        /// Gets the selected channel to join
        /// </summary>
        public ChannelListItem SelectedChannel {
            get;
            private set;
        }

        private void joinChannelImpl(ChannelListItem channelItem) {
            this.SelectedChannel = channelItem;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void joinChannelToolStripMenuItem_Click(object sender, EventArgs e) {
            ListViewItem item = lvChannels.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
            if (item != null) {
                joinChannelImpl((ChannelListItem)item.Tag);
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e) {
            ListViewItem item = lvChannels.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
            if (item != null) {
                ChannelListItem channelInfo = (ChannelListItem)item.Tag;

                Clipboard.SetText(channelInfo.ToString());
            }
        }

        private void ChannelList_FormClosed(object sender, FormClosedEventArgs e) {
            Program.GlobalClient.Events.ChannelListReceived -= GlobalClient_ChannelListReceived;
        }


    }
}
