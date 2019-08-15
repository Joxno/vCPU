using System;
using Core.Architecture.vCPU.Operations;
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
        public CPU m_CPU = null;
        public IMemoryBank m_Bank = null;

        [Test]
        public void ExecuteSingleQueuedOperationCPU()
        {
            m_CPU.QueueOperation(new NoOp());
            m_CPU.Execute();

            m_CPU.ExecutedOperations.Should().Be(1, "We queued a NoOp and called Execute once.");
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
        public void ExecuteLoadValueOperation()
        {
            m_CPU.ExecuteOperation(new OpLoadConst<int>(5, new MemoryLocation(new MemoryAddress(0), m_Bank)));
            var t_Value = m_Bank.Load<int>(new MemoryAddress(0));

            t_Value.Should().Be(5, "We executed a Load operation that should load the value 5 into memory.");
        }

        [Test]
        public void QueueAndExecuteAddValueOperation()
        {
            m_Bank.Store(5, new MemoryAddress(0));
            m_Bank.Store(15, new MemoryAddress(4));
            m_CPU.QueueOperation(new OpAdd
            (
                new MemoryLocation(new MemoryAddress(0), m_Bank ),
                new MemoryLocation(new MemoryAddress(4), m_Bank), 
                new MemoryLocation(new MemoryAddress(8), m_Bank)
            ));

            m_CPU.Execute();

            var t_Value = m_Bank.Load<int>(new MemoryAddress(8));

            t_Value.Should().Be(20);
            m_CPU.ExecutedOperations.Should().Be(1);
        }

        [SetUp]
        public void InitializeTestVariables()
        {
            m_CPU = new CPU();
            m_Bank = new MemoryBank(32);
        }
    }
}
