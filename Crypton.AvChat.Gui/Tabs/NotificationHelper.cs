using Crypton.AvChat.Gui.Media;
using Crypton.AvChat.Gui.Properties;
using Crypton.AvChat.Gui.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Crypton.AvChat.Gui.Tabs {
    static class NotificationHelper {

        public static void NotifyMessageReceived(bool isActiveTab = false, bool isPrivateTab = false, bool isWatchWord = false, bool isOwn = false) {
            MainForm frmMain = Application.OpenForms.Cast<Form>().FirstOrDefault(f => f is MainForm) as MainForm;
            bool isActiveForm = Win32Impl.GetForegroundWindow() == frmMain.Handle;

            if (Notifications.Default.Mode == NotificationTypes.Disabled)
                return;
            if (frmMain.IsAway && Notifications.Default.DisableWhenAway)
                return;

            if (
                Notifications.Default.Mode == NotificationTypes.Always || (
                (Notifications.Default.Mode == NotificationTypes.NotifyWatchwords && isWatchWord) ||
                (Notifications.Default.Mode == NotificationTypes.NotifyPrivateTabs && isPrivateTab) ||
                (Notifications.Default.Mode == NotificationTypes.NotifyInactive && (isActiveTab == false || isActiveForm == false))
                )) {

                if (Notifications.Default.FlashWindowTaskbar && !isActiveForm) {
                    if (!frmMain.activeFlashState) {
                        FlashWindow();
                    }
                }

                if (isOwn && Notifications.Default.EnableOwnSounds) {
                    if (isActiveForm) {
                        if (isActiveTab) {
                            MediaManager.Sounds.PlaySound(Media.Sound.SoundDirectory.SoundTypes.MessageReceivedThisChannel);
                        }
                        else {
                            MediaManager.Sounds.PlaySound(Media.Sound.SoundDirectory.SoundTypes.MessageReceivedOtherChannel);
                        }
                    }
                }
                else if (!isOwn && Notifications.Default.EnableSounds) {
                    if (isWatchWord) {
                        MediaManager.Sounds.PlaySound(Media.Sound.SoundDirectory.SoundTypes.Watchword);
                    }
                    else {
                        if (isActiveForm) {
                            // active form 
                            if (isActiveTab) {
                                //current tab
                                if (isPrivateTab) {
                                    MediaManager.Sounds.PlaySound(Media.Sound.SoundDirectory.SoundTypes.MessageReceivedPrivate);
                                }
                                else {
                                    MediaManager.Sounds.PlaySound(Media.Sound.SoundDirectory.SoundTypes.MessageReceivedThisChannel);
                                }
                            }
                            else {
                                if (isPrivateTab) {
                                    MediaManager.Sounds.PlaySound(Media.Sound.SoundDirectory.SoundTypes.MessageReceivedPrivate);
                                }
                                else {
                                    MediaManager.Sounds.PlaySound(Media.Sound.SoundDirectory.SoundTypes.MessageReceivedOtherChannel);
                                }
                            }
                        }
                        else {
                            // inactive form
                            if (isPrivateTab) {
                                MediaManager.Sounds.PlaySound(Media.Sound.SoundDirectory.SoundTypes.MessageReceivedPrivate);
                            }
                            else {
                                MediaManager.Sounds.PlaySound(Media.Sound.SoundDirectory.SoundTypes.MessageReceivedOtherChannel);
                            }
                        }
                    }
                }
            }
        }

        public static void FlashWindow() {
            MainForm frmMain = Application.OpenForms.Cast<Form>().FirstOrDefault(f => f is MainForm) as MainForm;
            // flash taskbar
            FLASHWINFO fInfo = new FLASHWINFO();

            fInfo.cbSize = Convert.ToUInt32(Marshal.SizeOf(fInfo));
            fInfo.hwnd = frmMain.Handle;
            fInfo.dwFlags = FLASHW_FLAGS.FLASHW_ALL | FLASHW_FLAGS.FLASHW_TIMERNOFG;
            fInfo.uCount = 4;
            fInfo.dwTimeout = 0;

            Win32Impl.FlashWindowEx(ref fInfo);

            frmMain.activeFlashState = true;
        }

    }
}
