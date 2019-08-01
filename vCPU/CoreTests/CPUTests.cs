﻿using System;
using Core.Components;
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
        public void ExecuteOpCode()
        {
            m_CPU.ExecuteOperation(new NoOp());

            var t_OpCounter = m_CPU.ExecutedOperations;
            t_OpCounter.Should().Be(1, "We executed a NoOp operation");
        }

        [TestMethod]
        public void QueueOpCode()
        {
            m_CPU.QueueOperation(new NoOp());

            var t_QueuedCounter = m_CPU.QueuedOperations;
            t_QueuedCounter.Should().Be(1, "We queued up a NoOp operation");
        }

        [TestMethod]
        public void QueueAndExecuteOpCodeFromTick()
        {
            m_CPU.QueueOperation(new NoOp());
            m_CPU.Tick();

            m_CPU.QueuedOperations.Should().Be(0, "Tick should execute queued operation");
            m_CPU.ExecutedOperations.Should().Be(1, "Tick should execute queued operation");
        }

        [TestMethod]
        public void SuspendCPU()
        {
            m_CPU.QueueOperation(new NoOp());
            m_CPU.Suspend();
            m_CPU.Tick();
            m_CPU.QueuedOperations.Should().Be(1, "We suspended the CPU after queuing a NoOp");
        }

        [TestMethod]
        public void ExecuteOperationWhileCPUSuspended()
        {
            m_CPU.Suspend();
            m_CPU.ExecuteOperation(new NoOp());
            m_CPU.ExecutedOperations.Should().Be(1, "Suspensions should not have any affect on directly executed operations.");
        }

        [TestMethod]
        public void SuspendAndResumeCPU()
        {
            m_CPU.QueueOperation(new NoOp());
            m_CPU.QueueOperation(new NoOp());
            m_CPU.Suspend();
            m_CPU.Tick();
            m_CPU.Resume();
            m_CPU.Tick();
            m_CPU.QueuedOperations.Should().Be(1, "We resumed CPU after suspension and ticked once while CPU was active.");
        }

        [TestInitialize]
        public void InitializeTestVariables()
        {
            m_CPU = new CPU();
        }
    }
}
