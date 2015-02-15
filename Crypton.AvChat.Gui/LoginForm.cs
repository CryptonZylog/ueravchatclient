using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Configuration;
using Crypton.AvChat.Client;

namespace Crypton.AvChat.Gui {
    public partial class LoginForm : Form {
        public LoginForm() {
            InitializeComponent();
            this.Font = SystemFonts.DialogFont;

            bool rememberLogin = Properties.Settings.Default.rememberLogin;
            if (rememberLogin) {
                // load
                SavedUsernamePassword unpw = Program.LoadSavedCredentials();
                txtUsername.Text = unpw.Username;
                txtPassword.Text = unpw.Password;

                cbRemember.Checked = true;
            }

            Program.GlobalClient.LoginEvent += GlobalClient_LoginEvent;
        }

        void GlobalClient_LoginEvent(ChatClient chatClient, bool loginResult, string errorMessage)
        {
            this.Invoke(new Action(delegate
            {
                this.UseWaitCursor = false;
                groupBox1.Enabled = true;

                Application.DoEvents();

                if (loginResult)
                {
                    // dialog result success

                    // save VALID credentials
                    if (cbRemember.Checked)
                    {
                        Program.SaveCredentials(new SavedUsernamePassword() { Username = txtUsername.Text, Password = txtPassword.Text });
                        Properties.Settings.Default.Save();
                    }

                    Program.Username = txtUsername.Text;
                    Program.Password = txtPassword.Text;

                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else
                {
                    MessageBox.Show(this, errorMessage, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }));
        }

        private delegate bool loginProcessDelegate(string username, string password);
        private delegate void updateLoginFormDelegate(bool success);

        private loginProcessDelegate loginProcess = null;

        private void btnExit_Click(object sender, EventArgs e) {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnLogin_Click(object sender, EventArgs e) {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            // validate
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password)) {
                MessageBox.Show(this, "Username and Password are required", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            this.UseWaitCursor = true;
            groupBox1.Enabled = false;
            Application.DoEvents();

            Program.GlobalClient.BeginLogin(txtUsername.Text, txtPassword.Text);
        }
        
        private void cbRemember_CheckedChanged(object sender, EventArgs e) {
            Properties.Settings.Default.rememberLogin = cbRemember.Checked;
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.GlobalClient.LoginEvent -= GlobalClient_LoginEvent;
        }

        

    }
}
