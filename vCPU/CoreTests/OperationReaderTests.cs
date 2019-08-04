using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Interfaces;
using Core.Components;
using FluentAssertions;
using Core.Operations;
using Core.DTO;
using Core.Exceptions;
using Core.Operations.Converters;
using Core.Services;

namespace CoreTests
{
    [TestClass]
    public class OperationReaderTests
    {
        private IMemoryBankService m_BankService = null;
        private IOperationReader m_Reader = null;

        [TestMethod]
        public void ReadANoOp()
        {
            var t_Operation = m_Reader.ReadOperation(new OperationDTO(0));

            t_Operation.Should().BeOfType<NoOp>();
        }

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
        public void ReadInvalidOpCode()
        {
            Action t_Invalid = () => m_Reader.ReadOperation(new OperationDTO(255, new byte[] { }));

            t_Invalid.Should().Throw<UnknownOperation>();
        }

        [TestInitialize]
        public void Initialize()
        {
            m_BankService = new MemoryBankService(new List<IMemoryBank>() { new MemoryBank(32) });
            m_Reader = new OperationReader(_CreateConverters());
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
    }
}
