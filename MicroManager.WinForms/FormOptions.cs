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
    public partial class FormOptions : Form
    {
        public event EventHandler OptionSaved;

        public FormOptions()
        {
            InitializeComponent();
        }

        private void FormOptions_Load(object sender, EventArgs e)
        {

        }

        public int ScreenCaptureInterval()
        {
            var seconds = this.txtCaptureInterval.Value * 1000;
            return Math.Abs(int.Parse(seconds.ToString()));
        }

        public int PomodoroDuration()
        {
            return Math.Abs(int.Parse(this.txtPomodoroDuration.Value.ToString()));
        }

        public int ShortBreakDuration()
        {
            return Math.Abs(int.Parse(this.txtShortBreakDuration.Value.ToString()));
        }

        public int LongBreakDuration()
        {
            return Math.Abs(int.Parse(this.txtLongBreakDuration.Value.ToString()));
        }

        public int LongBreakDelay()
        {
            return Math.Abs(int.Parse(this.txtLongBreakDelay.Value.ToString()));
        }

        private void btnSaveOptions_Click(object sender, EventArgs e)
        {
            if (OptionSaved != null)
            {
                OptionSaved(this, e);
            }
            this.Close();
        }
    }
}
