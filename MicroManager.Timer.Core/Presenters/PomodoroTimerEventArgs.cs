using MicroManager.Timer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroManager.Timer.Core.Presenters
{
    class PomodoroTimerEventArgs: EventArgs
    {
        public PomodoroTimerEventArgs(PomodoroTask currentTask)
        {

        }
    }
}
