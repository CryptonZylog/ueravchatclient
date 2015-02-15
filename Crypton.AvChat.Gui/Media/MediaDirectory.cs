

namespace Crypton.AvChat.Gui.Media {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Drawing;
    using System.IO;

    internal abstract class MediaDirectory {

        protected abstract string BaseDirectory {
            get;
        }

        protected Image GetImage(string filename) {
            string path = Path.GetFullPath(Path.Combine(this.BaseDirectory, filename));

            if (!File.Exists(path)) {
                return null;
            }

            Image img = null;
            try {
                img = Image.FromFile(path);
            }
            catch {
                img = null;
            }
            return img;
        }

    }
}
