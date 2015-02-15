namespace Crypton.AvChat.Gui {
    partial class ChannelApplet {
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
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.wbChat = new System.Windows.Forms.WebBrowser();
            this.ctxBrowserMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.clearHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvUserList = new System.Windows.Forms.DataGridView();
            this.colUserAvatar = new System.Windows.Forms.DataGridViewImageColumn();
            this.colUserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.txtNewMessage = new System.Windows.Forms.TextBox();
            this.tmrUnreadMsgFlash = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.ctxBrowserMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserList)).BeginInit();
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
            this.splitMain.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.txtNewMessage);
            this.splitMain.Panel2.Padding = new System.Windows.Forms.Padding(3);
            this.splitMain.Size = new System.Drawing.Size(650, 639);
            this.splitMain.SplitterDistance = 538;
            this.splitMain.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.wbChat);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvUserList);
            this.splitContainer1.Size = new System.Drawing.Size(650, 538);
            this.splitContainer1.SplitterDistance = 493;
            this.splitContainer1.TabIndex = 0;
            // 
            // wbChat
            // 
            this.wbChat.AllowNavigation = false;
            this.wbChat.AllowWebBrowserDrop = false;
            this.wbChat.ContextMenuStrip = this.ctxBrowserMenu;
            this.wbChat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbChat.IsWebBrowserContextMenuEnabled = false;
            this.wbChat.Location = new System.Drawing.Point(0, 0);
            this.wbChat.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbChat.Name = "wbChat";
            this.wbChat.ScriptErrorsSuppressed = true;
            this.wbChat.Size = new System.Drawing.Size(493, 538);
            this.wbChat.TabIndex = 1;
            this.wbChat.WebBrowserShortcutsEnabled = false;
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
            this.ctxBrowserMenu.Size = new System.Drawing.Size(159, 104);
            this.ctxBrowserMenu.Opening += new System.ComponentModel.CancelEventHandler(this.ctxBrowserMenu_Opening);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // quoteToolStripMenuItem
            // 
            this.quoteToolStripMenuItem.Name = "quoteToolStripMenuItem";
            this.quoteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.quoteToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.quoteToolStripMenuItem.Text = "Quote";
            this.quoteToolStripMenuItem.Click += new System.EventHandler(this.quoteToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(155, 6);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.selectAllToolStripMenuItem.Text = "Select All";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(155, 6);
            // 
            // clearHistoryToolStripMenuItem
            // 
            this.clearHistoryToolStripMenuItem.Name = "clearHistoryToolStripMenuItem";
            this.clearHistoryToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.clearHistoryToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.clearHistoryToolStripMenuItem.Text = "Clear History";
            this.clearHistoryToolStripMenuItem.Click += new System.EventHandler(this.clearHistoryToolStripMenuItem_Click);
            // 
            // dgvUserList
            // 
            this.dgvUserList.AllowUserToAddRows = false;
            this.dgvUserList.AllowUserToDeleteRows = false;
            this.dgvUserList.AllowUserToResizeColumns = false;
            this.dgvUserList.AllowUserToResizeRows = false;
            this.dgvUserList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUserList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvUserList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUserList.ColumnHeadersVisible = false;
            this.dgvUserList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colUserAvatar,
            this.colUserName});
            this.dgvUserList.ContextMenuStrip = this.ctxUserMenu;
            this.dgvUserList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUserList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvUserList.Location = new System.Drawing.Point(0, 0);
            this.dgvUserList.Name = "dgvUserList";
            this.dgvUserList.ReadOnly = true;
            this.dgvUserList.RowHeadersVisible = false;
            this.dgvUserList.RowTemplate.Height = 30;
            this.dgvUserList.RowTemplate.ReadOnly = true;
            this.dgvUserList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUserList.ShowEditingIcon = false;
            this.dgvUserList.Size = new System.Drawing.Size(153, 538);
            this.dgvUserList.TabIndex = 0;
            // 
            // colUserAvatar
            // 
            this.colUserAvatar.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colUserAvatar.FillWeight = 25.38071F;
            this.colUserAvatar.HeaderText = "Avatar";
            this.colUserAvatar.Name = "colUserAvatar";
            this.colUserAvatar.ReadOnly = true;
            this.colUserAvatar.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colUserAvatar.Width = 30;
            // 
            // colUserName
            // 
            this.colUserName.FillWeight = 174.6193F;
            this.colUserName.HeaderText = "Username";
            this.colUserName.Name = "colUserName";
            this.colUserName.ReadOnly = true;
            this.colUserName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
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
            this.ctxUserMenu.Size = new System.Drawing.Size(176, 192);
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
            // txtNewMessage
            // 
            this.txtNewMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNewMessage.Location = new System.Drawing.Point(3, 3);
            this.txtNewMessage.Multiline = true;
            this.txtNewMessage.Name = "txtNewMessage";
            this.txtNewMessage.Size = new System.Drawing.Size(644, 91);
            this.txtNewMessage.TabIndex = 0;
            this.txtNewMessage.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtNewMessage_KeyUp);
            // 
            // tmrUnreadMsgFlash
            // 
            this.tmrUnreadMsgFlash.Interval = 500;
            this.tmrUnreadMsgFlash.Tick += new System.EventHandler(this.tmrUnreadMsgFlash_Tick);
            // 
            // ChannelApplet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitMain);
            this.Name = "ChannelApplet";
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
            this.ctxBrowserMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserList)).EndInit();
            this.ctxUserMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.TextBox txtNewMessage;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvUserList;
        private System.Windows.Forms.DataGridViewImageColumn colUserAvatar;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUserName;
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
    }
}
