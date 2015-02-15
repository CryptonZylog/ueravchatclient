using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Crypton.AvChat.Gui.Power.OpWhitelist {
    public partial class ChannelPrompt : Form {
        public ChannelPrompt() {
            InitializeComponent();
            this.Font = SystemFonts.DialogFont;
        }

        /// <summary>
        /// Gets the entered channel name
        /// </summary>
        public string ChannelName {
            get;
            private set;
        }

        private void ChannelPrompt_Shown(object sender, EventArgs e) {
            this.ActiveControl = txtChannelName;
        }

        private void btnOk_Click(object sender, EventArgs e) {
            if (!string.IsNullOrWhiteSpace(txtChannelName.Text)) {
                this.ChannelName = txtChannelName.Text.Trim();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else {
                MessageBox.Show("Channel name is required", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
