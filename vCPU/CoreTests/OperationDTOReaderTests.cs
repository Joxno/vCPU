using System;
using System.Collections.Generic;
using Core.Components;
using Core.DTO;
using Core.Interfaces;
using Core.Models;
using Core.Services;
using CoreTests.Factories;
using FluentAssertions;
using NUnit.Framework;

namespace CoreTests
{
    [TestFixture]
    public class OperationDTOReaderTests
    {
        private IMemoryBank m_Bank = null;
        private IOperationDTOReader m_Reader = null;

        [Test]
        public void ReadNoOpFromRawDataWithGenericReader()
        {
            var t_DTO = m_Reader.ReadMemory(new MemoryAddress(0), m_Bank);

            t_DTO.Should().NotBeNull();
            t_DTO.OpCode.Should().Be(0);
            t_DTO.Data.Length.Should().Be(0);
        }

        [Test]
        public void ReadOpLoadValueFromRawDataWithGenericReader()
        {
            _WriteOpLoadToBank(m_Bank);

            var t_DTO = m_Reader.ReadMemory(new MemoryAddress(0), m_Bank);

            t_DTO.OpCode.Should().Be(1);
            t_DTO.Should().NotBeNull();
            t_DTO.Data.Length.Should().Be(3 * 4);
        }

        [Test]
        public void CheckCanReadOperation()
        {
            var t_CanRead = m_Reader.CanRead(0);

            t_CanRead.Should().BeTrue();
        }

        [SetUp]
        public void Initialize()
        {
            m_Bank = new MemoryBank(64);
            m_Reader = new OperationDTOReader(
                ArchitectureFactory.CreateArchitecture(new MemoryBankService(new List<IMemoryBank> {m_Bank})));
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
