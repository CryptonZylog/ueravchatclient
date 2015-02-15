using System;
using System.Collections.Generic;

using System.Text;
using System.Collections.Specialized;
using System.Web;

namespace Crypton.AvChat.Client.Events {
    /// <summary>
    /// Information about a received user message
    /// </summary>
    public class AddMessageEventArgs : PacketEventArgs {
        /// <summary>
        /// Contains detailed information about how the message should be displayed with regard to the user
        /// </summary>
        public class MessageUserInfo {
            /// <summary>
            /// Gets the name of the user
            /// </summary>
            public string Name {
                get;
                private set;
            }
            /// <summary>
            /// Gets the UER user id
            /// </summary>
            public int UserID {
                get;
                private set;
            }
            /// <summary>
            /// Whether the message is spoken in /me format
            /// </summary>
            public bool IsMe {
                get;
                private set;
            }
            /// <summary>
            /// Whether the originating user is you
            /// </summary>
            public bool IsMyself {
                get;
                private set;
            }
            /// <summary>
            /// Gets the HTML color of the given message
            /// </summary>
            public string Color {
                get;
                private set;
            }
            /// <summary>
            /// Gets the gender of the user
            /// </summary>
            public UserGenders Gender {
                get;
                private set;
            }

            internal MessageUserInfo(string data) {
                /*
Name:UserID:is /me command:is this myself speaking:chat color:gender

Name - The user's name
UserID - The UER User ID that can be used to link to this person's profile.
is /me command - can be 1 or 0. If 1, the /me command was used, and it should be displayed differently, see below.
is this myself speaking - can be 1 or 0. If 1, this message is from myself, and you can choose to show it in a different color to help ID it.
chat color - six characters indicating the user's chosen chat color.
gender - can be 0, 1, or 2.  0 means unknown or other, 1 means male, 2 means female.  Usually the name is shown in blue if male, red if female.*/

                string[] fields = data.Split(':');

                this.Name = Uri.UnescapeDataString(fields[1]);
                this.UserID = int.Parse(fields[2]);
                this.IsMe = int.Parse(fields[3]) == 1;
                this.IsMyself = int.Parse(fields[4]) == 1;
                this.Color = !string.IsNullOrEmpty(fields[5]) ? "#" + Uri.UnescapeDataString(fields[5]) : null;
                this.Gender = (UserGenders)int.Parse(fields[6]);
                

            }
        }

        /// <summary>
        /// Gets the name of the channel
        /// </summary>
        public string ChannelName {
            get;
            private set;
        }
        /// <summary>
        /// Gets the message text
        /// </summary>
        public string Text {
            get;
            private set;
        }
        /// <summary>
        /// Gets the information about the user
        /// </summary>
        public MessageUserInfo UserInfo {
            get;
            private set;
        }

        internal AddMessageEventArgs(NameValueCollection packet)
            : base(packet) {
            /*
When an actual person speaks in a channel, you get a different message:

SERVER: addmore channel="#general" more="(data about the user)" msg="Hello everyone!"

This command is similar to the addtext action, but it provides more details about the origin and type of the message.  The "more" parameter is formatted with data separated by colons,  with each piece of data escaped with Escape, standard URL escaping:

:Name:UserID:is /me command:is this myself speaking:chat color:gender

Name - The user's name
UserID - The UER User ID that can be used to link to this person's profile.
is /me command - can be 1 or 0. If 1, the /me command was used, and it should be displayed differently, see below.
is this myself speaking - can be 1 or 0. If 1, this message is from myself, and you can choose to show it in a different color to help ID it.
chat color - six characters indicating the user's chosen chat color.
gender - can be 0, 1, or 2.  0 means unknown or other, 1 means male, 2 means female.  Usually the name is shown in blue if male, red if female.

Example:
:Bob:4332:0:0:FF0000:1*/

                this.ChannelName = packet["channel"];
                this.Text = packet["msg"];
                this.UserInfo = new MessageUserInfo(packet["more"]);

        }
    }
}
