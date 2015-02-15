using Crypton.AvChat.Client;
using mshtml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crypton.AvChat.Client.Events;

namespace Crypton.AvChat.Win.ChatModel
{
    public class ChatUserMessageNode : ChatNode
    {

        public string Username
        {
            get;
            set;
        }

        public string UserColor
        {
            get;
            set;
        }

        public string MessageHtml
        {
            get;
            set;
        }
        
        public UserGenders Gender
        {
            get;
            set;
        }

        public int UserID
        {
            get;
            set;
        }

        public bool IsMe
        {
            get;
            set;
        }

        public bool IsMyMessage
        {
            get;
            set;
        }

        public ChatUserMessageNode() : base() { }

        public ChatUserMessageNode(AddMessageEventArgs messageInfo) : base()
        {
            this.MessageHtml = messageInfo.Text;
            this.UserColor = messageInfo.UserInfo.Color;
            this.Gender = messageInfo.UserInfo.Gender;
            this.IsMe = messageInfo.UserInfo.IsMe;
            this.IsMyMessage = messageInfo.UserInfo.IsMyself;
            this.Username = messageInfo.UserInfo.Name;
            this.UserID = messageInfo.UserInfo.UserID;
        }



        public override void Render(mshtml.HTMLLIElement liNode)
        {
            base.Render(liNode);

            liNode.className = "message-user";

            // get the date element reference
            HTMLSpanElement date = (HTMLSpanElement)liNode.childNodes[0];
            if (this.IsMyMessage)
            {
                date.className += " mine";
            }

            HTMLDocument document = (HTMLDocument)liNode.document;

            if (this.IsMe)
            {
                // star before username
                HTMLSpanElement spanOpenTag = (HTMLSpanElement)document.createElement("SPAN");
                spanOpenTag.className = "tag";
                spanOpenTag.innerText = "* ";
                try
                {
                    spanOpenTag.style.color = this.UserColor;
                }
                catch { } // invalid color
                liNode.appendChild((IHTMLDOMNode)spanOpenTag);

                // username
                HTMLSpanElement spanUsername = (HTMLSpanElement)document.createElement("SPAN");
                spanUsername.className = "username";
                spanUsername.innerText = this.Username;
                spanUsername.setAttribute("data-id", this.UserID);
                try
                {
                    spanUsername.style.color = this.UserColor;
                }
                catch { } // invalid color
                // clicking on username should open a window to view their profile
                (spanUsername as HTMLElementEvents2_Event).onclick += delegate(IHTMLEventObj pEvtObj)
                {
                    HTMLSpanElement eventSourceElement = (HTMLSpanElement)pEvtObj.srcElement;
                    int userId = (int)eventSourceElement.getAttribute("data-id");
                    // TODO: open uer profile page
                    return false;
                };
                liNode.appendChild((IHTMLDOMNode)spanUsername);

                // render /me style message
                // message
                HTMLSpanElement spanText = (HTMLSpanElement)document.createElement("SPAN");
                try
                {
                    spanText.style.color = this.UserColor;
                }
                catch { } // invalid color
                spanText.className = "message me";
                spanText.innerHTML = BrowserController.ReplaceCorrectHtml(this.MessageHtml);
                liNode.appendChild((IHTMLDOMNode)spanText);
            }
            else
            {
                // open tag before username
                HTMLSpanElement spanOpenTag = (HTMLSpanElement)document.createElement("SPAN");
                spanOpenTag.className = "tag";
                spanOpenTag.innerText = "<";
                liNode.appendChild((IHTMLDOMNode)spanOpenTag);

                // username
                HTMLSpanElement spanUsername = (HTMLSpanElement)document.createElement("SPAN");
                spanUsername.className = "username " + (this.Gender == UserGenders.Male ? "male" : this.Gender == UserGenders.Female ? "female" : "unknown");
                spanUsername.innerText = this.Username;
                spanUsername.setAttribute("data-id", this.UserID);
                // clicking on username should open a window to view their profile
                (spanUsername as HTMLElementEvents2_Event).onclick += delegate(IHTMLEventObj pEvtObj)
                {
                    HTMLSpanElement eventSourceElement = (HTMLSpanElement)pEvtObj.srcElement;
                    int userId = (int)eventSourceElement.getAttribute("data-id");
                    // TODO: open uer profile page
                    return false;
                };
                liNode.appendChild((IHTMLDOMNode)spanUsername);

                // close tag
                HTMLSpanElement spanCloseTag = (HTMLSpanElement)document.createElement("SPAN");
                spanCloseTag.className = "tag";
                spanCloseTag.innerText = ">";
                liNode.appendChild((IHTMLDOMNode)spanCloseTag);

                // message
                HTMLSpanElement spanText = (HTMLSpanElement)document.createElement("SPAN");
                try
                {
                    spanText.style.color = this.UserColor;
                }
                catch { } // Invalid color
                spanText.className = "message";
                spanText.innerHTML = BrowserController.ReplaceCorrectHtml(this.MessageHtml);
                liNode.appendChild((IHTMLDOMNode)spanText);
            }
        }


    }
}
