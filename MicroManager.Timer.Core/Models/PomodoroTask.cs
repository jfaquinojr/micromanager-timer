using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroManager.Timer.Core.Models
{
    public class PomodoroTask
    {
        private static int _counter = 0;
        private int _consumedPointIndex;
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<PomodoroPoint> Points { get; set; }
        public PomodoroPoint CurrentPoint { get; private set; }

        public int ConsumedPoints { get => _consumedPointIndex + 1; }

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
            CurrentPoint.StartedTime = DateTime.Now;
        }

        public void Next()
        {
            CurrentPoint.StoppedTime = DateTime.Now;
            _consumedPointIndex += 1;
            if(_consumedPointIndex >= Points.Count() )
            {
                Points.Add(new PomodoroPoint(false));
            }
            CurrentPoint = Points[_consumedPointIndex];
            CurrentPoint.StartedTime = DateTime.Now;
        }

        public void Interrupt()
        {
            CurrentPoint.IsInterrupted = true;
        }


        public override string ToString() => $"{Name} ({Points} pomodoros)";
    }
}
