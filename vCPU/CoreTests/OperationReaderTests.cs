using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;
using Core.Interfaces;
using Core.Components;
using FluentAssertions;
using Core.Operations;
using Core.DTO;
using Core.Exceptions;
using Core.Models;
using Core.Operations.Converters;
using Core.Services;
using CoreTests.Factories;
using Core.Architecture.vCPU.Operations;

namespace CoreTests
{
    [TestFixture]
    public class OperationReaderTests
    {
        private IMemoryBankService m_BankService = null;
        private IOperationReader m_Reader = null;

        [Test]
        public void ReadANoOp()
        {
            var t_Operation = m_Reader.ReadOperation(new OperationDTO(0));

            t_Operation.Should().BeOfType<NoOp>();
        }

        [Test]
        public void ReadALoadOp()
        {
            var t_Operation = m_Reader.ReadOperation(new OperationDTO(1, new byte[]
            {
                10, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 0
            }));

            t_Operation.Should().BeOfType<OpLoadConst<int>>();
        }

        [Test]
        public void ReadALoadAddressOp()
        {
            var t_Operation = m_Reader.ReadOperation(new OperationDTO(2, new byte[]
            {
                0, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 0
            }));

            t_Operation.Should().BeOfType<OpLoad<int>>();
        }

        [Test]
        public void ReadInvalidOpCode()
        {
            Action t_Invalid = () => m_Reader.ReadOperation(new OperationDTO(255, new byte[] { }));

            t_Invalid.Should().Throw<UnknownOperation>();
        }

        [Test]
        public void ReadInvalidOpCodeFromMemory()
        {
            var t_Bank = new MemoryBank(32);
            t_Bank.Store(255, new MemoryAddress(0));

            Action t_Invalid = () => m_Reader.ReadOperationFromMemory(new MemoryAddress(0), t_Bank);

            t_Invalid.Should().Throw<UnknownOperation>();
        }

        [Test]
        public void ReadNoOpFromMemory()
        {
            var t_Bank = new MemoryBank(32);
            var t_Operation = m_Reader.ReadOperationFromMemory(new MemoryAddress(0), t_Bank);

            t_Operation.Should().BeOfType<NoOp>();
        }

        [Test]
        public void ReadOpLoadValueFromMemory()
        {
            var t_Bank = new MemoryBank(32);
            _WriteOpLoadToBank(t_Bank);
            var t_Operation = m_Reader.ReadOperationFromMemory(new MemoryAddress(0), t_Bank);

            t_Operation.Should().BeOfType<OpLoadConst<int>>();
        }

        [Test]
        public void ReadNextOperationLocation()
        {
            var t_Bank = new MemoryBank(64);
            _WriteOpLoadToBank(t_Bank);

            var t_NextAddress = m_Reader.ReadNextOperationAddress(new MemoryAddress(0), t_Bank);

            (t_NextAddress == new MemoryAddress(1 + 3 * 4))
                .Should().BeTrue();
        }

        [SetUp]
        public void Initialize()
        {
            m_BankService = new MemoryBankService(new List<IMemoryBank>() { new MemoryBank(32) });
            m_Reader = new OperationReader(ArchitectureFactory.CreateArchitecture(m_BankService), _CreateReader());
        }

        private IOperationDTOReader _CreateReader()
        {
            return new OperationDTOReader(ArchitectureFactory.CreateArchitecture(m_BankService));
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

            for (int i = 0; i < t_Data.Length; i++)
                Bank.Store(t_Data[i], new MemoryAddress(i));
        }
    }
}
