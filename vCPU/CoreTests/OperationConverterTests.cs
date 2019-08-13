using System;
using System.Collections.Generic;
using Core.Components;
using Core.DTO;
using Core.Interfaces;
using Core.Operations;
using Core.Operations.Converters;
using Core.Services;
using FluentAssertions;
using NUnit.Framework;

namespace CoreTests
{
    [TestFixture]
    public class OperationConverterTests
    {
        private IMemoryBankService m_BankService = null;

        [Test]
        public void ConvertNoOp()
        {
            var t_Converter = new NoOpConverter();
            var t_NoOp = t_Converter.Convert(new OperationDTO(0));

            t_NoOp.Should().BeOfType<NoOp>();
        }

        [Test]
        public void ConvertLoadValueOp()
        {
            var t_Converter = new OpLoadConstConverter(m_BankService);
            var t_LoadOp = t_Converter.Convert(new OperationDTO(1, new byte[] 
            {
                10, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 0
            }));

            t_LoadOp.Should().BeOfType<OpLoadConst<int>>();
        }

        [Test]
        public void ConvertLoadAddressOp()
        {
            var t_Converter = new OpLoadConverter(m_BankService);
            var t_LoadAddressOp = t_Converter.Convert(new OperationDTO(2, new byte[]
            {
                0, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 0,
            }));

            t_LoadAddressOp.Should().BeOfType<OpLoad<int>>();
        }

        [SetUp]
        public void Initialize()
        {
            m_BankService = new MemoryBankService(new List<IMemoryBank> { new MemoryBank(128) });
        }
    }
}
