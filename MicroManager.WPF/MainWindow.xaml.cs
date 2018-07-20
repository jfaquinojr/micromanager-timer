using MicroManager.Timer.Core.Presenters;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MicroManager.Timer.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IPomodoroTimer _pomodoroTimer;

        public MainWindow(): this(new PomodoroTimer(new PomodoroConfiguration(), new SystemTimer(), LogManager.GetCurrentClassLogger()))
        {
        }
        public MainWindow(IPomodoroTimer pomodoroTimer)
        {
            InitializeComponent();

            _pomodoroTimer = pomodoroTimer;
            _pomodoroTimer.Tick += _pomodoroTimer_Tick;

        }

        private void _pomodoroTimer_Tick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
