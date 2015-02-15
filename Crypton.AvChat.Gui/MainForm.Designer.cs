namespace Crypton.AvChat.Gui {
    partial class MainForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblConnectionStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.prgUpdate = new System.Windows.Forms.ToolStripProgressBar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.channelListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.officialThreadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsConnectDisconnect = new System.Windows.Forms.ToolStripMenuItem();
            this.leaveChannelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newVersionIsAvailableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbRooms = new System.Windows.Forms.TabControl();
            this.imgListMsgNotifyIcons = new System.Windows.Forms.ImageList(this.components);
            this.tmrIdleTimeReporter = new System.Windows.Forms.Timer(this.components);
            this.ctxTabMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.closeTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closePrivateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nfBossMode = new System.Windows.Forms.NotifyIcon(this.components);
            this.reportBugfeatureRequestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.ctxTabMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblConnectionStatus,
            this.prgUpdate});
            this.statusStrip1.Location = new System.Drawing.Point(0, 645);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(626, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblConnectionStatus
            // 
            this.lblConnectionStatus.Name = "lblConnectionStatus";
            this.lblConnectionStatus.Size = new System.Drawing.Size(107, 17);
            this.lblConnectionStatus.Text = "[connectionStatus]";
            // 
            // prgUpdate
            // 
            this.prgUpdate.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.prgUpdate.Name = "prgUpdate";
            this.prgUpdate.Size = new System.Drawing.Size(100, 16);
            this.prgUpdate.Visible = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.channelListToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.tsConnectDisconnect,
            this.leaveChannelToolStripMenuItem,
            this.newVersionIsAvailableToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(626, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "&Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // channelListToolStripMenuItem
            // 
            this.channelListToolStripMenuItem.Name = "channelListToolStripMenuItem";
            this.channelListToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.channelListToolStripMenuItem.Text = "&Channel List";
            this.channelListToolStripMenuItem.Click += new System.EventHandler(this.channelListToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.officialThreadToolStripMenuItem,
            this.checkForUpdatesToolStripMenuItem,
            this.reportBugfeatureRequestToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // officialThreadToolStripMenuItem
            // 
            this.officialThreadToolStripMenuItem.Name = "officialThreadToolStripMenuItem";
            this.officialThreadToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.officialThreadToolStripMenuItem.Text = "Official Thread";
            this.officialThreadToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.checkForUpdatesToolStripMenuItem.Text = "Check For Updates";
            this.checkForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdatesToolStripMenuItem_Click);
            // 
            // tsConnectDisconnect
            // 
            this.tsConnectDisconnect.Name = "tsConnectDisconnect";
            this.tsConnectDisconnect.Size = new System.Drawing.Size(91, 20);
            this.tsConnectDisconnect.Text = "Disconnected";
            this.tsConnectDisconnect.Click += new System.EventHandler(this.tsConnectDisconnect_Click);
            // 
            // leaveChannelToolStripMenuItem
            // 
            this.leaveChannelToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.leaveChannelToolStripMenuItem.AutoToolTip = true;
            this.leaveChannelToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.leaveChannelToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("leaveChannelToolStripMenuItem.Image")));
            this.leaveChannelToolStripMenuItem.Name = "leaveChannelToolStripMenuItem";
            this.leaveChannelToolStripMenuItem.Size = new System.Drawing.Size(28, 20);
            this.leaveChannelToolStripMenuItem.Text = "toolStripMenuItem1";
            this.leaveChannelToolStripMenuItem.ToolTipText = "Leave current channel";
            this.leaveChannelToolStripMenuItem.Click += new System.EventHandler(this.leaveChannelToolStripMenuItem_Click);
            // 
            // newVersionIsAvailableToolStripMenuItem
            // 
            this.newVersionIsAvailableToolStripMenuItem.Name = "newVersionIsAvailableToolStripMenuItem";
            this.newVersionIsAvailableToolStripMenuItem.Size = new System.Drawing.Size(147, 20);
            this.newVersionIsAvailableToolStripMenuItem.Text = "New version is available!";
            this.newVersionIsAvailableToolStripMenuItem.Visible = false;
            this.newVersionIsAvailableToolStripMenuItem.Click += new System.EventHandler(this.newVersionIsAvailableToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbRooms);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(626, 621);
            this.panel1.TabIndex = 2;
            // 
            // tbRooms
            // 
            this.tbRooms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbRooms.ImageList = this.imgListMsgNotifyIcons;
            this.tbRooms.Location = new System.Drawing.Point(10, 10);
            this.tbRooms.Name = "tbRooms";
            this.tbRooms.SelectedIndex = 0;
            this.tbRooms.Size = new System.Drawing.Size(606, 601);
            this.tbRooms.TabIndex = 0;
            this.tbRooms.SelectedIndexChanged += new System.EventHandler(this.tbRooms_SelectedIndexChanged);
            this.tbRooms.TabIndexChanged += new System.EventHandler(this.tbRooms_SelectedIndexChanged);
            this.tbRooms.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tbRooms_MouseClick);
            this.tbRooms.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tbRooms_MouseDown);
            this.tbRooms.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tbRooms_MouseMove);
            this.tbRooms.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tbRooms_MouseUp);
            // 
            // imgListMsgNotifyIcons
            // 
            this.imgListMsgNotifyIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgListMsgNotifyIcons.ImageStream")));
            this.imgListMsgNotifyIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imgListMsgNotifyIcons.Images.SetKeyName(0, "channel_default");
            this.imgListMsgNotifyIcons.Images.SetKeyName(1, "channel_newmessage");
            this.imgListMsgNotifyIcons.Images.SetKeyName(2, "channel_private");
            // 
            // tmrIdleTimeReporter
            // 
            this.tmrIdleTimeReporter.Interval = 5000;
            this.tmrIdleTimeReporter.Tick += new System.EventHandler(this.tmrIdleTimeReporter_Tick);
            // 
            // ctxTabMenu
            // 
            this.ctxTabMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeTabToolStripMenuItem,
            this.toolStripMenuItem1,
            this.closeAllToolStripMenuItem,
            this.closePrivateToolStripMenuItem});
            this.ctxTabMenu.Name = "ctxTabMenu";
            this.ctxTabMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ctxTabMenu.ShowImageMargin = false;
            this.ctxTabMenu.Size = new System.Drawing.Size(118, 76);
            // 
            // closeTabToolStripMenuItem
            // 
            this.closeTabToolStripMenuItem.Name = "closeTabToolStripMenuItem";
            this.closeTabToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.closeTabToolStripMenuItem.Text = "Close Tab";
            this.closeTabToolStripMenuItem.Click += new System.EventHandler(this.closeTabToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(114, 6);
            // 
            // closeAllToolStripMenuItem
            // 
            this.closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
            this.closeAllToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.closeAllToolStripMenuItem.Text = "Close All";
            this.closeAllToolStripMenuItem.Click += new System.EventHandler(this.closeAllToolStripMenuItem_Click);
            // 
            // closePrivateToolStripMenuItem
            // 
            this.closePrivateToolStripMenuItem.Name = "closePrivateToolStripMenuItem";
            this.closePrivateToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.closePrivateToolStripMenuItem.Text = "Close Private";
            this.closePrivateToolStripMenuItem.Click += new System.EventHandler(this.closePrivateToolStripMenuItem_Click);
            // 
            // nfBossMode
            // 
            this.nfBossMode.Icon = ((System.Drawing.Icon)(resources.GetObject("nfBossMode.Icon")));
            this.nfBossMode.Text = "Click to restore chat";
            this.nfBossMode.Click += new System.EventHandler(this.nfBossMode_Click);
            // 
            // reportBugfeatureRequestToolStripMenuItem
            // 
            this.reportBugfeatureRequestToolStripMenuItem.Name = "reportBugfeatureRequestToolStripMenuItem";
            this.reportBugfeatureRequestToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.reportBugfeatureRequestToolStripMenuItem.Text = "Report bug/feature request";
            this.reportBugfeatureRequestToolStripMenuItem.Click += new System.EventHandler(this.reportBugfeatureRequestToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 667);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Crypton AvChat (Beta)";
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.ResizeBegin += new System.EventHandler(this.MainForm_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.MainForm_ResizeEnd);
            this.Move += new System.EventHandler(this.MainForm_Move);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ctxTabMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem channelListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsConnectDisconnect;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripStatusLabel lblConnectionStatus;
        private System.Windows.Forms.ImageList imgListMsgNotifyIcons;
        private System.Windows.Forms.ToolStripMenuItem leaveChannelToolStripMenuItem;
        private System.Windows.Forms.Timer tmrIdleTimeReporter;
        public System.Windows.Forms.TabControl tbRooms;
        private System.Windows.Forms.ContextMenuStrip ctxTabMenu;
        private System.Windows.Forms.ToolStripMenuItem closeTabToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closePrivateToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon nfBossMode;
        private System.Windows.Forms.ToolStripMenuItem newVersionIsAvailableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem officialThreadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripProgressBar prgUpdate;
        private System.Windows.Forms.ToolStripMenuItem reportBugfeatureRequestToolStripMenuItem;
    }
}