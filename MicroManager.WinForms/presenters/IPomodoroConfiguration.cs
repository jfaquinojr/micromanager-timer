using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroManager.WinForms.presenters
{
    interface IPomodoroConfiguration
    {
        string GetConfig(string name, string defaultValue);
    }
}
