using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Diagnostics;
using System.Windows.Forms;
using Crypton.AvChat.Gui.Win32;

namespace Crypton.AvChat.Gui.Tabs {
    [ComVisible(true)]
    public class BrowserProxy {

        private UserControl Tab {
            get;
            set;
        }

        private TextBox txtNewMessage {
            get;
            set;
        }

        public void open(string url) {
            Win32Impl.OpenUrl(url);
        }

        public void quoteText(string text) {
            txtNewMessage.Text += ">" + text + Environment.NewLine;
            txtNewMessage.Focus();
            txtNewMessage.SelectionLength = 0;
            txtNewMessage.SelectionStart = txtNewMessage.Text.Length;
        }

        internal BrowserProxy(UserControl tab) {
            this.Tab = tab;
            this.txtNewMessage = (TextBox)this.Tab.Controls.Find("txtNewMessage", true).FirstOrDefault();
        }

    }
}
