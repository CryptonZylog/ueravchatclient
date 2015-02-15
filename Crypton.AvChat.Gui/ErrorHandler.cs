using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Crypton.AvChat.Gui.Win32;

namespace Crypton.AvChat.Gui {
    public partial class ErrorHandler : Form {

        Exception sourceException = null;

        public ErrorHandler(Exception sourceException) {
            InitializeComponent();
            this.Font = SystemFonts.DialogFont;
            this.sourceException = sourceException;

            try {
                File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "error.log"), sourceException.ToString());
            }
            catch {

            }

            txtError.Text = sourceException.ToString();
        }

        private void lnlLaunchThread_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Win32Impl.OpenUrl("http://www.uer.ca/forum_showthread.asp?fid=1&threadid=102140&currpage=1");
        }

    }
}
