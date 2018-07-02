using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroManager.WinForms.Models
{
    class PomodoroPoint
    {
        public bool IsPlanned { get; private set; }
        public bool IsInterrupted { get; set; }

        public PomodoroPoint()
        {
        }

        public PomodoroPoint(bool planned)
        {
            IsPlanned = planned;
        }
    }
}
