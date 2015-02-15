namespace Crypton.AvChat.Gui {
    partial class SettingsForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnOpenHistory = new System.Windows.Forms.Button();
            this.chkLogHistory = new System.Windows.Forms.CheckBox();
            this.chkAutoAway = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.chkDisableWhenAway = new System.Windows.Forms.CheckBox();
            this.chkEnableSounds = new System.Windows.Forms.CheckBox();
            this.rbNotifyWatchwords = new System.Windows.Forms.RadioButton();
            this.rbNotifyPrivateTab = new System.Windows.Forms.RadioButton();
            this.chkEnableOwnNotifications = new System.Windows.Forms.CheckBox();
            this.chkEnableDogSounds = new System.Windows.Forms.CheckBox();
            this.chkFlashToolbar = new System.Windows.Forms.CheckBox();
            this.rbNotifyAlways = new System.Windows.Forms.RadioButton();
            this.rbNotifyInactive = new System.Windows.Forms.RadioButton();
            this.rbDisableNotifications = new System.Windows.Forms.RadioButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnFontReset = new System.Windows.Forms.LinkLabel();
            this.btnTextColorReset = new System.Windows.Forms.LinkLabel();
            this.btnBackColorReset = new System.Windows.Forms.LinkLabel();
            this.btnBackColorChange = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnFontChange = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnTextColorChange = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.txtAutoJoinList = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.gbUserList = new System.Windows.Forms.GroupBox();
            this.btnRemOpWhitelistUser = new System.Windows.Forms.Button();
            this.btnAddOpWhitelistUser = new System.Windows.Forms.Button();
            this.lbUserList = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRemOpWhitelistChannel = new System.Windows.Forms.Button();
            this.btnAddOpWhitelistChannel = new System.Windows.Forms.Button();
            this.lbChannelList = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.btnRemoveWatchword = new System.Windows.Forms.Button();
            this.btnAddWatchword = new System.Windows.Forms.Button();
            this.lbWatchwords = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.chkEnableWatchwords = new System.Windows.Forms.CheckBox();
            this.cdPickColor = new System.Windows.Forms.ColorDialog();
            this.fndDgPickFont = new System.Windows.Forms.FontDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.gbUserList.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(245, 308);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(86, 28);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(346, 308);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(86, 28);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Location = new System.Drawing.Point(13, 13);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(419, 289);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnOpenHistory);
            this.tabPage1.Controls.Add(this.chkLogHistory);
            this.tabPage1.Controls.Add(this.chkAutoAway);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(411, 263);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnOpenHistory
            // 
            this.btnOpenHistory.Location = new System.Drawing.Point(157, 57);
            this.btnOpenHistory.Name = "btnOpenHistory";
            this.btnOpenHistory.Size = new System.Drawing.Size(75, 23);
            this.btnOpenHistory.TabIndex = 5;
            this.btnOpenHistory.Text = "View...";
            this.btnOpenHistory.UseVisualStyleBackColor = true;
            this.btnOpenHistory.Click += new System.EventHandler(this.btnOpenHistory_Click);
            // 
            // chkLogHistory
            // 
            this.chkLogHistory.AutoSize = true;
            this.chkLogHistory.Location = new System.Drawing.Point(34, 61);
            this.chkLogHistory.Name = "chkLogHistory";
            this.chkLogHistory.Size = new System.Drawing.Size(101, 17);
            this.chkLogHistory.TabIndex = 4;
            this.chkLogHistory.Text = "Log chat history";
            this.toolTip1.SetToolTip(this.chkLogHistory, "Enables chat history log");
            this.chkLogHistory.UseVisualStyleBackColor = true;
            // 
            // chkAutoAway
            // 
            this.chkAutoAway.AutoSize = true;
            this.chkAutoAway.Location = new System.Drawing.Point(34, 26);
            this.chkAutoAway.Name = "chkAutoAway";
            this.chkAutoAway.Size = new System.Drawing.Size(224, 17);
            this.chkAutoAway.TabIndex = 3;
            this.chkAutoAway.Text = "Auto set away after 15 minutes of idle time";
            this.toolTip1.SetToolTip(this.chkAutoAway, "Do not track away status");
            this.chkAutoAway.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.chkDisableWhenAway);
            this.tabPage3.Controls.Add(this.chkEnableSounds);
            this.tabPage3.Controls.Add(this.rbNotifyWatchwords);
            this.tabPage3.Controls.Add(this.rbNotifyPrivateTab);
            this.tabPage3.Controls.Add(this.chkEnableOwnNotifications);
            this.tabPage3.Controls.Add(this.chkEnableDogSounds);
            this.tabPage3.Controls.Add(this.chkFlashToolbar);
            this.tabPage3.Controls.Add(this.rbNotifyAlways);
            this.tabPage3.Controls.Add(this.rbNotifyInactive);
            this.tabPage3.Controls.Add(this.rbDisableNotifications);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(411, 263);
            this.tabPage3.TabIndex = 3;
            this.tabPage3.Text = "Notifications";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // chkDisableWhenAway
            // 
            this.chkDisableWhenAway.AutoSize = true;
            this.chkDisableWhenAway.Location = new System.Drawing.Point(228, 18);
            this.chkDisableWhenAway.Name = "chkDisableWhenAway";
            this.chkDisableWhenAway.Size = new System.Drawing.Size(118, 17);
            this.chkDisableWhenAway.TabIndex = 9;
            this.chkDisableWhenAway.Text = "Disable when away";
            this.chkDisableWhenAway.UseVisualStyleBackColor = true;
            // 
            // chkEnableSounds
            // 
            this.chkEnableSounds.AutoSize = true;
            this.chkEnableSounds.Checked = true;
            this.chkEnableSounds.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnableSounds.Location = new System.Drawing.Point(22, 172);
            this.chkEnableSounds.Name = "chkEnableSounds";
            this.chkEnableSounds.Size = new System.Drawing.Size(96, 17);
            this.chkEnableSounds.TabIndex = 8;
            this.chkEnableSounds.Text = "Enable sounds";
            this.chkEnableSounds.UseVisualStyleBackColor = true;
            // 
            // rbNotifyWatchwords
            // 
            this.rbNotifyWatchwords.AutoSize = true;
            this.rbNotifyWatchwords.Location = new System.Drawing.Point(22, 41);
            this.rbNotifyWatchwords.Name = "rbNotifyWatchwords";
            this.rbNotifyWatchwords.Size = new System.Drawing.Size(149, 17);
            this.rbNotifyWatchwords.TabIndex = 7;
            this.rbNotifyWatchwords.Text = "Notify only on watchwords";
            this.rbNotifyWatchwords.UseVisualStyleBackColor = true;
            // 
            // rbNotifyPrivateTab
            // 
            this.rbNotifyPrivateTab.AutoSize = true;
            this.rbNotifyPrivateTab.Location = new System.Drawing.Point(22, 64);
            this.rbNotifyPrivateTab.Name = "rbNotifyPrivateTab";
            this.rbNotifyPrivateTab.Size = new System.Drawing.Size(132, 17);
            this.rbNotifyPrivateTab.TabIndex = 6;
            this.rbNotifyPrivateTab.Text = "Notify only private tabs";
            this.rbNotifyPrivateTab.UseVisualStyleBackColor = true;
            // 
            // chkEnableOwnNotifications
            // 
            this.chkEnableOwnNotifications.AutoSize = true;
            this.chkEnableOwnNotifications.Checked = true;
            this.chkEnableOwnNotifications.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnableOwnNotifications.Location = new System.Drawing.Point(22, 218);
            this.chkEnableOwnNotifications.Name = "chkEnableOwnNotifications";
            this.chkEnableOwnNotifications.Size = new System.Drawing.Size(207, 17);
            this.chkEnableOwnNotifications.TabIndex = 5;
            this.chkEnableOwnNotifications.Text = "Enable sounds for your own messages";
            this.toolTip1.SetToolTip(this.chkEnableOwnNotifications, "Play message received sound when your own message is posted in chat");
            this.chkEnableOwnNotifications.UseVisualStyleBackColor = true;
            // 
            // chkEnableDogSounds
            // 
            this.chkEnableDogSounds.AutoSize = true;
            this.chkEnableDogSounds.Checked = true;
            this.chkEnableDogSounds.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnableDogSounds.Location = new System.Drawing.Point(22, 195);
            this.chkEnableDogSounds.Name = "chkEnableDogSounds";
            this.chkEnableDogSounds.Size = new System.Drawing.Size(117, 17);
            this.chkEnableDogSounds.TabIndex = 4;
            this.chkEnableDogSounds.Text = "Enable dog sounds";
            this.chkEnableDogSounds.UseVisualStyleBackColor = true;
            // 
            // chkFlashToolbar
            // 
            this.chkFlashToolbar.AutoSize = true;
            this.chkFlashToolbar.Checked = true;
            this.chkFlashToolbar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFlashToolbar.Location = new System.Drawing.Point(22, 149);
            this.chkFlashToolbar.Name = "chkFlashToolbar";
            this.chkFlashToolbar.Size = new System.Drawing.Size(160, 17);
            this.chkFlashToolbar.TabIndex = 3;
            this.chkFlashToolbar.Text = "Flash window && taskbar icon";
            this.chkFlashToolbar.UseVisualStyleBackColor = true;
            // 
            // rbNotifyAlways
            // 
            this.rbNotifyAlways.AutoSize = true;
            this.rbNotifyAlways.Checked = true;
            this.rbNotifyAlways.Location = new System.Drawing.Point(22, 110);
            this.rbNotifyAlways.Name = "rbNotifyAlways";
            this.rbNotifyAlways.Size = new System.Drawing.Size(87, 17);
            this.rbNotifyAlways.TabIndex = 2;
            this.rbNotifyAlways.TabStop = true;
            this.rbNotifyAlways.Text = "Notify always";
            this.rbNotifyAlways.UseVisualStyleBackColor = true;
            // 
            // rbNotifyInactive
            // 
            this.rbNotifyInactive.AutoSize = true;
            this.rbNotifyInactive.Location = new System.Drawing.Point(22, 87);
            this.rbNotifyInactive.Name = "rbNotifyInactive";
            this.rbNotifyInactive.Size = new System.Drawing.Size(188, 17);
            this.rbNotifyInactive.TabIndex = 1;
            this.rbNotifyInactive.Text = "Notify only on inactive window/tab";
            this.rbNotifyInactive.UseVisualStyleBackColor = true;
            // 
            // rbDisableNotifications
            // 
            this.rbDisableNotifications.AutoSize = true;
            this.rbDisableNotifications.Location = new System.Drawing.Point(22, 18);
            this.rbDisableNotifications.Name = "rbDisableNotifications";
            this.rbDisableNotifications.Size = new System.Drawing.Size(119, 17);
            this.rbDisableNotifications.TabIndex = 0;
            this.rbDisableNotifications.Text = "Disable notifications";
            this.rbDisableNotifications.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnFontReset);
            this.tabPage2.Controls.Add(this.btnTextColorReset);
            this.tabPage2.Controls.Add(this.btnBackColorReset);
            this.tabPage2.Controls.Add(this.btnBackColorChange);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.btnFontChange);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.btnTextColorChange);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(411, 263);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Fonts and Colours";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnFontReset
            // 
            this.btnFontReset.AutoSize = true;
            this.btnFontReset.LinkColor = System.Drawing.SystemColors.HotTrack;
            this.btnFontReset.Location = new System.Drawing.Point(227, 126);
            this.btnFontReset.Name = "btnFontReset";
            this.btnFontReset.Size = new System.Drawing.Size(78, 13);
            this.btnFontReset.TabIndex = 8;
            this.btnFontReset.TabStop = true;
            this.btnFontReset.Text = "System Default";
            // 
            // btnTextColorReset
            // 
            this.btnTextColorReset.AutoSize = true;
            this.btnTextColorReset.LinkColor = System.Drawing.SystemColors.HotTrack;
            this.btnTextColorReset.Location = new System.Drawing.Point(227, 36);
            this.btnTextColorReset.Name = "btnTextColorReset";
            this.btnTextColorReset.Size = new System.Drawing.Size(78, 13);
            this.btnTextColorReset.TabIndex = 7;
            this.btnTextColorReset.TabStop = true;
            this.btnTextColorReset.Text = "System Default";
            this.btnTextColorReset.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnTextColorReset_LinkClicked);
            // 
            // btnBackColorReset
            // 
            this.btnBackColorReset.AutoSize = true;
            this.btnBackColorReset.LinkColor = System.Drawing.SystemColors.HotTrack;
            this.btnBackColorReset.Location = new System.Drawing.Point(227, 81);
            this.btnBackColorReset.Name = "btnBackColorReset";
            this.btnBackColorReset.Size = new System.Drawing.Size(78, 13);
            this.btnBackColorReset.TabIndex = 6;
            this.btnBackColorReset.TabStop = true;
            this.btnBackColorReset.Text = "System Default";
            // 
            // btnBackColorChange
            // 
            this.btnBackColorChange.Location = new System.Drawing.Point(162, 71);
            this.btnBackColorChange.Name = "btnBackColorChange";
            this.btnBackColorChange.Size = new System.Drawing.Size(32, 32);
            this.btnBackColorChange.TabIndex = 5;
            this.toolTip1.SetToolTip(this.btnBackColorChange, "Click to change window background");
            this.btnBackColorChange.UseVisualStyleBackColor = true;
            this.btnBackColorChange.Click += new System.EventHandler(this.btnBackColorChange_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Custom background:";
            // 
            // btnFontChange
            // 
            this.btnFontChange.Location = new System.Drawing.Point(108, 121);
            this.btnFontChange.Name = "btnFontChange";
            this.btnFontChange.Size = new System.Drawing.Size(86, 23);
            this.btnFontChange.TabIndex = 3;
            this.btnFontChange.Text = "AaBbCcDd";
            this.toolTip1.SetToolTip(this.btnFontChange, "Click to change chat window font");
            this.btnFontChange.UseVisualStyleBackColor = true;
            this.btnFontChange.Click += new System.EventHandler(this.btnFontChange_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Font:";
            // 
            // btnTextColorChange
            // 
            this.btnTextColorChange.Location = new System.Drawing.Point(162, 26);
            this.btnTextColorChange.Name = "btnTextColorChange";
            this.btnTextColorChange.Size = new System.Drawing.Size(32, 32);
            this.btnTextColorChange.TabIndex = 1;
            this.toolTip1.SetToolTip(this.btnTextColorChange, "Click to change your text color");
            this.btnTextColorChange.UseVisualStyleBackColor = true;
            this.btnTextColorChange.Click += new System.EventHandler(this.btnTextColorChange_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "My text color:";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.txtAutoJoinList);
            this.tabPage4.Controls.Add(this.label3);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(411, 263);
            this.tabPage4.TabIndex = 2;
            this.tabPage4.Text = "Auto-Join";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // txtAutoJoinList
            // 
            this.txtAutoJoinList.Location = new System.Drawing.Point(32, 80);
            this.txtAutoJoinList.Multiline = true;
            this.txtAutoJoinList.Name = "txtAutoJoinList";
            this.txtAutoJoinList.Size = new System.Drawing.Size(333, 94);
            this.txtAutoJoinList.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(243, 26);
            this.label3.TabIndex = 0;
            this.label3.Text = "Join these channels automatically when launching\r\napplication (one per line)";
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.gbUserList);
            this.tabPage5.Controls.Add(this.groupBox1);
            this.tabPage5.Controls.Add(this.label5);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(411, 263);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Ops Whitelist";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // gbUserList
            // 
            this.gbUserList.Controls.Add(this.btnRemOpWhitelistUser);
            this.gbUserList.Controls.Add(this.btnAddOpWhitelistUser);
            this.gbUserList.Controls.Add(this.lbUserList);
            this.gbUserList.Enabled = false;
            this.gbUserList.Location = new System.Drawing.Point(212, 80);
            this.gbUserList.Name = "gbUserList";
            this.gbUserList.Size = new System.Drawing.Size(173, 161);
            this.gbUserList.TabIndex = 2;
            this.gbUserList.TabStop = false;
            this.gbUserList.Text = "Users";
            // 
            // btnRemOpWhitelistUser
            // 
            this.btnRemOpWhitelistUser.Location = new System.Drawing.Point(132, 53);
            this.btnRemOpWhitelistUser.Name = "btnRemOpWhitelistUser";
            this.btnRemOpWhitelistUser.Size = new System.Drawing.Size(35, 28);
            this.btnRemOpWhitelistUser.TabIndex = 4;
            this.btnRemOpWhitelistUser.Text = "-";
            this.btnRemOpWhitelistUser.UseVisualStyleBackColor = true;
            this.btnRemOpWhitelistUser.Click += new System.EventHandler(this.btnRemOpWhitelistUser_Click);
            // 
            // btnAddOpWhitelistUser
            // 
            this.btnAddOpWhitelistUser.Location = new System.Drawing.Point(132, 19);
            this.btnAddOpWhitelistUser.Name = "btnAddOpWhitelistUser";
            this.btnAddOpWhitelistUser.Size = new System.Drawing.Size(35, 28);
            this.btnAddOpWhitelistUser.TabIndex = 3;
            this.btnAddOpWhitelistUser.Text = "+";
            this.btnAddOpWhitelistUser.UseVisualStyleBackColor = true;
            this.btnAddOpWhitelistUser.Click += new System.EventHandler(this.btnAddOpWhitelistUser_Click);
            // 
            // lbUserList
            // 
            this.lbUserList.FormattingEnabled = true;
            this.lbUserList.Location = new System.Drawing.Point(6, 19);
            this.lbUserList.Name = "lbUserList";
            this.lbUserList.Size = new System.Drawing.Size(120, 134);
            this.lbUserList.TabIndex = 1;
            this.lbUserList.SelectedIndexChanged += new System.EventHandler(this.lbUserList_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRemOpWhitelistChannel);
            this.groupBox1.Controls.Add(this.btnAddOpWhitelistChannel);
            this.groupBox1.Controls.Add(this.lbChannelList);
            this.groupBox1.Location = new System.Drawing.Point(21, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(173, 161);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Channel";
            // 
            // btnRemOpWhitelistChannel
            // 
            this.btnRemOpWhitelistChannel.Location = new System.Drawing.Point(132, 53);
            this.btnRemOpWhitelistChannel.Name = "btnRemOpWhitelistChannel";
            this.btnRemOpWhitelistChannel.Size = new System.Drawing.Size(35, 28);
            this.btnRemOpWhitelistChannel.TabIndex = 2;
            this.btnRemOpWhitelistChannel.Text = "-";
            this.btnRemOpWhitelistChannel.UseVisualStyleBackColor = true;
            this.btnRemOpWhitelistChannel.Click += new System.EventHandler(this.btnRemOpWhitelistChannel_Click);
            // 
            // btnAddOpWhitelistChannel
            // 
            this.btnAddOpWhitelistChannel.Location = new System.Drawing.Point(132, 19);
            this.btnAddOpWhitelistChannel.Name = "btnAddOpWhitelistChannel";
            this.btnAddOpWhitelistChannel.Size = new System.Drawing.Size(35, 28);
            this.btnAddOpWhitelistChannel.TabIndex = 1;
            this.btnAddOpWhitelistChannel.Text = "+";
            this.btnAddOpWhitelistChannel.UseVisualStyleBackColor = true;
            this.btnAddOpWhitelistChannel.Click += new System.EventHandler(this.btnAddOpWhitelistChannel_Click);
            // 
            // lbChannelList
            // 
            this.lbChannelList.FormattingEnabled = true;
            this.lbChannelList.Location = new System.Drawing.Point(6, 19);
            this.lbChannelList.Name = "lbChannelList";
            this.lbChannelList.Size = new System.Drawing.Size(120, 134);
            this.lbChannelList.TabIndex = 0;
            this.lbChannelList.SelectedIndexChanged += new System.EventHandler(this.lbChannelList_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(18, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(341, 64);
            this.label5.TabIndex = 0;
            this.label5.Text = resources.GetString("label5.Text");
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.btnRemoveWatchword);
            this.tabPage6.Controls.Add(this.btnAddWatchword);
            this.tabPage6.Controls.Add(this.lbWatchwords);
            this.tabPage6.Controls.Add(this.label6);
            this.tabPage6.Controls.Add(this.chkEnableWatchwords);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(411, 263);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Watchwords";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // btnRemoveWatchword
            // 
            this.btnRemoveWatchword.Location = new System.Drawing.Point(260, 151);
            this.btnRemoveWatchword.Name = "btnRemoveWatchword";
            this.btnRemoveWatchword.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveWatchword.TabIndex = 4;
            this.btnRemoveWatchword.Text = "Remove";
            this.btnRemoveWatchword.UseVisualStyleBackColor = true;
            this.btnRemoveWatchword.Click += new System.EventHandler(this.btnRemoveWatchword_Click);
            // 
            // btnAddWatchword
            // 
            this.btnAddWatchword.Location = new System.Drawing.Point(260, 122);
            this.btnAddWatchword.Name = "btnAddWatchword";
            this.btnAddWatchword.Size = new System.Drawing.Size(75, 23);
            this.btnAddWatchword.TabIndex = 3;
            this.btnAddWatchword.Text = "Add";
            this.btnAddWatchword.UseVisualStyleBackColor = true;
            this.btnAddWatchword.Click += new System.EventHandler(this.btnAddWatchword_Click);
            // 
            // lbWatchwords
            // 
            this.lbWatchwords.FormattingEnabled = true;
            this.lbWatchwords.Location = new System.Drawing.Point(20, 122);
            this.lbWatchwords.Name = "lbWatchwords";
            this.lbWatchwords.Size = new System.Drawing.Size(234, 95);
            this.lbWatchwords.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(17, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(388, 53);
            this.label6.TabIndex = 1;
            this.label6.Text = resources.GetString("label6.Text");
            // 
            // chkEnableWatchwords
            // 
            this.chkEnableWatchwords.AutoSize = true;
            this.chkEnableWatchwords.Location = new System.Drawing.Point(20, 16);
            this.chkEnableWatchwords.Name = "chkEnableWatchwords";
            this.chkEnableWatchwords.Size = new System.Drawing.Size(119, 17);
            this.chkEnableWatchwords.TabIndex = 0;
            this.chkEnableWatchwords.Text = "Enable watchwords";
            this.chkEnableWatchwords.UseVisualStyleBackColor = true;
            // 
            // cdPickColor
            // 
            this.cdPickColor.FullOpen = true;
            // 
            // fndDgPickFont
            // 
            this.fndDgPickFont.Color = System.Drawing.SystemColors.ControlText;
            this.fndDgPickFont.FontMustExist = true;
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 300;
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(445, 346);
            this.ControlBox = false;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SettingsForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Crypton AvChat Settings";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.gbUserList.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.CheckBox chkAutoAway;
        private System.Windows.Forms.CheckBox chkLogHistory;
        private System.Windows.Forms.Button btnOpenHistory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnTextColorChange;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnFontChange;
        private System.Windows.Forms.TextBox txtAutoJoinList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel btnBackColorReset;
        private System.Windows.Forms.Button btnBackColorChange;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel btnTextColorReset;
        private System.Windows.Forms.LinkLabel btnFontReset;
        private System.Windows.Forms.ColorDialog cdPickColor;
        private System.Windows.Forms.FontDialog fndDgPickFont;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.GroupBox gbUserList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox lbUserList;
        private System.Windows.Forms.ListBox lbChannelList;
        private System.Windows.Forms.Button btnRemOpWhitelistUser;
        private System.Windows.Forms.Button btnAddOpWhitelistUser;
        private System.Windows.Forms.Button btnRemOpWhitelistChannel;
        private System.Windows.Forms.Button btnAddOpWhitelistChannel;
        private System.Windows.Forms.RadioButton rbNotifyPrivateTab;
        private System.Windows.Forms.CheckBox chkEnableOwnNotifications;
        private System.Windows.Forms.CheckBox chkEnableDogSounds;
        private System.Windows.Forms.CheckBox chkFlashToolbar;
        private System.Windows.Forms.RadioButton rbNotifyAlways;
        private System.Windows.Forms.RadioButton rbNotifyInactive;
        private System.Windows.Forms.RadioButton rbDisableNotifications;
        private System.Windows.Forms.RadioButton rbNotifyWatchwords;
        private System.Windows.Forms.CheckBox chkEnableSounds;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.CheckBox chkDisableWhenAway;
        private System.Windows.Forms.Button btnRemoveWatchword;
        private System.Windows.Forms.Button btnAddWatchword;
        private System.Windows.Forms.ListBox lbWatchwords;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkEnableWatchwords;

    }
}