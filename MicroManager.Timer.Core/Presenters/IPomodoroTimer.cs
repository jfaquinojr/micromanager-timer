using System;
using System.Collections.Generic;
using MicroManager.Timer.Core.Models;

namespace MicroManager.Timer.Core.Presenters
{
    public interface IPomodoroTimer
    {
        event EventHandler LongBreakStarted;
        event EventHandler LongBreakStopped;
        event EventHandler ShortBreakStarted;
        event EventHandler ShortBreakStopped;
        event EventHandler TaskStarted;
        event EventHandler TaskStopped;
        event EventHandler Tick;
        event EventHandler TickMinute;

        PomodoroTask AddTask(string name, int pomodoros, string description);

        void Start();
        void Stop(bool interrupt = true);
    }
}