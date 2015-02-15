using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Crypton.AvChat.Win.Themes.Default
{
    [Serializable]
    [XmlRoot("Theme")]
    public class DefaultTheme : Theme
    {
        public DefaultTheme() : base()
        {
            this.Name = "Default Theme";
            this.BasePath = System.IO.Path.Combine(Environment.CurrentDirectory, "Themes", "default");

            this.CssFile = "uer.css";

            this.Images.Add(new ThemeImage(ThemeImage.KEY_TAB_CLOSE) { Normal = "tab_close_fade.png", MouseOver = "tab_close.png", MouseDown = "tab_close_press.png" });

            this.Images.Add(new ThemeImage(ThemeImage.KEY_TAB_CONSOLE) { Normal = "tab_console.png", Notification = "console_message.png" });
            this.Images.Add(new ThemeImage(ThemeImage.KEY_TAB_CHANNEL) { Normal = "channel.png", Notification = "channel_message.png" });
            this.Images.Add(new ThemeImage(ThemeImage.KEY_TAB_PRIVATE) { Normal = "private.png", Notification = "private_message.png" });
        }
    }
}
