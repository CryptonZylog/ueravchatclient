// -----------------------------------------------------------------------
// <copyright file="SoundDirectory.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Crypton.AvChat.Gui.Media.Sound {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;

    internal class SoundDirectory : MediaDirectory {

        public enum SoundTypes {
            Alert,
            MessageReceivedOtherChannel,
            MessageReceivedThisChannel,
            MessageReceivedPrivate,
            Join,
            Watchword
        }

        protected override string BaseDirectory {
            get { return Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "Media", "Sound")); }
        }

        public string GetSoundFile(SoundTypes type) {
            switch (type) {
                case SoundTypes.Alert:
                    return "alert.wav";
                case SoundTypes.MessageReceivedOtherChannel:
                    return "bing2.wav";
                case SoundTypes.MessageReceivedThisChannel:
                    return "bing1.wav";
                case SoundTypes.MessageReceivedPrivate:
                    return "bing3.wav";
                case SoundTypes.Join:
                    return "join.wav";
                case SoundTypes.Watchword:
                    return "accepted.wav";
                default:
                    return null;
            }
        }

        public string GetDogSound(int num) {
            string fpath = Path.Combine(BaseDirectory, string.Format("dog{0}.wav", num));
            if (File.Exists(fpath)) {
                return fpath;
            }
            return null;
        }

        public void PlaySound(SoundTypes type) {
            Action<SoundTypes> dg = new Action<SoundTypes>(PlaySoundImpl);
            dg.BeginInvoke(type, null, null);
        }
        private void PlaySoundImpl(SoundTypes type) {
            string fname = GetSoundFile(type);
            if (fname != null) {
                string fpath = Path.Combine(BaseDirectory, fname);
                if (File.Exists(fpath)) {
                    using (System.Media.SoundPlayer player = new System.Media.SoundPlayer(fpath)) {
                        player.PlaySync();
                    }
                }
            }
        }
    }
}
