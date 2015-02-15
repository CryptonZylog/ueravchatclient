using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Diagnostics;

namespace Crypton.AvChat.Gui.Power.OpWhitelist {

    [Serializable]
    public class Channel {
        public string ChannelName {
            get;
            set;
        }

        public List<User> Users {
            get;
            set;
        }

        public Channel() {
            this.Users = new List<User>();
        }

        public override string ToString() {
            return this.ChannelName;
        }

    }

    [Serializable]
    public class User {
        public string Name {
            get;
            set;
        }

        public override string ToString() {
            return this.Name;
        }
    }

    /// <summary>
    /// Manages Ops Whitelist
    /// </summary>
    internal static class WhitelistManager {

        private static List<Channel> _cache = null;
        /// <summary>
        /// Gets the white list
        /// </summary>
        public static List<Channel> List {
            get {
                if (_cache == null) {
                    _cache = Load();
                }
                return _cache;
            }
        }

        /// <summary>
        /// Gets ops white list, returns new list on any errors
        /// </summary>
        /// <returns></returns>
        public static List<Channel> Load() {
            string fpath = Path.Combine(DirectoryService.UERFolder, "opswhitelist.xml");
            if (File.Exists(fpath)) {
                using (StreamReader sr = new StreamReader(fpath)) {
                    XmlSerializer xs = new XmlSerializer(typeof(List<Channel>));
                    try {
                        List<Channel> list = (List<Channel>)xs.Deserialize(sr);
                        return list ?? new List<Channel>();
                    }
                    catch (Exception any) {
                        Trace.TraceError(any.ToString());
                    }
                }
            }

            return new List<Channel>();
        }

        /// <summary>
        /// Saves channel list
        /// </summary>
        /// <param name="list"></param>
        public static void Save(List<Channel> list) {
            list = list ?? new List<Channel>();
            _cache = list;

            string fpath = Path.Combine(DirectoryService.UERFolder, "opswhitelist.xml");
            if (File.Exists(fpath)) {
                File.Delete(fpath);
            }

            using (StreamWriter sw = new StreamWriter(fpath)) {
                XmlSerializer xs = new XmlSerializer(typeof(List<Channel>));
                xs.Serialize(sw, list);
            }
        }

        public static IEnumerable<Channel> Channels {
            get {
                return Load();
            }
        }

    }


}
