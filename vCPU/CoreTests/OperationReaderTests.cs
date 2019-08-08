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

            t_Operation.Should().BeOfType<OpLoad<int>>();
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

            t_Operation.Should().BeOfType<OpLoadAddress<int>>();
        }

        [Test]
        public void ReadInvalidOpCode()
        {
            Action t_Invalid = () => m_Reader.ReadOperation(new OperationDTO(255, new byte[] { }));

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

            t_Operation.Should().BeOfType<OpLoad<int>>();
        }

        [SetUp]
        public void Initialize()
        {
            m_BankService = new MemoryBankService(new List<IMemoryBank>() { new MemoryBank(32) });
            m_Reader = new OperationReader(_CreateConverters(), _CreateReader());
        }

        private Dictionary<int, IOperationConverter> _CreateConverters()
        {
            return new Dictionary<int, IOperationConverter>
            {
                { 0, new NoOpConverter() },
                { 1, new OpLoadConverter(m_BankService) },
                { 2, new OpLoadAddressConverter(m_BankService) }
            };
        }

        private IOperationDTOReader _CreateReader()
        {
            return new OperationDTOReader(new List<OperationDefinition>
            {
                new OperationDefinition(0, 0),
                new OperationDefinition(1, 4*3)
            });
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
