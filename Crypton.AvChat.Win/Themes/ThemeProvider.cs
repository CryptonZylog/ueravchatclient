using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Crypton.AvChat.Win.Themes
{
    public static class ThemeProvider
    {

        public static Theme Load(string name)
        {
            string filepath = Path.Combine(Environment.CurrentDirectory, "Themes", name, "theme.xml");
            if (!File.Exists(filepath))
            {
                throw new FileNotFoundException("Theme file does not exist: " + filepath);
            }

            Theme theme = null;
            XmlSerializer xs = new XmlSerializer(typeof(Theme));
            using (FileStream fs = File.OpenRead(filepath))
            {
                try
                {
                    theme = (Theme)xs.Deserialize(fs);
                }
                catch (Exception any)
                {
                    throw new InvalidOperationException("Failed to load theme: " + name, any);
                }
            }
            theme.SetBasePath(Path.GetDirectoryName(filepath));
            theme.OnDeserialize();
            return theme;
        }


    }

}
