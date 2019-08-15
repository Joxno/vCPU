using System;
using Core.Architecture.vCPU.Operations;
using Core.Components;
using Core.Models;
using FluentAssertions;
using NUnit.Framework;

namespace vCPUTests
{
    [TestFixture]
    public class OperationTests
    {
        private MemoryBank m_Bank = null;

        [Test]
        public void ExecuteLoadConstOperation()
        {
            new OpLoadConst<int>(5, new MemoryLocation(new MemoryAddress(0), m_Bank))
                .Execute();

            var t_Value = m_Bank.Load<int>(new MemoryAddress(0));

            t_Value.Should().Be(5, "We loaded the constant value of 5 into memory.");
        }

        [Test]
        public void ExecuteLoadAddressOperation()
        {
            m_Bank.Store(5, new MemoryAddress(0));

            new OpLoad<int>(new MemoryLocation(new MemoryAddress(0), m_Bank),
                new MemoryLocation(new MemoryAddress(4), m_Bank))
                .Execute();

            var t_Value = m_Bank.Load<int>(new MemoryAddress(4));

            t_Value.Should().Be(5, "We loaded 5 into memory and copied data into a separate address and loaded from there.");
        }

        [Test]
        public void ExecuteAddOperation()
        {
            m_Bank.Store(10, new MemoryAddress(0));
            m_Bank.Store(20, new MemoryAddress(4));

            new OpAdd
            (
                new MemoryLocation(new MemoryAddress(0), m_Bank),
                new MemoryLocation(new MemoryAddress(4), m_Bank),
                new MemoryLocation(new MemoryAddress(8), m_Bank)
            ).Execute();

            var t_Value = m_Bank.Load<int>(new MemoryAddress(8));

            t_Value.Should().Be(30);
        }

        [Test]
        public void ExecuteSubOperation()
        {
            m_Bank.Store(20, new MemoryAddress(0));
            m_Bank.Store(15, new MemoryAddress(4));

            new OpSub
            (
                new MemoryLocation(new MemoryAddress(0), m_Bank),
                new MemoryLocation(new MemoryAddress(4), m_Bank),
                new MemoryLocation(new MemoryAddress(8), m_Bank)
            ).Execute();

            var t_Value = m_Bank.Load<int>(new MemoryAddress(8));
            t_Value.Should().Be(5);
        }

        [Test]
        public void ExecuteLoadIfZeroOperation()
        {
            var t_Destination = new MemoryLocation(new MemoryAddress(4), m_Bank);
            new OpLoadConstIfZero<int>(5,
                    t_Destination,
                    new MemoryLocation(new MemoryAddress(8), m_Bank))
                .Execute();

            t_Destination.Load<int>()
                .Should().Be(5);
        }

        [SetUp]
        public void Initialize()
        {
            m_Bank = new MemoryBank(64);
        }
    }
}
