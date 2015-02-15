// -----------------------------------------------------------------------
// <copyright file="XmlPacketBuilder.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Crypton.AvChat.Client {
    using System;
    using System.Collections.Generic;
    
    using System.Text;
    using System.Xml;
    using System.Collections.Specialized;
    using System.IO;
    using System.Web;

    internal static class XmlPacketBuilder {
        /// <summary>
        /// Builds a low level XML packet
        /// </summary>
        /// <param name="data">Collection of data</param>
        /// <returns></returns>
        public static string CreatePacket(NameValueCollection data) {
            if (data == null) {
                throw new ArgumentNullException("data");
            }
            StringBuilder sbOutput = new StringBuilder();
            XmlWriter w = XmlWriter.Create(sbOutput, new XmlWriterSettings { ConformanceLevel = ConformanceLevel.Fragment, OmitXmlDeclaration = true });
            w.WriteStartElement("data");

            string[] keys = data.AllKeys;
            for (int i =0; i < keys.Length; i++) {
                w.WriteAttributeString(keys[i], data[i]);
            }

            w.WriteEndElement();
            w.Close();

            string finalString = sbOutput.ToString();

            // replace Encoded newlines with actual CR
            finalString = finalString.Replace("&#xD;", "\r").Replace("&#xA;", "").Replace("&#x9;", "\t");

            return finalString;
        }
        /// <summary>
        /// Reads packet from XML encoded string, returns null if unable to decode data
        /// </summary>
        /// <param name="data">Data to read</param>
        /// <returns></returns>
        public static NameValueCollection ReadPacket(string data) {
            if (data == null) {
                throw new ArgumentNullException("data");
            }
            NameValueCollection nvc = new NameValueCollection();
            using (StringReader s = new StringReader(data)) {
                XmlReader r = XmlReader.Create(s, new XmlReaderSettings { ConformanceLevel = ConformanceLevel.Fragment, ValidationFlags = System.Xml.Schema.XmlSchemaValidationFlags.None, ValidationType = ValidationType.None });

                try {
                    if (r.Read()) {
                        // first and only node
                        if (r.Name.ToLowerInvariant() == "data") {
                            // data node
                            for (int i = 0; i < r.AttributeCount; i++) {
                                r.MoveToAttribute(i);
                                string name = r.Name;
                                string value = r.GetAttribute(i);

                                nvc[name] = value;
                            }
                        }
                    }
                }
                catch (XmlException) {
                    return null;
                }

                r.Close();
            }
            return nvc;
        }
    }
}
