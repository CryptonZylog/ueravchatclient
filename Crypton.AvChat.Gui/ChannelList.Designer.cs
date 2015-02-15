namespace Crypton.AvChat.Gui {
    partial class ChannelList {
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
            this.lvChannels = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ctxMenuChannelListItem = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.joinChannelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuChannelListItem.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvChannels
            // 
            this.lvChannels.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lvChannels.ContextMenuStrip = this.ctxMenuChannelListItem;
            this.lvChannels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvChannels.FullRowSelect = true;
            this.lvChannels.Location = new System.Drawing.Point(10, 10);
            this.lvChannels.MultiSelect = false;
            this.lvChannels.Name = "lvChannels";
            this.lvChannels.Size = new System.Drawing.Size(614, 432);
            this.lvChannels.TabIndex = 0;
            this.lvChannels.UseCompatibleStateImageBehavior = false;
            this.lvChannels.View = System.Windows.Forms.View.Details;
            this.lvChannels.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvChannels_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Channel";
            this.columnHeader1.Width = 200;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Users";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Topic";
            this.columnHeader3.Width = 300;
            // 
            // ctxMenuChannelListItem
            // 
            this.ctxMenuChannelListItem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.joinChannelToolStripMenuItem,
            this.toolStripMenuItem1,
            this.copyToolStripMenuItem});
            this.ctxMenuChannelListItem.Name = "ctxMenuChannelListItem";
            this.ctxMenuChannelListItem.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ctxMenuChannelListItem.ShowImageMargin = false;
            this.ctxMenuChannelListItem.Size = new System.Drawing.Size(158, 54);
            this.ctxMenuChannelListItem.Opening += new System.ComponentModel.CancelEventHandler(this.ctxMenuChannelListItem_Opening);
            // 
            // joinChannelToolStripMenuItem
            // 
            this.joinChannelToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.joinChannelToolStripMenuItem.Name = "joinChannelToolStripMenuItem";
            this.joinChannelToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.J)));
            this.joinChannelToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.joinChannelToolStripMenuItem.Text = "Join channel";
            this.joinChannelToolStripMenuItem.Click += new System.EventHandler(this.joinChannelToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(154, 6);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // ChannelList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 452);
            this.Controls.Add(this.lvChannels);
            this.Name = "ChannelList";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Channel List";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ChannelList_FormClosed);
            this.Shown += new System.EventHandler(this.ChannelList_Shown);
            this.ctxMenuChannelListItem.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvChannels;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ContextMenuStrip ctxMenuChannelListItem;
        private System.Windows.Forms.ToolStripMenuItem joinChannelToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
    }
}