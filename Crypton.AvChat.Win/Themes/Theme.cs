using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Crypton.AvChat.Win.Themes
{
    [Serializable]
    public class Theme
    {

        [XmlIgnore]
        public string BasePath
        {
            get;
            protected set;
        }


        [XmlAttribute]
        public string Name
        {
            get;
            set;
        }

        [XmlAttribute]
        public Version MinVersion
        {
            get;
            set;
        }

        [XmlAttribute]
        public Version MaxVersion
        {
            get;
            set;
        }

        public string Author
        {
            get;
            set;
        }

        public string Website
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public ImageCollection Images
        {
            get;
            set;
        }

        public string CssFile
        {
            get;
            set;
        }

        public Theme()
        {
            this.Images = new ImageCollection();
            this.Images.SetTheme(this);
        }

        public virtual void SetBasePath(string path)
        {
            this.BasePath = path;
        }

        public virtual void OnDeserialize()
        {
            if (this.Images != null)
            {
                this.Images.SetTheme(this);
            }
        }

        public virtual string GetCss()
        {
            if (!string.IsNullOrEmpty(this.CssFile))
            {
                string path = System.IO.Path.Combine(this.BasePath, CssFile);
                if (System.IO.File.Exists(path))
                {
                    return System.IO.File.ReadAllText(path);
                }
            }
            return null;
        }
    }
}
