using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Crypton.AvChat.Win
{
    public static class OpsWhitelistService
    {
        [Serializable]
        public class Channel
        {
            public string ChannelName
            {
                get;
                set;
            }

            public List<User> Users
            {
                get;
                set;
            }

            public Channel()
            {
                this.Users = new List<User>();
            }

            public override string ToString()
            {
                return this.ChannelName;
            }
        }
        [Serializable]
        public class User
        {
            public string Name
            {
                get;
                set;
            }

            public override string ToString()
            {
                return this.Name;
            }
        }

        private static List<Channel> _channelList = null;

        static OpsWhitelistService()
        {
            _channelList = Load();
        }

        /// <summary>
        /// Gets ops white list, returns new list on any errors
        /// </summary>
        /// <returns></returns>
        private static List<Channel> Load()
        {
            string fpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UER", "opswhitelist.xml");
            if (File.Exists(fpath))
            {
                using (StreamReader sr = new StreamReader(fpath))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(List<Channel>));
                    try
                    {
                        List<Channel> list = (List<Channel>)xs.Deserialize(sr);
                        return list ?? new List<Channel>();
                    }
                    catch (Exception any)
                    {
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
        private static void SaveXml(List<Channel> list)
        {
            string fpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UER", "opswhitelist.xml");
            if (File.Exists(fpath))
            {
                File.Delete(fpath);
            }

            using (StreamWriter sw = new StreamWriter(fpath))
            {
                XmlSerializer xs = new XmlSerializer(typeof(List<Channel>));
                xs.Serialize(sw, list);
            }
        }

        public static void Save(List<Channel> list)
        {
            _channelList = list;
            SaveXml(_channelList);
        }

        public static bool Match(string channelName, string username)
        {
            var chan = _channelList.FirstOrDefault(c => c.ChannelName == channelName);
            if (chan != null)
            {
                return chan.Users.Any(u => username.StartsWith(u.Name));
            }
            return false;
        }

        public static IEnumerable<Channel> GetChannelList()
        {
            return _channelList.AsReadOnly();
        }

        public static void Remove(string channelName, string username)
        {
            var chan = _channelList.FirstOrDefault(c => c.ChannelName == channelName);
            if (chan != null)
            {
                var user = chan.Users.FirstOrDefault(u => u.Name == username);
                if (user != null)
                {
                    chan.Users.Remove(user);
                }
                if (chan.Users.Count == 0)
                    _channelList.Remove(chan);
            }
        }

        public static void Add(string channelName, string username)
        {
            var chan = _channelList.FirstOrDefault(c => c.ChannelName == channelName);
            if (chan == null)
            {
                chan = new Channel();
                _channelList.Add(chan);
            }

            var user = chan.Users.FirstOrDefault(u => u.Name == username);
            if (user == null)
            {
                user = new User();
                user.Name = username;
                chan.Users.Add(user);
            }
        }

    }
}
