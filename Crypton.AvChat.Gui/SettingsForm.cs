using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Crypton.AvChat.Gui.Properties;
using System.IO;
using System.Diagnostics;
using System.Collections;

namespace Crypton.AvChat.Gui {
    public partial class SettingsForm : Form {
        public SettingsForm() {
            InitializeComponent();
            this.Font = SystemFonts.DialogFont;
            
            // load settings
            this.loadGeneralSettings();
            this.loadNotificationSettings();
            this.loadFontsColors();
            this.loadAutoJoin();
            this.loadWatchwords();
            this.loadWhiteList();
        }

        #region Loading
        private void loadGeneralSettings() {
            chkAutoAway.Checked = Settings.Default.enableAutoAway;
            chkLogHistory.Checked = Settings.Default.enableHistory;
        }
        private void loadNotificationSettings() {
            switch (Notifications.Default.Mode) {
                case NotificationTypes.Always:
                    rbNotifyAlways.Checked = true;
                    break;
                case NotificationTypes.Disabled:
                    rbDisableNotifications.Checked = true;
                    break;
                case NotificationTypes.NotifyInactive:
                    rbNotifyInactive.Checked = true;
                    break;
                case NotificationTypes.NotifyPrivateTabs:
                    rbNotifyPrivateTab.Checked = true;
                    break;
                case NotificationTypes.NotifyWatchwords:
                    rbNotifyWatchwords.Checked = true;
                    break;
            }
            chkFlashToolbar.Checked = Notifications.Default.FlashWindowTaskbar;
            chkEnableSounds.Checked = Notifications.Default.EnableSounds;
            chkEnableDogSounds.Checked = Notifications.Default.EnableDogSounds;
            chkEnableOwnNotifications.Checked = Notifications.Default.EnableOwnSounds;
        }
        private void loadWhiteList() {
            var opsWl = Crypton.AvChat.Gui.Power.OpWhitelist.WhitelistManager.Load();
            foreach (var opChan in opsWl) {
                lbChannelList.Items.Add(opChan);
            }
        }
        private void loadWatchwords() {
            chkEnableWatchwords.Checked = Watchwords.Default.Enabled;
            if (Watchwords.Default.List != null) {
                foreach (string s in Watchwords.Default.List) {
                    lbWatchwords.Items.Add(s);
                }
            }
        }
        private void loadAutoJoin() {
            StringBuilder sbAutoJoin = new StringBuilder();
            foreach (string str in Settings.Default.autoJoinList) {
                sbAutoJoin.AppendLine(str);
            }
            txtAutoJoinList.Text = sbAutoJoin.ToString();
        }
        private void loadFontsColors() {
            btnTextColorChange.BackColor = Settings.Default.userTextColor;
            btnBackColorChange.BackColor = Settings.Default.userBackColor;
            btnFontChange.Font = Settings.Default.userFont;
        }
        #endregion

        #region Saving
        private void saveGeneralSettings() {
            Settings.Default.enableAutoAway = chkAutoAway.Checked;
            Settings.Default.enableHistory = chkLogHistory.Checked;
        }
        private void saveNotificationSettings() {
            if (rbDisableNotifications.Checked)
                Notifications.Default.Mode = NotificationTypes.Disabled;
            if (rbNotifyWatchwords.Checked)
                Notifications.Default.Mode = NotificationTypes.NotifyWatchwords;
            if (rbNotifyPrivateTab.Checked)
                Notifications.Default.Mode = NotificationTypes.NotifyPrivateTabs;
            if (rbNotifyInactive.Checked)
                Notifications.Default.Mode = NotificationTypes.NotifyInactive;
            if (rbNotifyAlways.Checked)
                Notifications.Default.Mode = NotificationTypes.Always;

            Notifications.Default.FlashWindowTaskbar = chkFlashToolbar.Checked;
            Notifications.Default.EnableSounds = chkEnableSounds.Checked;
            Notifications.Default.EnableDogSounds = chkEnableDogSounds.Checked;
            Notifications.Default.EnableOwnSounds = chkEnableOwnNotifications.Checked;
            Notifications.Default.DisableWhenAway = chkDisableWhenAway.Checked;
        }
        private void saveWhiteList() {
            var opsWl = new List<Crypton.AvChat.Gui.Power.OpWhitelist.Channel>();
            foreach (Power.OpWhitelist.Channel chan in lbChannelList.Items) {
                opsWl.Add(chan);
            }
            Crypton.AvChat.Gui.Power.OpWhitelist.WhitelistManager.Save(opsWl);
        }
        private void saveWatchwords() {
            Watchwords.Default.Enabled = chkEnableWatchwords.Checked;
            Watchwords.Default.List = new System.Collections.Specialized.StringCollection();
            foreach (string s in lbWatchwords.Items) {
                Watchwords.Default.List.Add(s);
            }
        }
        private void saveAutoJoin() {
            Settings.Default.autoJoinList.Clear();
            using (StringReader sr = new StringReader(txtAutoJoinList.Text)) {
                string line = null;
                while ((line = sr.ReadLine()) != null) {
                    if (!string.IsNullOrWhiteSpace(line)) {
                        Settings.Default.autoJoinList.Add(line);
                    }
                }
            }
        }
        private void saveFontsColors() {
            Settings.Default.userTextColor = btnTextColorChange.BackColor;
            Settings.Default.userBackColor = btnBackColorChange.BackColor;
            Settings.Default.userFont = btnFontChange.Font;
        }

        private void applyChanges() {
            // send color setting to server
            Program.GlobalClient.SetColor(btnTextColorChange.BackColor.R, btnTextColorChange.BackColor.G, btnTextColorChange.BackColor.B);
            Program.GlobalClient.AutoAway.Enable = chkAutoAway.Checked;

            Settings.Default.Save();
            Notifications.Default.Save();
            Watchwords.Default.Save();
        }
        #endregion


        #region UI
        private void btnOK_Click(object sender, EventArgs e) {

            // save
            this.saveGeneralSettings();
            this.saveFontsColors();
            this.saveAutoJoin();
            this.saveWhiteList();
            this.saveWatchwords();
            this.saveNotificationSettings();

            this.applyChanges();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnTextColorChange_Click(object sender, EventArgs e) {
            cdPickColor.Color = btnTextColorChange.BackColor;
            if (cdPickColor.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                btnTextColorChange.BackColor = cdPickColor.Color;
            }
        }

        private void btnBackColorChange_Click(object sender, EventArgs e) {
            cdPickColor.Color = btnBackColorChange.BackColor;
            if (cdPickColor.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                btnBackColorChange.BackColor = cdPickColor.Color;
            }
        }

        private void btnFontChange_Click(object sender, EventArgs e) {
            fndDgPickFont.Font = btnFontChange.Font;
            if (fndDgPickFont.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                btnFontChange.Font = fndDgPickFont.Font;
            }
        }

        private void btnTextColorReset_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            btnTextColorChange.BackColor = SystemColors.ControlText;
        }

        private void btnOpenHistory_Click(object sender, EventArgs e) {
            string dpath = History.HistoryManager.HistoryFolder;
            Process.Start("explorer.exe", dpath);
        }

        private void btnAddWatchword_Click(object sender, EventArgs e) {
            string word = Microsoft.VisualBasic.Interaction.InputBox("Enter watchword");
            if (!string.IsNullOrWhiteSpace(word)) {
                word = word.Trim();
                var items = lbWatchwords.Items.Cast<string>();
                if (!items.Contains(word)) {
                    lbWatchwords.Items.Add(word);
                }
            }
        }

        private void btnRemoveWatchword_Click(object sender, EventArgs e) {
            if (lbWatchwords.SelectedItem != null) {
                lbWatchwords.Items.Remove(lbWatchwords.SelectedItem);
            }
        }

        #endregion

        #region Op Whitelist
        // add channel
        private void btnAddOpWhitelistChannel_Click(object sender, EventArgs e) {
            // add new channel prompt
            using (Crypton.AvChat.Gui.Power.OpWhitelist.ChannelPrompt wnd = new Power.OpWhitelist.ChannelPrompt()) {
                if (wnd.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                    // add/check chan
                    if (this.lbChannelList.Items.Cast<Crypton.AvChat.Gui.Power.OpWhitelist.Channel>().Any(c => c.ChannelName == wnd.ChannelName)) {
                        MessageBox.Show("Specified channel already exists!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else {
                        var chan = new Crypton.AvChat.Gui.Power.OpWhitelist.Channel();
                        chan.ChannelName = wnd.ChannelName;
                        lbChannelList.Items.Add(chan);
                    }
                }
            }
        }
        // remove channel
        private void btnRemOpWhitelistChannel_Click(object sender, EventArgs e) {
            if (lbChannelList.SelectedItem != null) {
                var chan = (Crypton.AvChat.Gui.Power.OpWhitelist.Channel)lbChannelList.SelectedItem;
                if (MessageBox.Show(string.Format("Remove channel {0}?", chan.ChannelName), Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes) {
                    // remove
                    lbChannelList.Items.Remove(chan);
                }
            }
        }

        private void btnAddOpWhitelistUser_Click(object sender, EventArgs e) {
            if (lbChannelList.SelectedItem != null) {
                var chan = (Crypton.AvChat.Gui.Power.OpWhitelist.Channel)lbChannelList.SelectedItem;
                using (var wnd = new Power.OpWhitelist.UserPrompt()) {
                    if (wnd.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                        // check user
                        if (chan.Users.Any(u => u.Name == wnd.UserName)) {
                            MessageBox.Show("Specified user already exists!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else {
                            // add
                            var user = new Power.OpWhitelist.User();
                            user.Name = wnd.UserName;
                            chan.Users.Add(user);
                            lbUserList.Items.Add(user);
                        }
                    }
                }
            }
        }

        private void btnRemOpWhitelistUser_Click(object sender, EventArgs e) {
            if (lbChannelList.SelectedItem != null) {
                var chan = (Crypton.AvChat.Gui.Power.OpWhitelist.Channel)lbChannelList.SelectedItem;
                if (lbUserList.SelectedItem != null) {
                    var user = (Crypton.AvChat.Gui.Power.OpWhitelist.User)lbUserList.SelectedItem;
                    chan.Users.Remove(user);
                    lbUserList.Items.Remove(user);
                }
            }
        }
        // selected channel state changes
        private void lbChannelList_SelectedIndexChanged(object sender, EventArgs e) {
            gbUserList.Enabled = lbChannelList.SelectedItem != null;
            if (lbChannelList.SelectedItem != null) {
                // refresh list of users for selected chan
                var chan = (Crypton.AvChat.Gui.Power.OpWhitelist.Channel)lbChannelList.SelectedItem;
                lbUserList.Items.Clear();
                foreach (var user in chan.Users) {
                    lbUserList.Items.Add(user);
                }
            }
        }
        // selected user of channel state changes (not important)
        private void lbUserList_SelectedIndexChanged(object sender, EventArgs e) {

        }
        #endregion

    }
}
