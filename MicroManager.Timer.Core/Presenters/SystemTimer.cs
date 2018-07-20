using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MicroManager.Timer.Core.Presenters
{
    public class SystemTimer : ITimer
    {
        private System.Timers.Timer _timer;

        public double Interval { get => _timer.Interval; set => _timer.Interval = value; }
        public bool Enabled { get => _timer.Enabled; set => _timer.Enabled = value; }

        public event ElapsedEventHandler Elapsed;

        public SystemTimer()
        {
            _timer = new System.Timers.Timer();
            _timer.Elapsed += _timer_Elapsed;
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Elapsed(sender, e);
        }
    }
}
