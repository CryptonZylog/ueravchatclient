using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace Crypton.AvChat.Win.Themes
{
    [Serializable]
    public class ImageCollection : List<ThemeImage>
    {
        private IDictionary<string, ThemeImage> localMap = null;

        [XmlIgnore]
        public Theme Theme
        {
            get;
            private set;
        }


        public void SetTheme(Theme theme)
        {
            this.Theme = theme;
            foreach (var image in this)
            {
                image.SetTheme(theme);
            }
        }

        public new void Add(ThemeImage item)
        {
            item.SetTheme(this.Theme);
            base.Add(item);
        }

        public BitmapImage GetImage(string imageKey, ThemeImage.ImageTypes imageType)
        {
            if (localMap == null)
            {
                localMap = this.ToDictionary(x => x.Key);
            }
            if (this.localMap.ContainsKey(imageKey))
            {
                string imagePath = this.localMap[imageKey].GetPath(imageType);
                if (File.Exists(imagePath))
                {
                    return new BitmapImage(new Uri(imagePath));
                }
            }
            return null;
        }

    }

    [Serializable]
    public class ThemeImage
    {

        public enum ImageTypes
        {
            Normal,
            MouseOver,
            MouseDown,
            Notification
        }

        #region Image Keys
        public const string KEY_TAB_CLOSE = "BTN_TAB_CLOSE";
        public const string KEY_TAB_CONSOLE = "TAB_TYPE_CONSOLE";
        public const string KEY_TAB_CHANNEL = "TAB_TYPE_CHANNEL";
        public const string KEY_TAB_PRIVATE = "TAB_TYPE_PRIVATE";
        #endregion
        
        [XmlAttribute]
        public string Key
        {
            get;
            set;
        }

        public string Normal
        {
            get;
            set;
        }

        public string MouseOver
        {
            get;
            set;
        }

        public string MouseDown
        {
            get;
            set;
        }

        public string Notification
        {
            get;
            set;
        }
        
        [XmlIgnore]
        public Theme Theme
        {
            get;
            private set;
        }

        public ThemeImage() { }
        public ThemeImage(string key)
        {
            this.Key = key;
        }

        public virtual void SetTheme(Theme theme)
        {
            this.Theme = theme;
        }

        public virtual string GetPath(ImageTypes imageType)
        {
            switch (imageType)
            {
                case ImageTypes.Normal:
                    return Path.Combine(this.Theme.BasePath, this.Normal);
                case ImageTypes.MouseOver:
                    return Path.Combine(this.Theme.BasePath, this.MouseOver ?? this.Normal);
                case ImageTypes.MouseDown:
                    return Path.Combine(this.Theme.BasePath, this.MouseDown ?? this.Normal);
                case ImageTypes.Notification:
                    return Path.Combine(this.Theme.BasePath, this.Notification ?? this.Normal);                    
            }
            return null;
        }

        public virtual Uri GetUri(ImageTypes imageType)
        {
            string path = this.GetPath(imageType);
            if (!string.IsNullOrEmpty(path))
            {
                return new Uri(path);
            }
            else
            {
                return null;
            }
        }
        
    }

}
