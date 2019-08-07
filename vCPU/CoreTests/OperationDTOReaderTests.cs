using System;
using System.Collections.Generic;
using Core.Components;
using Core.DTO;
using Core.Interfaces;
using Core.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoreTests
{
    [TestClass]
    public class OperationDTOReaderTests
    {
        private IMemoryBank m_Bank = null;

        [TestMethod]
        public void ReadNoOpFromRawDataWithGenericReader()
        {
            var t_Reader = new OperationDTOReader(
                new List<OperationDefinition>
                {
                    new OperationDefinition(0, 0)
                });

            var t_DTO = t_Reader.ReadMemory(new MemoryAddress(0), m_Bank);

            t_DTO.Should().NotBeNull();
            t_DTO.OpCode.Should().Be(0);
            t_DTO.Data.Length.Should().Be(0);
        }

        [TestMethod]
        public void ReadOpLoadValueFromRawDataWithGenericReader()
        {
            _WriteOpLoadToBank(m_Bank);
            var t_Reader = new OperationDTOReader(
                new List<OperationDefinition>
                {
                    new OperationDefinition(1, 3*4)
                });
            var t_DTO = t_Reader.ReadMemory(new MemoryAddress(0), m_Bank);

            t_DTO.OpCode.Should().Be(1);
            t_DTO.Should().NotBeNull();
            t_DTO.Data.Length.Should().Be(3 * 4);
        }

        [TestMethod]
        public void CheckCanReadOperation()
        {
            var t_Reader = new OperationDTOReader(
                new List<OperationDefinition>
                {
                    new OperationDefinition(0, 0)
                });

            var t_CanRead = t_Reader.CanRead(0);

            t_CanRead.Should().BeTrue();
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
