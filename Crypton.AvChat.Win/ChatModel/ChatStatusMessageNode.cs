using mshtml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crypton.AvChat.Client.Events;
using System.Xml.Serialization;

namespace Crypton.AvChat.Win.ChatModel
{
    public class ChatStatusMessageNode : ChatNode
    {

        public string Html
        {
            get;
            set;
        }

        [XmlIgnore]
        public string Text
        {
            get { return System.Net.WebUtility.HtmlDecode(this.Html); }
            set { this.Html = System.Net.WebUtility.HtmlEncode(value); }
        }

        public ChatStatusMessageNode() : base() { }

        public ChatStatusMessageNode(AddStatusEventArgs chatEvent) : base()
        {
            this.Html = chatEvent.Text;
        }

        public ChatStatusMessageNode(AddTextEventArgs chatEvent)
            : base()
        {
            this.Html = chatEvent.Text;
        }

        public override void Render(mshtml.HTMLLIElement liNode)
        {
            base.Render(liNode);

            liNode.className = "message-status";
            
            HTMLDocument document = (HTMLDocument)liNode.document;
            HTMLSpanElement spanText = (HTMLSpanElement)document.createElement("SPAN");

            spanText.className = "message";
            spanText.innerHTML = BrowserController.ReplaceCorrectHtml(this.Html);
            liNode.appendChild((IHTMLDOMNode)spanText);
        }

    }
}
