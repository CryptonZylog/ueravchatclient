namespace Crypton.AvChat.Gui.Tabs {
    partial class ChannelTab {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChannelTab));
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.wbChat = new System.Windows.Forms.WebBrowser();
            this.ctxBrowserMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.clearHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblTopic = new System.Windows.Forms.Label();
            this.lvUsers = new System.Windows.Forms.ListView();
            this.ctxUserMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.usernameChatVersionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.privateChatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendAlertToolStripItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorOp = new System.Windows.Forms.ToolStripSeparator();
            this.opOpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.opDeOpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.opKickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imgUserAvatars = new System.Windows.Forms.ImageList(this.components);
            this.txtNewMessage = new System.Windows.Forms.TextBox();
            this.tmrUnreadMsgFlash = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ctxTopicOption = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.ctxBrowserMenu.SuspendLayout();
            this.ctxUserMenu.SuspendLayout();
            this.ctxTopicOption.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitMain
            // 
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(0, 0);
            this.splitMain.Name = "splitMain";
            this.splitMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.txtNewMessage);
            this.splitMain.Panel2.Padding = new System.Windows.Forms.Padding(3);
            this.splitMain.Size = new System.Drawing.Size(650, 639);
            this.splitMain.SplitterDistance = 538;
            this.splitMain.TabIndex = 0;
            this.splitMain.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitMain_SplitterMoved);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.Panel1MinSize = 250;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lvUsers);
            this.splitContainer1.Panel2MinSize = 100;
            this.splitContainer1.Size = new System.Drawing.Size(650, 538);
            this.splitContainer1.SplitterDistance = 531;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.wbChat, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblTopic, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(531, 538);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // wbChat
            // 
            this.wbChat.AllowNavigation = false;
            this.wbChat.AllowWebBrowserDrop = false;
            this.tableLayoutPanel1.SetColumnSpan(this.wbChat, 2);
            this.wbChat.ContextMenuStrip = this.ctxBrowserMenu;
            this.wbChat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbChat.IsWebBrowserContextMenuEnabled = false;
            this.wbChat.Location = new System.Drawing.Point(3, 35);
            this.wbChat.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbChat.Name = "wbChat";
            this.wbChat.ScriptErrorsSuppressed = true;
            this.wbChat.Size = new System.Drawing.Size(525, 500);
            this.wbChat.TabIndex = 1;
            this.wbChat.NewWindow += new System.ComponentModel.CancelEventHandler(this.wbChat_NewWindow);
            // 
            // ctxBrowserMenu
            // 
            this.ctxBrowserMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.quoteToolStripMenuItem,
            this.toolStripMenuItem1,
            this.selectAllToolStripMenuItem,
            this.toolStripMenuItem2,
            this.clearHistoryToolStripMenuItem});
            this.ctxBrowserMenu.Name = "ctxBrowserMenu";
            this.ctxBrowserMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ctxBrowserMenu.ShowImageMargin = false;
            this.ctxBrowserMenu.Size = new System.Drawing.Size(140, 104);
            this.ctxBrowserMenu.Opening += new System.ComponentModel.CancelEventHandler(this.ctxBrowserMenu_Opening);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // quoteToolStripMenuItem
            // 
            this.quoteToolStripMenuItem.Name = "quoteToolStripMenuItem";
            this.quoteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.quoteToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.quoteToolStripMenuItem.Text = "Quote";
            this.quoteToolStripMenuItem.Click += new System.EventHandler(this.quoteToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(136, 6);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.selectAllToolStripMenuItem.Text = "Select All";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(136, 6);
            // 
            // clearHistoryToolStripMenuItem
            // 
            this.clearHistoryToolStripMenuItem.Name = "clearHistoryToolStripMenuItem";
            this.clearHistoryToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.clearHistoryToolStripMenuItem.Text = "Clear History";
            this.clearHistoryToolStripMenuItem.Click += new System.EventHandler(this.clearHistoryToolStripMenuItem_Click);
            // 
            // lblTopic
            // 
            this.lblTopic.ContextMenuStrip = this.ctxTopicOption;
            this.lblTopic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTopic.Location = new System.Drawing.Point(8, 8);
            this.lblTopic.Margin = new System.Windows.Forms.Padding(8);
            this.lblTopic.Name = "lblTopic";
            this.lblTopic.Size = new System.Drawing.Size(486, 16);
            this.lblTopic.TabIndex = 5;
            this.lblTopic.Text = "[topic or channel name]";
            this.lblTopic.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lvUsers
            // 
            this.lvUsers.ContextMenuStrip = this.ctxUserMenu;
            this.lvUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvUsers.FullRowSelect = true;
            this.lvUsers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvUsers.LargeImageList = this.imgUserAvatars;
            this.lvUsers.Location = new System.Drawing.Point(0, 0);
            this.lvUsers.MultiSelect = false;
            this.lvUsers.Name = "lvUsers";
            this.lvUsers.ShowGroups = false;
            this.lvUsers.Size = new System.Drawing.Size(115, 538);
            this.lvUsers.SmallImageList = this.imgUserAvatars;
            this.lvUsers.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvUsers.TabIndex = 0;
            this.lvUsers.TileSize = new System.Drawing.Size(30, 36);
            this.lvUsers.UseCompatibleStateImageBehavior = false;
            this.lvUsers.View = System.Windows.Forms.View.Tile;
            this.lvUsers.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvUsers_MouseDoubleClick);
            // 
            // ctxUserMenu
            // 
            this.ctxUserMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.usernameChatVersionToolStripMenuItem,
            this.timeToolStripMenuItem,
            this.toolStripSeparator1,
            this.privateChatToolStripMenuItem,
            this.sendAlertToolStripItem,
            this.toolStripSeparatorOp,
            this.opOpToolStripMenuItem,
            this.opDeOpToolStripMenuItem,
            this.opKickToolStripMenuItem});
            this.ctxUserMenu.Name = "ctxUserMenu";
            this.ctxUserMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ctxUserMenu.ShowImageMargin = false;
            this.ctxUserMenu.Size = new System.Drawing.Size(176, 170);
            this.ctxUserMenu.Opening += new System.ComponentModel.CancelEventHandler(this.ctxUserMenu_Opening);
            // 
            // usernameChatVersionToolStripMenuItem
            // 
            this.usernameChatVersionToolStripMenuItem.Name = "usernameChatVersionToolStripMenuItem";
            this.usernameChatVersionToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.usernameChatVersionToolStripMenuItem.Text = "[username/chatversion]";
            this.usernameChatVersionToolStripMenuItem.Click += new System.EventHandler(this.usernameChatVersionToolStripMenuItem_Click);
            // 
            // timeToolStripMenuItem
            // 
            this.timeToolStripMenuItem.Name = "timeToolStripMenuItem";
            this.timeToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.timeToolStripMenuItem.Text = "[time]";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(172, 6);
            // 
            // privateChatToolStripMenuItem
            // 
            this.privateChatToolStripMenuItem.Name = "privateChatToolStripMenuItem";
            this.privateChatToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.privateChatToolStripMenuItem.Text = "Private Chat";
            this.privateChatToolStripMenuItem.Click += new System.EventHandler(this.privateChatToolStripMenuItem_Click);
            // 
            // sendAlertToolStripItem
            // 
            this.sendAlertToolStripItem.Name = "sendAlertToolStripItem";
            this.sendAlertToolStripItem.Size = new System.Drawing.Size(175, 22);
            this.sendAlertToolStripItem.Text = "Send Alert";
            this.sendAlertToolStripItem.Click += new System.EventHandler(this.sendAlertToolStripItem_Click);
            // 
            // toolStripSeparatorOp
            // 
            this.toolStripSeparatorOp.Name = "toolStripSeparatorOp";
            this.toolStripSeparatorOp.Size = new System.Drawing.Size(172, 6);
            // 
            // opOpToolStripMenuItem
            // 
            this.opOpToolStripMenuItem.Name = "opOpToolStripMenuItem";
            this.opOpToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.opOpToolStripMenuItem.Text = "Op";
            this.opOpToolStripMenuItem.Click += new System.EventHandler(this.opOpToolStripMenuItem_Click);
            // 
            // opDeOpToolStripMenuItem
            // 
            this.opDeOpToolStripMenuItem.Name = "opDeOpToolStripMenuItem";
            this.opDeOpToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.opDeOpToolStripMenuItem.Text = "De-Op";
            this.opDeOpToolStripMenuItem.Click += new System.EventHandler(this.opDeOpToolStripMenuItem_Click);
            // 
            // opKickToolStripMenuItem
            // 
            this.opKickToolStripMenuItem.Name = "opKickToolStripMenuItem";
            this.opKickToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.opKickToolStripMenuItem.Text = "Kick";
            this.opKickToolStripMenuItem.Click += new System.EventHandler(this.opKickToolStripMenuItem_Click);
            // 
            // imgUserAvatars
            // 
            this.imgUserAvatars.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgUserAvatars.ImageStream")));
            this.imgUserAvatars.TransparentColor = System.Drawing.Color.Transparent;
            this.imgUserAvatars.Images.SetKeyName(0, "44601.jpg");
            // 
            // txtNewMessage
            // 
            this.txtNewMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNewMessage.Location = new System.Drawing.Point(3, 3);
            this.txtNewMessage.Multiline = true;
            this.txtNewMessage.Name = "txtNewMessage";
            this.txtNewMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNewMessage.Size = new System.Drawing.Size(644, 91);
            this.txtNewMessage.TabIndex = 0;
            this.txtNewMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNewMessage_KeyDown);
            // 
            // tmrUnreadMsgFlash
            // 
            this.tmrUnreadMsgFlash.Interval = 500;
            this.tmrUnreadMsgFlash.Tick += new System.EventHandler(this.tmrUnreadMsgFlash_Tick);
            // 
            // ctxTopicOption
            // 
            this.ctxTopicOption.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem1});
            this.ctxTopicOption.Name = "ctxTopicOption";
            this.ctxTopicOption.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ctxTopicOption.ShowImageMargin = false;
            this.ctxTopicOption.Size = new System.Drawing.Size(128, 48);
            // 
            // copyToolStripMenuItem1
            // 
            this.copyToolStripMenuItem1.Name = "copyToolStripMenuItem1";
            this.copyToolStripMenuItem1.Size = new System.Drawing.Size(127, 22);
            this.copyToolStripMenuItem1.Text = "Copy";
            this.copyToolStripMenuItem1.Click += new System.EventHandler(this.copyToolStripMenuItem1_Click);
            // 
            // ChannelTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitMain);
            this.Name = "ChannelTab";
            this.Size = new System.Drawing.Size(650, 639);
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            this.splitMain.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ctxBrowserMenu.ResumeLayout(false);
            this.ctxUserMenu.ResumeLayout(false);
            this.ctxTopicOption.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Timer tmrUnreadMsgFlash;
        private System.Windows.Forms.WebBrowser wbChat;
        private System.Windows.Forms.ContextMenuStrip ctxBrowserMenu;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quoteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem clearHistoryToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip ctxUserMenu;
        private System.Windows.Forms.ToolStripMenuItem usernameChatVersionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem timeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem privateChatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sendAlertToolStripItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorOp;
        private System.Windows.Forms.ToolStripMenuItem opOpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem opDeOpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem opKickToolStripMenuItem;
        internal System.Windows.Forms.TextBox txtNewMessage;
        private System.Windows.Forms.ListView lvUsers;
        private System.Windows.Forms.ImageList imgUserAvatars;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblTopic;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip ctxTopicOption;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem1;
    }
}
