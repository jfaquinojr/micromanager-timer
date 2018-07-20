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
    public class PomodoroTaskTests
    {
        [Theory]
        [InlineData(5)]
        [InlineData(15)]
        [InlineData(25)]
        [InlineData(6)]
        public void TestTask_Construction_ShouldHavePoints(int value)
        {
            var task = new PomodoroTask(value);
            Assert.Equal(value, task.Points.Count());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(6)]
        [InlineData(100)]
        public void TestTask_NextPoint_ShouldMoveNext(int value)
        {
            var task = new PomodoroTask(value);

            for (int i = 0; i < value; i++)
            {
                Assert.NotNull(task.CurrentPoint);
                Assert.False(task.CurrentPoint.IsInterrupted, "pomodoro should not be interrupted");
                Assert.True(task.CurrentPoint.IsPlanned, "pomodoro should be planned");
                task.Next();
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(12)]
        [InlineData(44)]
        [InlineData(4324)]
        public void TestTask_NextPomodoro_ShouldSaveDuration(int value)
        {
            var task = new PomodoroTask(value);

            for (int i = 0; i < value; i++)
            {
                var oldPomodoro = task.CurrentPoint;
                Assert.NotNull(oldPomodoro.StartedTime);
                Assert.Null(oldPomodoro.StoppedTime);
                task.Next();
                Assert.NotNull(oldPomodoro.StoppedTime);
                Assert.NotNull(task.CurrentPoint.StartedTime);
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(8)]
        public void TestTask_InterruptPomodoro_ShouldSayInterrupted(int value)
        {
            var task = new PomodoroTask(value);
            
            for (int i = 0; i < value; i++)
            {
                task.Interrupt();
                Assert.True(task.CurrentPoint.IsInterrupted);
                task.Next();
            }
        }
    }
}
