using mshtml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Crypton.AvChat.Win.ChatModel
{

    [XmlInclude(typeof(ChatStatusMessageNode))]
    [XmlInclude(typeof(ChatUserMessageNode))]
    public abstract class ChatNode
    {
        [XmlAttribute("date")]
        public DateTime DateAdded
        {
            get;
            set;
        }

        public ChatNode() {
            this.DateAdded = DateTime.Now;
        }

        /// <summary>
        /// Renders a time stamp in list item
        /// </summary>
        /// <param name="liNode"></param>
        public virtual void Render(HTMLLIElement liNode)
        {
            // add date
            HTMLDocument document = liNode.document;

            HTMLSpanElement dateSpan = (HTMLSpanElement)document.createElement("SPAN");
            dateSpan.className = "date";
            //TODO: quote on click
            dateSpan.innerText = string.Format("{0:00}:{1:00}:{2:00}", this.DateAdded.Hour, this.DateAdded.Minute, this.DateAdded.Second);
            liNode.appendChild((IHTMLDOMNode)dateSpan);
        }

    }
}
