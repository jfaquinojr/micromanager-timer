using MicroManager.WinForms.Models;
using MicroManager.WinForms.presenters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MicroManager.WinForms
{
    public partial class FormPomodoroTimer : Form
    {
        private PomodoroTimer _pomodoroTimer;

        public FormPomodoroTimer()
        {
            InitializeComponent();
            _pomodoroTimer = new PomodoroTimer(null, new SystemTimer());
            _pomodoroTimer.TaskStarted += _pomodoroTimer_TaskStarted;
            _pomodoroTimer.TaskStopped += _pomodoroTimer_TaskStopped;
            _pomodoroTimer.Tick += _pomodoroTimer_Tick;
        }

        private void _pomodoroTimer_Tick(object sender, EventArgs e)
        {
            Invoke(new Action(() => {
                lblClock.Text = _pomodoroTimer.Clock.ToString();
            }));
            
        }

        private void _pomodoroTimer_TaskStopped(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void _pomodoroTimer_TaskStarted(object sender, EventArgs e)
        {
            Console.WriteLine("Task started");
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            var task = new PomodoroTask(6);
            task.Name = "Task 1";

            _pomodoroTimer.Tasks.Add(task);
            _pomodoroTimer.CurrentTask = task;
            _pomodoroTimer.Start();
        }
    }
}
