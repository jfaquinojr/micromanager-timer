using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroManager.Timer.Core.Presenters
{
    public interface IPomodoroConfiguration
    {
        T GetConfig<T>(string name, T defaultValue);
        void WriteConfig<T>(string name, T value);
    }
}
