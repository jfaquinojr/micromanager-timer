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
    public class BasePomodoroTimerTests : IDisposable
    {
        public IPomodoroConfiguration config;
        public ITimer timer;
        public ILogger logger;

        protected BasePomodoroTimerTests()
        {
            config = Substitute.For<IPomodoroConfiguration>();
            timer = Substitute.For<ITimer>();
            logger = LogManager.GetCurrentClassLogger();

            config.GetConfig(ConfigurationConstants.PomodoroBeforeLongBreak, 4).Returns(4);
            config.GetConfig(ConfigurationConstants.LongBreakInMinutes, 4).Returns(15);
            config.GetConfig(ConfigurationConstants.ShortBreakInMinutes, 4).Returns(5);
            config.GetConfig(ConfigurationConstants.PomodoroDuration, 4).Returns(25);
        }

        public void Dispose()
        {
            // Do "global" teardown here; Called after every test method.
        }
    }
    public class PomodoroTimerTests : BasePomodoroTimerTests
    {
        [Theory]
        [InlineData(5)]
        [InlineData(15)]
        [InlineData(25)]
        [InlineData(6)]
        public void TestCustomPomodoroDuration(int value)
        {
            config.GetConfig(ConfigurationConstants.PomodoroDuration, 25).Returns(value);
            var pomodoroTimer = new PomodoroTimer(config, timer, logger);
            Assert.Equal(value, pomodoroTimer.Clock.Minutes);
        }

        [Fact]
        public void TestUnstartedTimerHasNoCurrentTask()
        {
            config.GetConfig(ConfigurationConstants.PomodoroDuration, 25).Returns(5);
            var pomodoroTimer = new PomodoroTimer(config, timer, logger);
            Assert.Null(pomodoroTimer.CurrentTask);
        }

        [Fact]
        public void TestEmptyPomodoroThrowsException()
        {
            config.GetConfig(ConfigurationConstants.PomodoroDuration, 25).Returns(5);

            var pomodoroTimer = new PomodoroTimer(config, timer, logger);
            Assert.Throws<InvalidOperationException>(() => pomodoroTimer.Start());
        }

        [Fact]
        public void TestStartTimerNoThrowsNoError()
        {
            config.GetConfig(ConfigurationConstants.PomodoroDuration, 25).Returns(5);

            var pomodoroTimer = new PomodoroTimer(config, timer, logger);
            pomodoroTimer.AddTask("TestStartTimerNoThrowsNoError", 1, "Hello world");
            pomodoroTimer.Start();
        }

        [Fact]
        public void TestStartTimer_AddTaskWithZeroPomodoros_ThrowsError()
        {

            config.GetConfig(ConfigurationConstants.PomodoroDuration, 25).Returns(5);

            var pomodoroTimer = new PomodoroTimer(config, timer, logger);
            Assert.Throws<ArgumentException>(() => pomodoroTimer.AddTask("TestStartTimer_AddTaskWithZeroPomodoros_ThrowsError", 0, "Hello world"));
        }

        [Fact]
        public void TestStartTimer_StartWithOneTask_SetsCurrentTask()
        {
            config.GetConfig(ConfigurationConstants.PomodoroDuration, 25).Returns(5);

            var pomodoroTimer = new PomodoroTimer(config, timer, logger);
            pomodoroTimer.AddTask("TestStartTimer_StartWithOneTask_SetsCurrentTask", 1, "Hello world");
            pomodoroTimer.Start();
            Assert.Equal("TestStartTimer_StartWithOneTask_SetsCurrentTask", pomodoroTimer.CurrentTask.Name);
            Assert.Equal(1, pomodoroTimer.CurrentTask.Id);
            Assert.Equal("Hello world", pomodoroTimer.CurrentTask.Description);
            Assert.Single(pomodoroTimer.Tasks);
        }


        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(10)]
        [InlineData(13870)]
        public void TestStartTimer_StartWithNTaskPoint_MustHaveNTaskPoints(int count)
        {
            config.GetConfig(ConfigurationConstants.PomodoroDuration, 25).Returns(5);

            var pomodoroTimer = new PomodoroTimer(config, timer, logger);
            var task = pomodoroTimer.AddTask("TestStartTimer_StartWithNTaskPoint_MustHaveNTaskPoints", count, "Hello world");
            //pomodoroTimer.Start();
            Assert.Equal(count, task.Points.Count());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(10)]
        public void TestStartTimer_StopTimer_ShouldConsumePoints(int tasksCount)
        {
            config.GetConfig(ConfigurationConstants.PomodoroDuration, 25).Returns(5);

            var pomodoroTimer = new PomodoroTimer(config, timer, logger);
            var task = pomodoroTimer.AddTask("TestStartTimer_StopTimer_ShouldConsumePoints", tasksCount, "Hello world");

            for (int i = 1; i <= tasksCount; i++)
            {
                pomodoroTimer.Start();
                Assert.Equal(i, pomodoroTimer.CurrentTask.ConsumedPoints);
                pomodoroTimer.Stop();
                Assert.True(pomodoroTimer.CurrentTask.CurrentPoint.IsInterrupted);
            }
            //Assert.Equal(count, task.Points.Count());
        }

        //[Fact]
        //public void TestStartTimer_SystemTimer_ShouldStopTaskUninterrupted()
        //{
        //    config.GetConfig(ConfigurationConstants.PomodoroDuration, 25).Returns(1);

        //    var pomodoroTimer = new PomodoroTimer(config, timer, logger);
        //    var task = pomodoroTimer.AddTask("TestStartTimer_SystemTimer_ShouldStopTaskUninterrupted", 2, "Hello world");
        //    pomodoroTimer.Tick += (object sender, EventArgs e) =>
        //    {
        //        logger.Trace("mock timer tick fired");
        //    };


        //    pomodoroTimer.Start();

        //    timer.Elapsed += (object sender, ElapsedEventArgs e) =>
        //    {
        //        pomodoroTimer.Stop(false);
        //    };
        //    timer.Elapsed += Raise.Event<ElapsedEventHandler>(null, null);

        //    Assert.False(pomodoroTimer.CurrentTask.CurrentPoint.IsInterrupted);
        //    Assert.Equal(1, pomodoroTimer.CurrentTask.ConsumedPoints);

        //    pomodoroTimer.Stop();
        //    //Assert.Equal(count, task.Points.Count());
        //}

        [Fact]
        public void TestStartTimer_Start_ShouldInvokeTaskStart()
        {
            config.GetConfig(ConfigurationConstants.PomodoroDuration, 25).Returns(1);

            var pomodoroTimer = new PomodoroTimer(config, timer, logger);
            var task = pomodoroTimer.AddTask("TestStartTimer_Start_ShouldInvokeTaskStart", 2, "Hello world");

            var hasFired = false;
            pomodoroTimer.TaskStarted += (object sender, EventArgs e) =>
            {
                hasFired = true;
            };

            pomodoroTimer.Start();
            Assert.True(hasFired);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(4)]
        [InlineData(10)]
        public void TestStartTimer_InterruptedStops_ShouldHaveNoBreaks(int stops)
        {
            config.GetConfig(ConfigurationConstants.PomodoroDuration, 25).Returns(1);

            var pomodoroTimer = new PomodoroTimer(config, timer, logger);
            var task = pomodoroTimer.AddTask("TestStartTimer_AFterNStops_ShouldStartLongBreak", 1, "Hello world");

            var hasFiredStartedShort = false;
            var hasFiredStoppedShort = false;
            var hasFiredStartedLong = false;
            var hasFiredStoppedLong = false;
            pomodoroTimer.ShortBreakStarted += (object sender, EventArgs e) =>
            {
                hasFiredStartedShort = true;
            };
            pomodoroTimer.ShortBreakStopped += (object sender, EventArgs e) =>
            {
                hasFiredStoppedShort = true;
            };
            pomodoroTimer.LongBreakStarted += (object sender, EventArgs e) =>
            {
                hasFiredStartedLong = true;
            };
            pomodoroTimer.LongBreakStopped += (object sender, EventArgs e) =>
            {
                hasFiredStoppedLong = true;
            };

            for (int i = 0; i < stops; i++)
            {
                pomodoroTimer.Start();
                pomodoroTimer.Stop();
            }

            Assert.False(hasFiredStartedShort);
            Assert.False(hasFiredStoppedShort);
            Assert.False(hasFiredStartedLong);
            Assert.False(hasFiredStoppedLong);
        }

        [Fact]
        public void TestStartTimer_TwoStops_ShouldInvokeShortBreak()
        {
            config.GetConfig(ConfigurationConstants.PomodoroDuration, 25).Returns(1);

            var pomodoroTimer = new PomodoroTimer(config, timer, logger);
            var task = pomodoroTimer.AddTask("TestStartTimer_AFterNStops_ShouldStartLongBreak", 1, "Hello world");

            var hasFiredStarted = false;
            var hasFiredStopped = false;
            pomodoroTimer.ShortBreakStarted += (object sender, EventArgs e) =>
            {
                hasFiredStarted = true;
            };
            pomodoroTimer.ShortBreakStopped += (object sender, EventArgs e) =>
            {
                hasFiredStopped = true;
            };

            pomodoroTimer.Start();
            pomodoroTimer.Stop(false);

            pomodoroTimer.Start();
            pomodoroTimer.Stop();

            Assert.True(hasFiredStarted);
            Assert.True(hasFiredStopped);
        }

        [Fact]
        public void TestStartTimer_FourStops_ShouldInvokeLongBreak()
        {
            var pomodoroTimer = new PomodoroTimer(config, timer, logger);
            var task = pomodoroTimer.AddTask("TestStartTimer_AFterNStops_ShouldStartLongBreak", 1, "Hello world");

            var hasFiredStarted = false;
            var hasFiredStopped = false;
            pomodoroTimer.LongBreakStarted += (object sender, EventArgs e) =>
            {
                hasFiredStarted = true;
            };
            pomodoroTimer.LongBreakStopped += (object sender, EventArgs e) =>
            {
                hasFiredStopped = true;
            };

            for (int i = 0; i < 5; i++)
            {
                pomodoroTimer.Start();
                pomodoroTimer.Stop(false);
            }

            Assert.True(hasFiredStarted);
            Assert.True(hasFiredStopped);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(4)]
        [InlineData(10)]
        public void TestStartTimer_NStops_ShouldInvokeLongBreak(int stops)
        {
            config.GetConfig("PomodoroBeforeLongBreak", 4).Returns(stops);


            var pomodoroTimer = new PomodoroTimer(config, timer, logger);
            var task = pomodoroTimer.AddTask("TestStartTimer_NStops_ShouldInvokeLongBreak", 1, "Hello world");

            var hasFiredStarted = false;
            var hasFiredStopped = false;
            pomodoroTimer.LongBreakStarted += (object sender, EventArgs e) =>
            {
                hasFiredStarted = true;
            };
            pomodoroTimer.LongBreakStopped += (object sender, EventArgs e) =>
            {
                hasFiredStopped = true;
            };

            for (int i = 0; i < stops; i++)
            {
                pomodoroTimer.Start();
                pomodoroTimer.Stop(false);
            }

            pomodoroTimer.Start();
            pomodoroTimer.Stop();

            Assert.True(hasFiredStarted);
            Assert.True(hasFiredStopped);
        }

        

    }
}
