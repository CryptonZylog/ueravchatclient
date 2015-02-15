using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Crypton.AvChat.Gui.Power.OpWhitelist {
    public partial class UserPrompt : Form {
        public UserPrompt() {
            InitializeComponent();
            this.Font = SystemFonts.DialogFont;
        }

        /// <summary>
        /// Gets the entered user name
        /// </summary>
        public string UserName {
            get;
            private set;
        }

        private void UserPrompt_Shown(object sender, EventArgs e) {
            this.ActiveControl = txtUserName;
        }

        private void btnOk_Click(object sender, EventArgs e) {
            if (!string.IsNullOrWhiteSpace(txtUserName.Text)) {
                this.UserName = txtUserName.Text.Trim();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else {
                MessageBox.Show("User name is required", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
