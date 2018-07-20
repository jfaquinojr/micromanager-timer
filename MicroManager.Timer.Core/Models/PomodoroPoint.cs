using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroManager.Timer.Core.Models
{
    public class PomodoroPoint
    {
        public bool IsPlanned { get; private set; }
        public bool IsInterrupted { get; set; }
        public DateTime? StartedTime { get; set; }
        public DateTime? StoppedTime { get; set; }

        public PomodoroPoint()
        {
        }

        public PomodoroPoint(bool planned)
        {
            IsPlanned = planned;
        }
    }
}
