using System;
using Core.Architecture.vCPU.Operations;
using Core.Components;
using Core.Interfaces;
using Core.Models;
using Core.Operations;
using FluentAssertions;
using NUnit.Framework;

namespace CoreTests
{
    [TestFixture]
    public class CPUTickableTests
    {
        private CPU m_CPU = null;
        private IMemoryBank m_Bank = null;

        [Test]
        public void TickCPU()
        {
            m_CPU.Tick();

            var t_Counter = m_CPU.Ticks;
            t_Counter.Should().Be(1, "We called Tick once.");
        }

        [Test]
        public void QueueAndExecuteOpCodeFromTick()
        {
            m_CPU.QueueOperation(new NoOp());
            m_CPU.Tick();

            m_CPU.QueuedOperations.Should().Be(0, "Tick should execute queued operation");
            m_CPU.ExecutedOperations.Should().Be(1, "Tick should execute queued operation");
        }

        [Test]
        public void SuspendCPU()
        {
            m_CPU.QueueOperation(new NoOp());
            m_CPU.Suspend();
            m_CPU.Tick();
            m_CPU.QueuedOperations.Should().Be(1, "We suspended the CPU after queuing a NoOp");
        }

        [Test]
        public void ExecuteOperationWhileCPUSuspended()
        {
            m_CPU.Suspend();
            m_CPU.ExecuteOperation(new NoOp());
            m_CPU.ExecutedOperations.Should().Be(1, "Suspensions should not have any affect on directly executed operations.");
        }

        [Test]
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

        [Test]
        public void ForceTick()
        {
            m_CPU.QueueOperation(new NoOp());
            m_CPU.Suspend();
            m_CPU.ForceTick();

            m_CPU.QueuedOperations.Should()
                .Be(0, "We used a force tick which should force execution when CPU is suspended.");
            m_CPU.ExecutedOperations.Should().Be(1);
        }

        [Test]
        public void ForceTickWithNoQueuedOperations()
        {
            m_CPU.Suspend();
            m_CPU.ForceTick();

            m_CPU.ExecutedOperations.Should().Be(0, "We have not queued any operations before ForceTick call.");
        }

        [Test]
        public void QueueLoadValueOperationAndTick()
        {
            m_CPU.QueueOperation(new OpLoadConst<int>(10, new MemoryLocation(new MemoryAddress(0), m_Bank)));
            m_CPU.Tick();
            var t_Value = m_Bank.Load<int>(new MemoryAddress(0));

            t_Value.Should().Be(10, "We queued a Load operation that should load the value 10 into memory.");
        }

        [Test]
        public void QueueOperationAndSuspendAndExecute()
        {
            m_CPU.QueueOperation(new NoOp());
            m_CPU.Suspend();
            m_CPU.Execute();

            m_CPU.ExecutedOperations.Should().Be(1);
            m_CPU.IsSuspended.Should().BeTrue();
        }

        [SetUp]
        public void Initialize()
        {
            m_CPU = new CPU();
            m_Bank = new MemoryBank(64);
        }
    }
}
