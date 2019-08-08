using System;
using Core.Components;
using Core.Interfaces;
using NUnit.Framework;
using FluentAssertions;
using Core.Operations;
using Core.Models;

namespace CoreTests
{
    [TestFixture]
    public class CPUTests
    {
        public ICPU m_CPU = null;
        public IMemoryBank m_Bank = null;

        [Test]
        public void TickCPU()
        {
            m_CPU.Tick();

            var t_Counter = m_CPU.Ticks;
            t_Counter.Should().Be(1, "We called Tick once.");
        }

        [Test]
        public void ExecuteOpCode()
        {
            m_CPU.ExecuteOperation(new NoOp());

            var t_OpCounter = m_CPU.ExecutedOperations;
            t_OpCounter.Should().Be(1, "We executed a NoOp operation");
        }

        [Test]
        public void QueueOpCode()
        {
            m_CPU.QueueOperation(new NoOp());

            var t_QueuedCounter = m_CPU.QueuedOperations;
            t_QueuedCounter.Should().Be(1, "We queued up a NoOp operation");
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
        public void ExecuteLoadValueOperation()
        {
            m_CPU.ExecuteOperation(new OpLoad<int>(5, new MemoryAddress(0), m_Bank));
            var t_Value = m_Bank.Load<int>(new MemoryAddress(0));

            t_Value.Should().Be(5, "We executed a Load operation that should load the value 5 into memory.");
        }

        [Test]
        public void QueueLoadValueOperationAndTick()
        {
            m_CPU.QueueOperation(new OpLoad<int>(10, new MemoryAddress(0), m_Bank));
            m_CPU.Tick();
            var t_Value = m_Bank.Load<int>(new MemoryAddress(0));

            t_Value.Should().Be(10, "We queued a Load operation that should load the value 10 into memory.");
        }

        [Test]
        public void ExecuteLoadAddressOperation()
        {
            m_CPU.ExecuteOperation(new OpLoad<int>(5, new MemoryAddress(0), m_Bank));
            m_CPU.ExecuteOperation(new OpLoadAddress<int>(new MemoryAddress(0), m_Bank, new MemoryAddress(4), m_Bank));
            var t_Value = m_Bank.Load<int>(new MemoryAddress(4));

            t_Value.Should().Be(5, "We loaded 5 into memory and copied data into a separate address and loaded from there.");
        }

        [SetUp]
        public void InitializeTestVariables()
        {
            m_CPU = new CPU();
            m_Bank = new MemoryBank(32);
        }
    }
}
