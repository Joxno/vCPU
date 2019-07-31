using System;
using Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace CoreTests
{
    [TestClass]
    public class CPUTests
    {
        [TestMethod]
        public void TickCPU()
        {
            var t_CPU = new CPU();
            t_CPU.Tick();

            var t_Counter = t_CPU.Ticks;
            t_Counter.Should().Be(1, "We called Tick once.");
        }
    }
}
