namespace Crypton.AvChat.Gui.Tabs {
    partial class PrivateTab {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrivateTab));
            this.splitMain = new System.Windows.Forms.SplitContainer();
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
            this.txtNewMessage = new System.Windows.Forms.TextBox();
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
            this.tmrUnreadMsgFlash = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.ctxBrowserMenu.SuspendLayout();
            this.ctxUserMenu.SuspendLayout();
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
            this.splitMain.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.txtNewMessage);
            this.splitMain.Panel2.Padding = new System.Windows.Forms.Padding(3);
            this.splitMain.Size = new System.Drawing.Size(650, 639);
            this.splitMain.SplitterDistance = 538;
            this.splitMain.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel1.Controls.Add(this.wbChat, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblTopic, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(650, 538);
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
            this.wbChat.Size = new System.Drawing.Size(644, 500);
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
            this.lblTopic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTopic.Location = new System.Drawing.Point(8, 8);
            this.lblTopic.Margin = new System.Windows.Forms.Padding(8);
            this.lblTopic.Name = "lblTopic";
            this.lblTopic.Size = new System.Drawing.Size(605, 16);
            this.lblTopic.TabIndex = 5;
            this.lblTopic.Text = "[topic or channel name]";
            this.lblTopic.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.txtNewMessage.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtNewMessage_KeyUp);
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
            // 
            // usernameChatVersionToolStripMenuItem
            // 
            this.usernameChatVersionToolStripMenuItem.Name = "usernameChatVersionToolStripMenuItem";
            this.usernameChatVersionToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.usernameChatVersionToolStripMenuItem.Text = "[username/chatversion]";
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
            // 
            // sendAlertToolStripItem
            // 
            this.sendAlertToolStripItem.Name = "sendAlertToolStripItem";
            this.sendAlertToolStripItem.Size = new System.Drawing.Size(175, 22);
            this.sendAlertToolStripItem.Text = "Send Alert";
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
            // 
            // opDeOpToolStripMenuItem
            // 
            this.opDeOpToolStripMenuItem.Name = "opDeOpToolStripMenuItem";
            this.opDeOpToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.opDeOpToolStripMenuItem.Text = "De-Op";
            // 
            // opKickToolStripMenuItem
            // 
            this.opKickToolStripMenuItem.Name = "opKickToolStripMenuItem";
            this.opKickToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.opKickToolStripMenuItem.Text = "Kick";
            // 
            // imgUserAvatars
            // 
            this.imgUserAvatars.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgUserAvatars.ImageStream")));
            this.imgUserAvatars.TransparentColor = System.Drawing.Color.Transparent;
            this.imgUserAvatars.Images.SetKeyName(0, "44601.jpg");
            // 
            // tmrUnreadMsgFlash
            // 
            this.tmrUnreadMsgFlash.Interval = 500;
            this.tmrUnreadMsgFlash.Tick += new System.EventHandler(this.tmrUnreadMsgFlash_Tick);
            // 
            // PrivateTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitMain);
            this.Name = "PrivateTab";
            this.Size = new System.Drawing.Size(650, 639);
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            this.splitMain.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ctxBrowserMenu.ResumeLayout(false);
            this.ctxUserMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitMain;
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
        private System.Windows.Forms.ImageList imgUserAvatars;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblTopic;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
