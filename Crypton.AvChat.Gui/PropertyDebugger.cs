using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using mshtml;

namespace Crypton.AvChat.Gui {
    public partial class PropertyDebugger : Form {


        private class HTMLWindow2ClassWrapper {
            private IHTMLWindow2 instance;

            public HTMLWindow2ClassWrapper(IHTMLWindow2 instance) {
                this.instance = instance;

                
            }

        }

        
        public PropertyDebugger(object reference) {
            InitializeComponent();

            this.Text = reference.ToString() + " - PropertyDebugger";
            propertyGrid1.SelectedObject = reference;            
        }

        private void enabledToolStripMenuItem_Click(object sender, EventArgs e) {
            int interval = 100;
            if (!int.TryParse(toolStripTextBox1.Text, out interval)) {
                interval = 100;
            }

            if (interval < 100) {
                interval = 100;
            }

            tmrPropertyUpdate.Interval = interval;

            if (enabledToolStripMenuItem.Checked) {
                enabledToolStripMenuItem.Checked = false;
                tmrPropertyUpdate.Enabled = false;
            }
            else {
                enabledToolStripMenuItem.Checked = true;
                tmrPropertyUpdate.Enabled = true;
            }
        }

        private void tmrPropertyUpdate_Tick(object sender, EventArgs e) {
            propertyGrid1.Refresh();
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e) {
            propertyGrid1.Refresh();
        }
    }
}
