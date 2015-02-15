using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Crypton.AvChat.Gui {
    public partial class ChangeLogView : Form {
        public ChangeLogView() {
            InitializeComponent();

            this.Font = SystemFonts.DialogFont;

            string fpath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "changelog.txt"));
            if (File.Exists(fpath)) {
                txtChangeLog.Text = File.ReadAllText(fpath);
            }

            txtChangeLog.SelectionStart = 0;
            txtChangeLog.SelectionLength = 0;
        }
    }
}
