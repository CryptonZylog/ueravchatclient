using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Interop;
using Crypton.AvChat.Win.Tabs;
using Crypton.AvChat.Win.Win32;

namespace Crypton.AvChat.Win
{

    public enum NotificationModes
    {
        Disabled = 0,
        Watchwords = 1,
        PrivateTabs = 2,
        InactiveWindow = 3,
        Always = 4
    }

    public enum NotificationSourceTypes
    {
        Console,
        Channel,
        Private,
        Watchword
    }

    static class NotificationService
    {

        static NotificationService()
        {
        }

        private static void playAudio(string filename)
        {
            string filepath = Path.Combine(Environment.CurrentDirectory, "NotificationAudio", filename + ".wav");
            if (File.Exists(filepath))
            {
                System.Windows.Media.MediaPlayer mp = new System.Windows.Media.MediaPlayer();
                mp.Open(new Uri(filepath));
                mp.Play();                
            }
        }

        private static void flashWindow(Crypton.AvChat.Win.AppWindows.ChatWindow window)
        {
            if (Properties.Settings.Default.NotificationFlashWindow && !window.IsActiveWindow)
            {
                // flash user window
                FLASHWINFO fInfo = new FLASHWINFO();

                fInfo.cbSize = Convert.ToUInt32(Marshal.SizeOf(fInfo));
                fInfo.hwnd = new WindowInteropHelper(window).Handle;
                fInfo.dwFlags = FLASHW_FLAGS.FLASHW_ALL | FLASHW_FLAGS.FLASHW_TIMERNOFG;
                fInfo.uCount = 4;
                fInfo.dwTimeout = 0;

                Win32Impl.FlashWindowEx(ref fInfo);
            }
        }

        private static void notifyPrivate(IChatTab sourceTab, bool isMyMessage = false)
        {
            if (sourceTab.ParentChatWindow.IsActiveTab(sourceTab) == false)
            {
                // new message received, tab not active
                playAudio("bing3");
            }
            else
            {
                if (isMyMessage)
                {
                    if (Properties.Settings.Default.NotificationEnableOwn)
                    {
                        playAudio("bing1");
                    }
                }
                else
                {
                    playAudio("bing1");
                }
            }
        }

        private static void notifyConsole(IChatTab sourceTab)
        {
            if (sourceTab.ParentChatWindow.IsActiveTab(sourceTab) == false)
            {
                playAudio("request");
            }
        }

        private static void notifyChannel(IChatTab sourceTab, bool isMyMessage = false)
        {
            if (sourceTab.ParentChatWindow.IsActiveTab(sourceTab))
            {
                if (isMyMessage)
                {
                    if (Properties.Settings.Default.NotificationEnableOwn)
                    {
                        playAudio("bing1");
                    }
                }
                else
                {
                    playAudio("bing1");
                }
            }
            else
            {
                playAudio("bing2");
            }
        }

        public static void NotifyNewMessage(NotificationSourceTypes source, IChatTab sourceTab, bool isMyMessage = false)
        {
            NotificationModes mode = (NotificationModes)Properties.Settings.Default.NotificationMode;
            if (mode == NotificationModes.Disabled)
                return; // do nothing
            if (mode == NotificationModes.Watchwords && (source & NotificationSourceTypes.Watchword) == NotificationSourceTypes.Watchword)
            {
                playAudio("accepted");
                flashWindow(sourceTab.ParentChatWindow);
                return;
            }
            switch (mode)
            {
                case NotificationModes.InactiveWindow:
                    // notify only for active windows
                    if (sourceTab.ParentChatWindow.IsActiveWindow == false)
                    {
                        switch (source)
                        {
                            case NotificationSourceTypes.Channel:
                                notifyChannel(sourceTab, isMyMessage);
                                break;
                            case NotificationSourceTypes.Console:
                                notifyConsole(sourceTab);
                                break;
                            case NotificationSourceTypes.Private:
                                notifyPrivate(sourceTab, isMyMessage);
                                break;
                        }
                    }
                    break;
                case NotificationModes.PrivateTabs:
                    // notify only on private tabs
                    if (source == NotificationSourceTypes.Private)
                    {
                        notifyPrivate(sourceTab, isMyMessage);
                    }
                    break;
                case NotificationModes.Always:
                    // either
                    switch (source)
                    {
                        case NotificationSourceTypes.Channel:
                            notifyChannel(sourceTab, isMyMessage);
                            break;
                        case NotificationSourceTypes.Console:
                            notifyConsole(sourceTab);
                            break;
                        case NotificationSourceTypes.Private:
                            notifyPrivate(sourceTab, isMyMessage);
                            break;
                    }
                    break;
            }
            if ((source & NotificationSourceTypes.Watchword) == NotificationSourceTypes.Watchword)
            {
                playAudio("accepted");
            }
            flashWindow(sourceTab.ParentChatWindow);
        }

        public static void NotifyDogSound(int dogNumber)
        {
            if (Properties.Settings.Default.NotificationEnableDog)
            {
                string format = string.Format("dog{0}", dogNumber);
                playAudio(format);
            }
        }

        public static void NotifyUserJoined(IChatTab sourceTab)
        {
            playAudio("join");
        }

        public static void NotifyAlert()
        {
            playAudio("alert");            
        }
    }
}
