using System;
using Core.Models;
using Core.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Core.Operations;

namespace CoreTests
{
    [TestClass]
    public class CPUTests
    {
        public ICPU m_CPU = null;

        [TestMethod]
        public void TickCPU()
        {
            m_CPU.Tick();

            var t_Counter = m_CPU.Ticks;
            t_Counter.Should().Be(1, "We called Tick once.");
        }

        [TestMethod]
        public void ExecuteOPCode()
        {
            m_CPU.ExecuteOP(new NoOP());

            var t_OpCounter = m_CPU.ExecutedOperations;
            t_OpCounter.Should().Be(1, "We executed a NoOp operation");
        }

        [TestMethod]
        public void QueueOpCode()
        {
            m_CPU.QueueOperation(new NoOP());

            var t_QueuedCounter = m_CPU.QueuedOperations;
            t_QueuedCounter.Should().Be(1, "We queued up a NoOp operation");
        }

        [TestInitialize]
        public void InitializeTestVariables()
        {
            m_CPU = new CPU();
        }
    }
}
