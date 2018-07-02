using MicroManager.WinForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MicroManager.WinForms.presenters
{
    class PomodoroTimer
    {
        public event EventHandler Tick;
        public event EventHandler TaskStarted;
        public event EventHandler TaskStopped;
        public event EventHandler ShortBreakStarted;
        public event EventHandler ShortBreakStopped;
        public event EventHandler LongBreakStarted;
        public event EventHandler LongBreakStopped;

        public Clock Clock { get; set; }
        public List<PomodoroTask> Tasks { get; set; } = new List<PomodoroTask>();
        public PomodoroTask CurrentTask { get; set; }

        private ITimer _timer;
        //private int _pomodoroMins;
        //private int _shortBreakMins;
        //private int _longBreakMins;
        private bool _isBreak;

        private IPomodoroConfiguration _config;


        public PomodoroTimer(IPomodoroConfiguration config, ITimer timer)
        {
            _config = config;
            _timer = timer;
            _timer.Interval = 1000;
            _timer.Elapsed += _timer_Elapsed;
        }

        public void Start()
        {
            if(CurrentTask == null)
            {
                throw new InvalidOperationException("Cannot start without choosing a Pomodoro Task first");
            }

            _timer.Enabled = true;
            Clock = new Clock(GetCurrentDuration(CurrentTask.ConsumedPoints, _isBreak));
            
            if (_isBreak)
            {
                if (CurrentTask.ConsumedPoints % 4 == 0)
                {
                    LongBreakStarted?.Invoke(this, new PomodoroTimerEventArgs(CurrentTask));
                }
                else
                {
                    ShortBreakStarted?.Invoke(this, new PomodoroTimerEventArgs(CurrentTask));
                }
            }
            else
            {
                TaskStarted?.Invoke(this, new PomodoroTimerEventArgs(CurrentTask));
            }
        }

        private int GetCurrentDuration(int pomodoros, bool isBreak)
        {
            // TODO: use _config to pull values
            // 15 mins long break
            // 5 mins short break
            // 25 mins pomodoro duration
            if(isBreak)
            {
                return pomodoros % 4 == 0? 15 : 5;
            }

            return 25;
        }

        public void End()
        {
            _isBreak = !_isBreak;
            _timer.Enabled = false;
            if(_isBreak)
            {
                if(CurrentTask.ConsumedPoints % 4 == 0)
                {
                    LongBreakStopped?.Invoke(this, new PomodoroTimerEventArgs(CurrentTask));
                } else
                {
                    ShortBreakStopped?.Invoke(this, new PomodoroTimerEventArgs(CurrentTask));
                }
            } else
            {
                TaskStopped?.Invoke(this, new PomodoroTimerEventArgs(CurrentTask));
            }
            
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Clock.Decrement();
            Tick?.Invoke(this, new PomodoroTimerEventArgs(CurrentTask));
        }

    }
}
