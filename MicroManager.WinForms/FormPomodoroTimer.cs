using MicroManager.Timer.Core.Presenters;
using NLog;
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
        private readonly ILogger _logger;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();


        public FormPomodoroTimer()
        {
            InitializeComponent();
            _logger = LogManager.GetCurrentClassLogger();
            _pomodoroTimer = new PomodoroTimer(new PomodoroConfiguration(), new SystemTimer(), _logger);
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

            _pomodoroTimer.AddTask("Task 1", 6, "");
            _pomodoroTimer.Start();
        }

        private void FormPomodoroTimer_Load(object sender, EventArgs e)
        {
            //_pomodoroTimer.Tasks.Add(new PomodoroTask(1));
            //_pomodoroTimer.Start();
        }

        private void FormPomodoroTimer_FormClosing(object sender, FormClosingEventArgs e)
        {
            _pomodoroTimer.Stop(true);
        }

        private void FormPomodoroTimer_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lblClock_Click(object sender, EventArgs e)
        {

        }

        private void lblTask_Click(object sender, EventArgs e)
        {

        }
    }
}
