using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace Crypton.AvChat.Win
{
    class TextEntryProcessor
    {

        private TextBox textBox = null;
        private string channelName = null;

        public TextEntryProcessor(TextBox entryBox, string channelName)
        {
            this.textBox = entryBox;
            this.channelName = channelName;
            this.textBox.KeyDown += textBox_KeyDown;
        }

        private void filterMessage(string text)
        {
            string lowered = text.ToLowerInvariant();
            if (lowered.StartsWith("/"))
            {
                // command
                string[] rawsegments = lowered.Substring(1).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (rawsegments.Length > 0)
                {
                    string command = rawsegments[0];
                    string[] arguments = rawsegments.Skip(1).ToArray();
                    switch (command)
                    {
                        case "debugkillconn":
                            ChatDispatcher.Singleton.DebugKillConnection();
                            break;
                        default:
                            // bubble to server if neither command
                            ChatDispatcher.Singleton.SendCommand(text, this.channelName);
                            break;
                    }
                }
            }
            else
            {
                // send message
                ChatDispatcher.Singleton.SendCommand(string.Format("/msg {0} {1}", this.channelName, text), this.channelName);
            }
        }

        private void textBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            bool isCtrl = (e.Key & Key.LeftCtrl) == Key.LeftCtrl || (e.Key & Key.RightCtrl) == Key.RightCtrl;
            if (isCtrl && (e.Key & Key.Up) == Key.Up)
            {
                // fetch previous sent message
            }

            if (isCtrl && (e.Key & Key.Down) == Key.Down)
            {
                // fetch next sent message
            }

            if (isCtrl && (e.Key & Key.Enter) == Key.Enter)
            {
                // CTRL+Enter, allow to go to next line
            }
            else if (e.Key == Key.Enter)
            {
                e.Handled = true;
                string text = textBox.Text.TrimEnd();
                if (text.Length > 0)
                {
                    textBox.Text = string.Empty;
                    this.filterMessage(text);
                    // strip out last CRLF
                    /*sendFilteredMessage(msg);
                    trackedSentMessages.Add(msg);
                    trackedSentMessageIndex = trackedSentMessages.Count - 1;
                    txtNewMessage.Text = string.Empty;
                    txtNewMessage.Focus();*/
                }
            }



            /*if (e.Control && e.KeyCode == Keys.Up)
            {
                // fetch previous sent message
                if (trackedSentMessageIndex - 1 >= 0 && trackedSentMessages.Count > 0)
                {
                    txtNewMessage.Text = trackedSentMessages[--trackedSentMessageIndex];
                    txtNewMessage.SelectionStart = txtNewMessage.Text.Length;
                    e.Handled = true;
                    return;
                }
                else
                {
                    // nothing available
                    System.Media.SystemSounds.Beep.Play();
                }
            }
            if (e.Control && e.KeyCode == Keys.Down)
            {
                // fetch next available message
                if (trackedSentMessageIndex + 1 < trackedSentMessages.Count)
                {
                    txtNewMessage.Text = trackedSentMessages[++trackedSentMessageIndex];
                    txtNewMessage.SelectionStart = txtNewMessage.Text.Length;
                    e.Handled = true;
                    return;
                }
                else
                {
                    // nothing available
                    System.Media.SystemSounds.Beep.Play();
                }
            }*/

            /* if (e.Control && e.KeyCode == Keys.Enter)
             {
                 // CTRL+Enter, allow to go to next line
             }
             else if (e.KeyCode == Keys.Enter)
             {
                 e.Handled = true;
                 e.SuppressKeyPress = true;
                 // send msg 
                 if (Program.GlobalClient.ConnectionStatus != ConnectionStatusTypes.Connected)
                 {
                     MessageBox.Show("You are not connected to the server!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                     return;
                 }
                 string msg = txtNewMessage.Text.TrimEnd();
                 if (msg.Length > 0)
                 {
                     // strip out last CRLF
                     sendFilteredMessage(msg);
                     trackedSentMessages.Add(msg);
                     trackedSentMessageIndex = trackedSentMessages.Count - 1;
                     txtNewMessage.Text = string.Empty;
                     txtNewMessage.Focus();
                 }
             }

             if (e.Control && e.KeyCode == Keys.A)
             {
                 //Ctrl+A
                 txtNewMessage.SelectAll();
             } */
        }

    }
}
