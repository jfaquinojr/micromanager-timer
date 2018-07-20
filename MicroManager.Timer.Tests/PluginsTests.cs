using System;
using Xunit;
using MicroManager.Timer.Core;
using MicroManager.Timer.Core.Presenters;
using NSubstitute;
using NLog;
using MicroManager.Timer.Core.Models;
using System.Linq;
using System.Timers;
//using System.Collections.Generic;

namespace MicroManager.Timer.Tests
{
    public class PluginsTests
    {
        [Theory]
        [InlineData(6)]
        public void PluginsTest_Construction_CreatePluginManager(int value)
        {
            //var pluginManager = new PluginManager(pomodoroTimer)
            
        }
    }
}
