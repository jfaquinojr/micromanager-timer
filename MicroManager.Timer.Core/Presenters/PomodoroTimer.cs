using MicroManager.Timer.Core.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MicroManager.Timer.Core.Presenters
{
    public class PomodoroTimer : IPomodoroTimer
    {
        public event EventHandler Tick;
        public event EventHandler TickMinute;
        public event EventHandler TaskStarted;
        public event EventHandler TaskStopped;
        public event EventHandler ShortBreakStarted;
        public event EventHandler ShortBreakStopped;
        public event EventHandler LongBreakStarted;
        public event EventHandler LongBreakStopped;

        // default values:
        // 4  pomodoros before long break
        // 15 minutes long break
        // 5  minutes short break
        // 25 minutes pomodoro duration
        private const int DefaultPomodorosBeforeLongBreak = 4;
        private const int DefaultDuration = 25;
        private const int DefaultLongBreakDuration = 15;
        private const int DefaultShortBreakDuration = 5;

        public Clock Clock { get; set; }
        public IEnumerable<PomodoroTask> Tasks { get; private set; } = new List<PomodoroTask>();
        public PomodoroTask CurrentTask { get; set; }

        private bool _isBreak;
        private ITimer _timer;
        private IPomodoroConfiguration _config;
        private ILogger _logger;


        public PomodoroTimer(IPomodoroConfiguration config, ITimer timer, ILogger logger)
        {
            _config = config;
            _timer = timer;
            _timer.Interval = 1000;
            _timer.Elapsed += _timer_Elapsed;
            _logger = logger;

            var duration = this.GetCurrentDuration(0, false);
            _logger.Trace("creating clock with initial duration of {duration} minutes.", duration);
            this.Clock = new Clock(duration);
        }

        public PomodoroTask AddTask(string name, int pomodoros, string description)
        {
            var task = new PomodoroTask(pomodoros);
            task.Id = Tasks.Count() + 1;
            task.Name = name;
            task.Description = description;
            Tasks = Tasks.Concat(new PomodoroTask[] { task });
            return task;
        }

        public void Start()
        {
            if(Tasks.Count() == 0)
            {
                throw new InvalidOperationException("No tasks defined.");
            }

            if(CurrentTask == null)
            {
                CurrentTask = Tasks.First();
            }
            else
            {
                CurrentTask.Next();
            }

            _timer.Enabled = true;
            //CurrentTask.Next();
            var duration = GetCurrentDuration(CurrentTask.ConsumedPoints, _isBreak);
            _logger.Info("Initial pomodoro duration is {duration}", duration);
            Clock = new Clock(duration);
            
            if (_isBreak)
            {
                var pomodoroBeforeLongBreak = GetPomodoroBeforeLongBreaks();
                if (CurrentTask.ConsumedPoints % pomodoroBeforeLongBreak == 0)
                {
                    _logger.Info("Starting long break: {@task}", CurrentTask);
                    LongBreakStarted?.Invoke(this, new PomodoroTimerEventArgs(CurrentTask));
                }
                else
                {
                    _logger.Info("Starting short break: {@task}", CurrentTask);
                    ShortBreakStarted?.Invoke(this, new PomodoroTimerEventArgs(CurrentTask));
                }
            }
            else
            {
                _logger.Info("Start of pomodoro: {@task}", CurrentTask);
                TaskStarted?.Invoke(this, new PomodoroTimerEventArgs(CurrentTask));
            }
        }

        public void Stop(bool interrupt = true)
        {
            if(!_timer.Enabled)
            {
                return; // already stopped
            }

            _timer.Enabled = false;
            if (_isBreak)
            {
                var pomodoroBeforeLongBreak = GetPomodoroBeforeLongBreaks();
                if (CurrentTask.ConsumedPoints % pomodoroBeforeLongBreak == 0)
                {
                    _logger.Info("End of long break: {@task}", CurrentTask);
                    LongBreakStopped?.Invoke(this, new PomodoroTimerEventArgs(CurrentTask));
                }
                else
                {
                    _logger.Info("End of short break: {@task}", CurrentTask);
                    ShortBreakStopped?.Invoke(this, new PomodoroTimerEventArgs(CurrentTask));
                }
                _isBreak = false;
                return;
            }


            if (interrupt)
            {
                _logger.Info("Interrupted pomodoro: {@task}", CurrentTask);
                _isBreak = false; // no breaks if interrupted
                this.CurrentTask.Interrupt();
            }
            else
            {
                _logger.Info("End of pomodoro: {@task}", CurrentTask);
                _isBreak = !_isBreak;
            }

            TaskStopped?.Invoke(this, new PomodoroTimerEventArgs(CurrentTask));
        }

        private int GetPomodoroBeforeLongBreaks()
        {
            var pomodoroBeforeLongBreak = _config.GetConfig(ConfigurationConstants.PomodoroBeforeLongBreak, DefaultPomodorosBeforeLongBreak);
            if(pomodoroBeforeLongBreak < 1)
            {
                pomodoroBeforeLongBreak = DefaultPomodorosBeforeLongBreak;
            }
            return pomodoroBeforeLongBreak;
        }

        private int GetCurrentDuration(int pomodoros, bool isBreak)
        {
            var pomodoroBeforeLongBreak = GetPomodoroBeforeLongBreaks();
            var longBreakMins = _config.GetConfig(ConfigurationConstants.LongBreakInMinutes, DefaultLongBreakDuration);
            var shortBreakMins = _config.GetConfig(ConfigurationConstants.ShortBreakInMinutes, DefaultShortBreakDuration);
            var pomodoroDuration = _config.GetConfig(ConfigurationConstants.PomodoroDuration, DefaultDuration);

            if (isBreak)
            {
                return pomodoros % pomodoroBeforeLongBreak == 0 ? longBreakMins : shortBreakMins;
            }

            return pomodoroDuration;
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //Console.WriteLine("1 second has elapsed");
            if(Clock.Seconds == 0)
            {
                TickMinute?.Invoke(this, new PomodoroTimerEventArgs(CurrentTask));

                if (Clock.Minutes == 0)
                {
                    _logger.Info("Time's up for task: {@task}", CurrentTask);
                    Stop();
                    return;
                }
            }
            _logger.Info("Timer has elapsed: {mins}:{secs}", Clock.Minutes, Clock.Seconds);
            Clock.Decrement();
            Tick?.Invoke(this, new PomodoroTimerEventArgs(CurrentTask));
        }

    }
}
