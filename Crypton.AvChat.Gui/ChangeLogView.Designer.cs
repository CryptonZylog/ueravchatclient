namespace Crypton.AvChat.Gui {
    partial class ChangeLogView {
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
            this.txtChangeLog = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtChangeLog
            // 
            this.txtChangeLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtChangeLog.Location = new System.Drawing.Point(10, 10);
            this.txtChangeLog.Multiline = true;
            this.txtChangeLog.Name = "txtChangeLog";
            this.txtChangeLog.ReadOnly = true;
            this.txtChangeLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtChangeLog.Size = new System.Drawing.Size(494, 524);
            this.txtChangeLog.TabIndex = 0;
            this.txtChangeLog.WordWrap = false;
            // 
            // ChangeLogView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 544);
            this.Controls.Add(this.txtChangeLog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChangeLogView";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Change Log";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtChangeLog;
    }
}