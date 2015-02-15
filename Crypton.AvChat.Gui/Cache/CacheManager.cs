// -----------------------------------------------------------------------
// <copyright file="CacheManager.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Crypton.AvChat.Gui.Cache {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Net;
    using System.IO;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Imaging;

    internal static class CacheManager {

        /// <summary>
        /// Gets the full path to the cache directory
        /// </summary>
        public static string Cache {
            get {
                string dpath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "Cache"));
                if (!Directory.Exists(dpath)) {
                    Directory.CreateDirectory(dpath);
                }
                return dpath;
            }
        }

        /// <summary>
        /// Gets the full path to the Cache/Avatars directory
        /// </summary>
        public static string Avatars {
            get {
                string dpath = Path.GetFullPath(Path.Combine(Cache, "Avatars"));
                if (!Directory.Exists(dpath)) {
                    Directory.CreateDirectory(dpath);
                }
                return dpath;
            }
        }

        /// <summary>
        /// Clears all cache
        /// </summary>
        public static void Clear() {
            DirectoryInfo cacheDir = new DirectoryInfo(Cache);

            FileInfo[] files = cacheDir.GetFiles("*", SearchOption.AllDirectories);
            foreach (FileInfo file in files) {
                try {
                    File.Delete(file.FullName);
                }
                catch (Exception ex) {
                    Trace.TraceError("Cannot clear cache file " + file.Name + " " + ex.ToString());
                }
            }
        }

        /// <summary>
        /// Downloads user avatar and places it in the cache folder, returning the full path to the image file. Null if no avatar is set or user does not exist
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public static Image DownloadAvatar(int UserId, int width = 32, int height = 32) {
            WebClient wc = new WebClient();

            byte[] imageData = null;
            try {
                imageData = wc.DownloadData("http://www.uer.ca/loadavatar.asp?posterid=" + UserId);
            }
            catch {
                wc.Dispose();
                return null;
            }

            string contentType = wc.ResponseHeaders["Content-Type"];

            string ext = "jpg";

            string cachedir = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "Cache"));
            string avCacheDir = Path.GetFullPath(Path.Combine(cachedir, "Avatars"));

            if (!Directory.Exists(cachedir)) {
                Directory.CreateDirectory(cachedir);
            }

            if (!Directory.Exists(avCacheDir)) {
                Directory.CreateDirectory(avCacheDir);
            }

            string fpath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "Cache", "Avatars", UserId.ToString() + "." + ext));

            if (!File.Exists(fpath)) {
                try {
                    File.WriteAllBytes(fpath, imageData);
                }
                catch (Exception ex) {
                    Trace.TraceError("Cannot save cached avatar: " + ex.ToString());
                    return null;
                }
            }

            if (File.Exists(fpath)) {
                // resize
                Image imgOriginal = null;
                using (Image imgFile = Image.FromFile(fpath)) {
                    imgOriginal = imgFile.Clone() as Image;
                }
                using (imgOriginal) {
                    double scaleWidthFactor = (double)imgOriginal.Width / (double)width;
                    double scaleHeightFactor = (double)imgOriginal.Height / (double)height;

                    Bitmap resized = new Bitmap(width, height);
                    using (Graphics g = Graphics.FromImage(resized)) {
                        g.Clear(Color.Transparent);

                        int drawWidth = 0;
                        int drawHeight = 0;
                        drawWidth = imgOriginal.Width;
                        drawHeight = imgOriginal.Height;

                        if (imgOriginal.Width > imgOriginal.Height) {
                            // landscape
                            drawWidth = (int)((double)imgOriginal.Width / scaleWidthFactor);
                            drawHeight = (int)((double)imgOriginal.Height / scaleWidthFactor);
                        }
                        else {
                            // portrait
                            drawWidth = (int)((double)imgOriginal.Width / scaleHeightFactor);
                            drawHeight = (int)((double)imgOriginal.Height / scaleHeightFactor);
                        }

                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        g.DrawImage(imgOriginal, (int)(((double)width - (double)drawWidth) / 2.0), (int)(((double)height - (double)drawHeight) / 2.0), drawWidth, drawHeight);
                        g.Flush();
                    }
                    return resized;

                }
            }

            return null;
        }

    }
}
