using System;
using System.Collections.Generic;
using System.Text;
using MicroManager.Timer.Core.Models;

namespace MicroManager.Timer.Core.Plugins
{
    public class ClockTickingSound : IPomodoroPlugin
    {
        private bool tiktok = true;

        public int ExecuteLongBreakStart(PomodoroTask task)
        {
            return 0;
        }

        public int ExecuteLongBreakStop(PomodoroTask task)
        {
            return 0;
        }

        public int ExecuteMinute(PomodoroTask task)
        {
            return 0;
        }

        public int ExecuteShortBreakStart(PomodoroTask task)
        {
            return 0;
        }

        public int ExecuteShortBreakStop(PomodoroTask task)
        {
            return 0;
        }

        public int ExecuteTaskStart(PomodoroTask task)
        {
            //WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
            //System.Media.SoundPlayer o;

            //wplayer.URL = "My MP3 file.mp3";
            //wplayer.Controls.Play();
            return 0;
        }

        public int ExecuteTaskStop(PomodoroTask task)
        {
            return 0;
        }

        public int ExecuteTick(PomodoroTask task)
        {
            return 0;
        }
    }
}
