using MicroManager.Timer.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroManager.Timer.Core.Plugins
{
    public interface IPomodoroPlugin
    {
        int ExecuteTick(PomodoroTask task);
        int ExecuteMinute(PomodoroTask task);

        int ExecuteTaskStart(PomodoroTask task);
        int ExecuteTaskStop(PomodoroTask task);

        int ExecuteLongBreakStart(PomodoroTask task);
        int ExecuteLongBreakStop(PomodoroTask task);

        int ExecuteShortBreakStart(PomodoroTask task);
        int ExecuteShortBreakStop(PomodoroTask task);

    }
}
