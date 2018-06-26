using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace MicroManager.WinForms
{
    public partial class FormMain : Form
    {

        System.Timers.Timer _timerPomodoro = new System.Timers.Timer();
        System.Timers.Timer _timerScreenshot = new System.Timers.Timer();

        private int GetScreenshotInterval()
        {
            return _formOptions.ScreenCaptureInterval();
        }

        private int _durationMinutes = 25;
        private int _durationSeconds = 60;

        private int _screenshotSeconds = 3000;

        private string _currentDirectory = "";
        private FormOptions _formOptions = new FormOptions();

        public FormMain()
        {
            InitializeComponent();
            _timerPomodoro.Interval = 1000;
            _timerPomodoro.Elapsed += Timer_Elapsed;
            
            DisplayTime(_durationMinutes, 0);

            _screenshotSeconds = _formOptions.ScreenCaptureInterval();
            _timerScreenshot.Interval = _screenshotSeconds;
            _timerScreenshot.Elapsed += _timerScreenshot_Elapsed;

            _durationMinutes = _formOptions.PomodoroDuration();
            _formOptions.OptionSaved += _formOptions_OptionSaved;
        }

        private void _formOptions_OptionSaved(object sender, EventArgs e)
        {
            _screenshotSeconds = _formOptions.ScreenCaptureInterval();
            _durationMinutes = _formOptions.PomodoroDuration();

            _timerScreenshot.Interval = _screenshotSeconds;
            DisplayTime(_durationMinutes, 0);
        }

        private void _timerScreenshot_Elapsed(object sender, ElapsedEventArgs e)
        {
            // Determine the size of the "virtual screen", which includes all monitors.
            int screenLeft = SystemInformation.VirtualScreen.Left;
            int screenTop = SystemInformation.VirtualScreen.Top;
            int screenWidth = SystemInformation.VirtualScreen.Width;
            int screenHeight = SystemInformation.VirtualScreen.Height;

            // https://stackoverflow.com/a/1163770/578334
            // Rectangle bounds = Screen.GetBounds(Point.Empty);
            using (Bitmap bitmap = new Bitmap(screenWidth, screenHeight))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(screenLeft, screenTop, 0, 0, bitmap.Size);
                }
                var dt = DateTime.Now;
                var imageFilename = _currentDirectory + "/" + dt.ToString("yyyyMMdd") + "-screenshot-" + dt.ToString("mmss") + ".jpg";
                try
                {
                    Debug.WriteLine($"saving screenshot '{imageFilename}'");
                    bitmap.Save(imageFilename, ImageFormat.Jpeg);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            // ensure path exists
            this._currentDirectory = Path.Combine(System.Environment.CurrentDirectory, DateTime.Now.ToString("yyyy-MM-dd"), GetSafeFilename(lstTasks.SelectedItem.ToString()));
            try
            {
                if (!Directory.Exists(_currentDirectory))
                {
                    Directory.CreateDirectory(_currentDirectory);
                }
            }
            catch
            {
                Debug.WriteLine("cannot create directory named " + _currentDirectory);
                throw;
            }


            this.btnStop.Enabled = true;
            this.btnStart.Enabled = false;
            lblTimer.ForeColor = Color.Black;

            _timerPomodoro.Enabled = true;
            _timerScreenshot.Enabled = true;

            this.lstTasks.Enabled = false;
        }

        private string GetSafeFilename(string filename)
        {
            return string.Join("_", filename.Split(Path.GetInvalidFileNameChars()));
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //this.lblTimer.Text = e.SignalTime.ToString("mm:ss");
            _durationSeconds -= 1;
            if(_durationSeconds <= 0)
            {
                _durationSeconds = 60;
                _durationMinutes -= 1;
            }

            if (_durationMinutes == 0)
            {
                // beep
                SystemSounds.Beep.Play();
                Invoke(new Action(() => { btnStop_Click(null, EventArgs.Empty); }));
                return;
            }

            Invoke(new Action(() => { DisplayTime(_durationMinutes-1, _durationSeconds-1); }));
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this._durationMinutes = _formOptions.PomodoroDuration();
            this._durationSeconds = 60;

            this.btnStop.Enabled = false;
            this.btnStart.Enabled = true;

            this._timerPomodoro.Enabled = false;
            this._timerScreenshot.Enabled = false;

            this.lstTasks.Enabled = true;
        }

        private void DisplayTime(int mins, int secs)
        {
            if(mins == 0 && secs == 0)
            {
                lblTimer.ForeColor = Color.Red;
            }
            lblTimer.Text = $"{(mins).ToString().PadLeft(2, '0')}:{(secs).ToString().PadLeft(2, '0')}";
        }

        private void lstTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnIncrease.Enabled = true;
            btnDecrease.Enabled = true;
            btnStart.Enabled = true;
        }

        private void btnAddTask_Click(object sender, EventArgs e)
        {
            lstTasks.Items.Add(txtAddTask.Text);
            txtAddTask.Text = "";
            if(lstTasks.Items.Count == 1)
            {
                lstTasks.SelectedIndex = 0;
            }
        }

        private void txtAddTask_TextChanged(object sender, EventArgs e)
        {
            btnAddTask.Enabled = !string.IsNullOrWhiteSpace(txtAddTask.Text);
        }

        private void btnIncrease_Click(object sender, EventArgs e)
        {

        }

        private void btnDecrease_Click(object sender, EventArgs e)
        {

        }

        private void mnuOpenFolder_Click(object sender, EventArgs e)
        {

        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mnuOptions_Click(object sender, EventArgs e)
        {
            _formOptions.ShowDialog(this);
        }
    }
}
