using System;
using Core.Components;
using Core.Interfaces;
using Core.Models;
using Core.Operations.Readers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoreTests
{
    [TestClass]
    public class OperationDTOReaderTests
    {
        private IMemoryBank m_Bank = null;

        [TestMethod]
        public void ReadNoOpFromRawData()
        {
            m_Bank.Store(0, new MemoryAddress(0));
            var t_Reader = new NoOpReader();
            var t_DTO = t_Reader.ReadMemory(new MemoryAddress(0), m_Bank);

            t_DTO.Should().NotBeNull();
            t_DTO.OpCode.Should().Be(0);
            t_DTO.Data.Length.Should().Be(0);
        }

        [TestInitialize]
        public void Initialize()
        {
            m_Bank = new MemoryBank(64);
        }
    }
}
