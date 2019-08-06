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

        [TestMethod]
        public void ReadOpLoadValueFromRawData()
        {
            _WriteOpLoadToBank(m_Bank);
            var t_Reader = new OpLoadReader();
            var t_DTO = t_Reader.ReadMemory(new MemoryAddress(0), m_Bank);

            t_DTO.Should().NotBeNull();
            t_DTO.Data.Length.Should().Be(3 * 4);
        }

        [TestInitialize]
        public void Initialize()
        {
            m_Bank = new MemoryBank(64);
        }

        private void _WriteOpLoadToBank(IMemoryBank Bank)
        {
            var t_Data = new byte[]
            {
                1,
                0, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 0
            };

            for(int i = 0; i < t_Data.Length; i++)
                Bank.Store(t_Data[i], new MemoryAddress(i));
        }
    }
}
