using MicroManager.Timer.Core.Plugins;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroManager.Timer.Core.Presenters
{
    public class MicroManagerPresenter
    {
        private readonly IPomodoroTimer _pomodoroTimer;
        private readonly IPluginManager _pluginManager;

        public MicroManagerPresenter(IPomodoroTimer pomodoroTimer, IPluginManager pluginManager)
        {
            _pomodoroTimer = pomodoroTimer;
            _pluginManager = pluginManager;
        }
    }
}
