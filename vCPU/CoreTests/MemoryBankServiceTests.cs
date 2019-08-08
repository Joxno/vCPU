using System;
using System.Collections.Generic;
using Core.Components;
using Core.Exceptions;
using Core.Interfaces;
using Core.Models;
using Core.Services;
using FluentAssertions;
using NUnit.Framework;

namespace CoreTests
{
    [TestFixture]
    public class MemoryBankServiceTests
    {
        private IMemoryBankService m_Service = null;

        [Test]
        public void ResolveAddress()
        {
            var t_Bank = m_Service.ResolveAddress(new MemoryBankAddress(0));

            t_Bank.Should().NotBeNull();
            t_Bank.Size.Should().Be(32);
        }

        [Test]
        public void AttachNewBank()
        {
            var t_Address = m_Service.Attach(new MemoryBank(64));
            var t_Bank = m_Service.ResolveAddress(t_Address);

            t_Bank.Should().NotBeNull();
            t_Bank.Size.Should().Be(64);
        }

        [Test]
        public void AttachNewBankAtAddress()
        {
            var t_Address = new MemoryBankAddress(100);
            m_Service.AttachAtAddress(new MemoryBank(96), t_Address);
            var t_Bank = m_Service.ResolveAddress(t_Address);

            t_Bank.Should().NotBeNull();
            t_Bank.Size.Should().Be(96);
        }

        [Test]
        public void AttachNewBankAtOccupiedAddress()
        {
            Action t_Attach = () => m_Service.AttachAtAddress(new MemoryBank(16), new MemoryBankAddress(0));
            t_Attach.Should().Throw<AddressOccupied>();
        }

        [Test]
        public void DetachBank()
        {
            m_Service.Detach(new MemoryBankAddress(0));

            var t_Found = m_Service.HasBankAtAddress(new MemoryBankAddress(0));
            t_Found.Should().BeFalse("We detached the initial bank attached to the service.");
        }

        [Test]
        public void CheckForBankAtAddress()
        {
            var t_Found = m_Service.HasBankAtAddress(new MemoryBankAddress(0));

            t_Found.Should().BeTrue("During instantiation we add an initial bank at address 0");
        }

        [SetUp]
        public void Initialize()
        {
            m_Service = new MemoryBankService(new List<IMemoryBank> { new MemoryBank(32) });
        }
    }
}
