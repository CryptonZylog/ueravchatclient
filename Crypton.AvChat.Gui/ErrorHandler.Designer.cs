namespace Crypton.AvChat.Gui {
    partial class ErrorHandler {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ErrorHandler));
            this.label1 = new System.Windows.Forms.Label();
            this.txtError = new System.Windows.Forms.TextBox();
            this.lnlLaunchThread = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(431, 99);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // txtError
            // 
            this.txtError.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtError.Location = new System.Drawing.Point(14, 114);
            this.txtError.Multiline = true;
            this.txtError.Name = "txtError";
            this.txtError.ReadOnly = true;
            this.txtError.Size = new System.Drawing.Size(541, 432);
            this.txtError.TabIndex = 1;
            // 
            // lnlLaunchThread
            // 
            this.lnlLaunchThread.AutoSize = true;
            this.lnlLaunchThread.Location = new System.Drawing.Point(340, 35);
            this.lnlLaunchThread.Name = "lnlLaunchThread";
            this.lnlLaunchThread.Size = new System.Drawing.Size(53, 13);
            this.lnlLaunchThread.TabIndex = 2;
            this.lnlLaunchThread.TabStop = true;
            this.lnlLaunchThread.Text = "click here";
            this.lnlLaunchThread.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnlLaunchThread_LinkClicked);
            // 
            // ErrorHandler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 569);
            this.Controls.Add(this.lnlLaunchThread);
            this.Controls.Add(this.txtError);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ErrorHandler";
            this.Text = "Application Error";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtError;
        private System.Windows.Forms.LinkLabel lnlLaunchThread;
    }
}