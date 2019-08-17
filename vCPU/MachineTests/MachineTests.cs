using System;
using System.Collections.Generic;
using Core.Components;
using Core.Interfaces;
using Core.Machine.Interfaces;
using Core.Machine.Models;
using Core.Models;
using Core.Services;
using FluentAssertions;
using NUnit.Framework;

namespace MachineTests
{
    [TestFixture]
    public class MachineTests
    {
        private IMachine m_Machine = null;
        private IMemoryBankService m_BankService = null;
        private IMemoryBank m_Bank = null;

        [Test]
        public void CheckIfMachineIsRunning()
        {
            m_Machine.IsRunning
                .Should().BeFalse();
        }

        [Test]
        public void StartMachine()
        {
            m_Machine.Start();

            m_Machine.IsRunning
                .Should().BeTrue();
        }

        [Test]
        public void StopMachine()
        {
            m_Machine.Start();
            m_Machine.Stop();

            m_Machine.IsRunning
                .Should().BeFalse();
        }

        [Test]
        public void SuspendMachine()
        {
            m_Machine.Start();
            m_Machine.Suspend();

            m_Machine.IsRunning
                .Should().BeTrue();
            m_Machine.IsSuspended
                .Should().BeTrue();
        }

        [Test]
        public void SuspendAndResumeMachine()
        {
            m_Machine.Start();
            m_Machine.Suspend();

            m_Machine.IsSuspended.Should().BeTrue();

            m_Machine.Resume();

            m_Machine.IsSuspended.Should().BeFalse();
            m_Machine.IsRunning.Should().BeTrue();
        }

        [Test]
        public void InspectMemory()
        {
            m_Bank.Store<byte>(5, new MemoryAddress(0));

            var t_Value =
                m_Machine.InspectMemory(new MemoryLocationAddress(new MemoryAddress(0), new MemoryBankAddress(0)));

            t_Value.Should().Be(5);
        }


        [SetUp]
        public void Initialize()
        {
            m_Bank = new MemoryBank(512);
            m_BankService = new MemoryBankService(new List<IMemoryBank>
            {
                m_Bank
            });
            m_Machine = new Machine(m_BankService);
        }

    }
}
