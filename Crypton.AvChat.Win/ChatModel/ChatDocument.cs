using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crypton.AvChat.Win.ChatModel
{
    /// <summary>
    /// Used as a container to hold chat messages
    /// </summary>
    public sealed class ChatDocument
    {

        public delegate void ChatDocumentChangeEventArgs(ChatDocument document);
        
        /// <summary>
        /// Fires when a topic is changed
        /// </summary>
        public event ChatDocumentChangeEventArgs OnTopicChanged;

        private string _topic = null;

        /// <summary>
        /// Gets or sets channel topic
        /// </summary>
        public string Topic
        {
            get
            {
                return this._topic;
            }
            set
            {
                this._topic = value;
                if (OnTopicChanged != null)
                {
                    foreach (ChatDocumentChangeEventArgs evt in OnTopicChanged.GetInvocationList())
                    {
                        evt.BeginInvoke(this, null, null);
                    }
                }
            }
        }
        /// <summary>
        /// Gets the list of added chat nodes
        /// </summary>
        public ChatNodeCollection Nodes
        {
            get;
            private set;
        }


        public ChatDocument()
        {
            this.Nodes = new ChatNodeCollection(this);
        }

    }

    public class ChatNodeCollection : IEnumerable<ChatNode>
    {
        private List<ChatNode> nodes = new List<ChatNode>();
        private ChatDocument document = null;
        
        public delegate void ChatNodeAddEventArgs(ChatNode node);

        public event ChatNodeAddEventArgs OnChatNodeAdd;

        public ChatNodeCollection(ChatDocument document)
        {
            this.document = document;
        }

        public void Add(ChatNode node)
        {
            this.nodes.Add(node);
            if (OnChatNodeAdd != null)
            {
                foreach (ChatNodeAddEventArgs evt in OnChatNodeAdd.GetInvocationList())
                {
                    evt.BeginInvoke(node, null, null);
                }
            }
        }

        public IEnumerator<ChatNode> GetEnumerator()
        {
            return nodes.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return nodes.GetEnumerator();
        }
    }
}
