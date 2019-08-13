using System;
using System.Collections.Generic;
using Core.Components;
using Core.DTO;
using Core.Interfaces;
using Core.Models;
using Core.Operations;
using Core.Operations.Converters;
using Core.Services;
using FluentAssertions;
using NUnit.Framework;

namespace CoreTests
{
    [TestFixture]
    public class OperationFetcherTests
    {
        private IOperationFetcher m_Fetcher = null;
        private IMemoryBankService m_BankService = null;

        [Test]
        public void SetCurrentAddress()
        {
            m_Fetcher.SetFetchFromAddress(new MemoryAddress(1), new MemoryBankAddress(0));
            m_Fetcher.FetchFromAddress
                .Should().Be(new MemoryAddress(1));
        }

        [Test]
        public void FetchOperationFromMemory()
        {
            m_Fetcher.SetFetchFromAddress(new MemoryAddress(0), new MemoryBankAddress(0));
            var t_Operation = m_Fetcher.FetchOperation();

            t_Operation.Should().BeOfType<NoOp>();
        }

        [Test]
        public void FetchOperationFromInvalidMemory()
        {
            m_Fetcher.SetFetchFromAddress(new MemoryAddress(200), new MemoryBankAddress(0));
            var t_Operation = m_Fetcher.FetchOperation();

            t_Operation.Should().BeOfType<NoOp>();
        }

        [Test]
        public void FetchOperationWithoutSettingAMemoryAddress()
        {
            var t_Operation = m_Fetcher.FetchOperation();
            t_Operation.Should().BeOfType<NoOp>();
        }

        [Test]
        public void FetchOperationShouldIncrementAddressInFetchAddress()
        {
            m_Fetcher.SetFetchFromAddress(new MemoryAddress(0), new MemoryBankAddress(0));
            m_Fetcher.FetchOperation();

            m_Fetcher.FetchFromAddress
                .Should().Be(new MemoryAddress(0));
            m_Fetcher.NextReadAddress
                .Should().Be(new MemoryAddress(1));
            m_Fetcher.CurrentReadAddress
                .Should().Be(new MemoryAddress(0));
        }

        [SetUp]
        public void Initialize()
        {
            m_BankService = new MemoryBankService(new List<IMemoryBank>() { _CreateMemoryBank() });
            m_Fetcher = new OperationFetcher(_CreateReader(), m_BankService, new MemoryLocationAddressReader());
        }

        private IOperationReader _CreateReader()
        {
            return new OperationReader(new Dictionary<int, IOperationConverter>
            {
                { 0, new NoOpConverter() },
                { 1, new OpLoadConverter(m_BankService) }
            }, 
            new OperationDTOReader(new List<OperationDefinition>
            {
                new OperationDefinition(0, 0),
                new OperationDefinition(1, 3*4)
            }) );
        }

        private IMemoryBank _CreateMemoryBank()
        {
            return new MemoryBank(128);
        }
    }
}
