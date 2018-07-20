using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroManager.Timer.Core.Models
{
    public class Clock
    {
        public int Minutes { get; private set; }
        public int Seconds { get; private set; }

        public Clock(int mins)
        {
            Minutes = mins;
            Seconds = 0;
        }

        public void Decrement()
        {
            Seconds -= 1;
            if(Seconds < 0)
            {
                if (Minutes > 0)
                {
                    Minutes -= 1;
                }
                Seconds = 59;
            }
        }
        public void Reset()
        {
            Minutes = 0;
            Seconds = 0;
        }

        public override string ToString()
        {
            return $"{(Minutes).ToString().PadLeft(2, '0')}:{(Seconds).ToString().PadLeft(2, '0')}";
        }
    }
}
