using System;
using System.Collections.Generic;
using Core.Components;
using Core.Interfaces;
using Core.Models;
using Core.Services;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoreTests
{
    [TestClass]
    public class MemoryBankServiceTests
    {
        private IMemoryBankService m_Service = null;

        [TestMethod]
        public void ResolveAddress()
        {
            var t_Bank = m_Service.ResolveAddress(new MemoryBankAddress(0));

            t_Bank.Should().NotBeNull();
            t_Bank.Size.Should().Be(32);
        }

        [TestMethod]
        public void AttachNewBank()
        {
            var t_Address = m_Service.Attach(new MemoryBank(64));
            var t_Bank = m_Service.ResolveAddress(t_Address);

            t_Bank.Should().NotBeNull();
            t_Bank.Size.Should().Be(64);
        }

        [TestMethod]
        public void CheckForBankAtAddress()
        {
            var t_Found = m_Service.HasBankAtAddress(new MemoryBankAddress(0));

            t_Found.Should().BeTrue();
        }

        [TestInitialize]
        public void Initialize()
        {
            m_Service = new MemoryBankService(new List<IMemoryBank> { new MemoryBank(32) });
        }
    }
}
