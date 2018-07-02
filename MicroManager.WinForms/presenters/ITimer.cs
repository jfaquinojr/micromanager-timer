using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MicroManager.WinForms.presenters
{
    interface ITimer
    {
        double Interval { get; set; }
        event ElapsedEventHandler Elapsed;
        bool Enabled { get; set; }
    }
}
