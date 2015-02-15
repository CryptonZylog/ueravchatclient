using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Crypton.AvChat.Win.History
{
    public class XmlHistoryProvider : IHistoryProvider
    {

        private string filepath = null;

        public XmlHistoryProvider()
        {
        }

        public ChatModel.ChatDocument Document
        {
            get;
            set;
        }

        public DateTime Date
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        private string initializeLogFilePath()
        {
            string historyDirectory = HistoryManager.HistoryDirectory;
            // current chat user
            string chatUser = Path.Combine(historyDirectory, ChatDispatcher.Singleton.Username ?? "other");
            if (!Directory.Exists(chatUser))
                Directory.CreateDirectory(chatUser);
            // get current named directory
            string nameDirectory = Path.Combine(chatUser, this.Name);
            if (!Directory.Exists(nameDirectory))
                Directory.CreateDirectory(nameDirectory);
            // create dated file
            string filename = string.Format("{0:0000}-{1:00}-{2:00}.{3:00}{4:00}{5:00}.xml", DateTime.Now.Year,
                DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            string dateFile = Path.Combine(nameDirectory, filename);
            return dateFile;
        }

        private StreamWriter AllocateLogFile()
        {
            StreamWriter output;
            do
            {
                try
                {
                    output = new StreamWriter(this.filepath, false, Encoding.UTF8);
                    break;
                }
                catch { }
            }
            while (true);
            return output;
        }

        public void BeginHistoryLog()
        {
            if (Document == null)
                throw new InvalidOperationException("ChatDocument is required (Document property)");
            if (string.IsNullOrEmpty(this.Name))
                throw new InvalidOperationException("Name is required (Name property)");
            this.Date = DateTime.Now;
            // init destination file stream
            this.filepath = initializeLogFilePath();
            
            Document.Nodes.OnChatNodeAdd += Nodes_OnChatNodeAdd;
        }

        private void Nodes_OnChatNodeAdd(ChatModel.ChatNode node)
        {
            this.Flush();
        }

        private XmlElement createDocumentElement(XmlDocument historyDocument)
        {
            XmlDeclaration xdec = historyDocument.CreateXmlDeclaration("1.0", null, null);
            historyDocument.AppendChild(xdec);

            XmlElement xd = historyDocument.CreateElement("History");

            XmlAttribute xaDate = historyDocument.CreateAttribute("Date");
            xaDate.Value = this.Date.ToString(CultureInfo.InvariantCulture);
            xd.Attributes.Append(xaDate);

            XmlAttribute xaName = historyDocument.CreateAttribute("Name");
            xaName.Value = this.Name;
            xd.Attributes.Append(xaName);

            historyDocument.AppendChild(xd);
            return xd;
        }

        private void saveNode(ChatModel.ChatNode node, XmlElement historyElement, XmlDocument historyDocument)
        {
            XmlElement xe = historyDocument.CreateElement("Entry");
            using (StringWriter sw = new StringWriter())
            {
                XmlSerializer xs = new XmlSerializer(typeof(ChatModel.ChatNode));
                xs.Serialize(sw, node);

                string rawXml = sw.ToString();
                int xdecEndIdx = rawXml.IndexOf("\r\n");

                xe.InnerXml = rawXml.Substring(xdecEndIdx);
            }
            historyElement.AppendChild(xe);
        }

        public void EndHistoryLog()
        {
            this.Flush();
            Document.Nodes.OnChatNodeAdd -= Nodes_OnChatNodeAdd;
        }


        public void Flush()
        {
            try
            {
                XmlDocument historyDocument = new XmlDocument();
                XmlElement historyElement = this.createDocumentElement(historyDocument);
                foreach (var node in this.Document.Nodes.ToArray())
                {
                    this.saveNode(node, historyElement, historyDocument);
                }
                using (StreamWriter output = this.AllocateLogFile())
                {
                    historyDocument.Save(output);
                }
            }
            catch { }
        }
    }
}
