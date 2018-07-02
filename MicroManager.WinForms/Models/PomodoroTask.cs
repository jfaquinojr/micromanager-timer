using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroManager.WinForms.Models
{
    class PomodoroTask
    {
        private static int _counter = 0;
        public int ConsumedPoints { get; private set; } = 0;
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<PomodoroPoint> Points { get; set; }
        public PomodoroPoint CurrentPoint { get; private set; }

        public PomodoroTask(int points)
        {
            if(points <= 0)
            {
                throw new ArgumentException("Number of points must be greater than 0", nameof(points));
            }

            _counter += 1;
            Id = _counter;
            Points = Enumerable.Range(0, points).Select(n => new PomodoroPoint(true)).ToList();
            CurrentPoint = Points[0];
        }

        public void Next()
        {
            ConsumedPoints += 1;
            CurrentPoint = Points[ConsumedPoints];
        }

        public void Interrupt()
        {
            CurrentPoint.IsInterrupted = true;
            Next();
        }


        public override string ToString() => $"{Name} ({Points} pomodoros)";
    }
}
