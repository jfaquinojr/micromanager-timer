using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MicroManager.WinForms.presenters
{
    class SystemTimer : ITimer
    {
        private Timer _timer;

        public double Interval { get => _timer.Interval; set => _timer.Interval = value; }
        public bool Enabled { get => _timer.Enabled; set => _timer.Enabled = value; }

        public event ElapsedEventHandler Elapsed;

        public SystemTimer()
        {
            _timer = new Timer();
            _timer.Elapsed += _timer_Elapsed;
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Elapsed(sender, e);
        }
    }
}
