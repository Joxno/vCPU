using System;
using System.Collections.Generic;
using Core.Components;
using Core.DTO;
using Core.Exceptions;
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
        public void SetFetchFromAddress()
        {
            m_BankService.ResolveAddress(new MemoryBankAddress(0))
                .Store(5, new MemoryAddress(1));

            m_Fetcher.SetFetchFromAddress(new MemoryAddress(1), new MemoryBankAddress(0));

            m_Fetcher.FetchFromAddress
                .Should().Be(new MemoryAddress(1), "We set fetch address to 1.");
            m_Fetcher.CurrentReadAddress
                .Should().Be(new MemoryAddress(5), "We stored the address 1 in the FetchAddress location.");
            m_Fetcher.NextReadAddress
                .Should().Be(new MemoryAddress(6),
                    "We stored the address 1 in the FetchAddress location and this should show the address of the next operation to be read.");
        }

        [Test]
        public void FetchOperationFromMemory()
        {
            m_Fetcher.SetFetchFromAddress(new MemoryAddress(0), new MemoryBankAddress(0));
            var t_Operation = m_Fetcher.FetchOperation();

            t_Operation.Should().BeOfType<NoOp>();
        }

        [Test]
        public void SetFetchAddressToInvalidMemory()
        {
            Action t_Invalid = () => m_Fetcher.SetFetchFromAddress(new MemoryAddress(200), new MemoryBankAddress(0));

            t_Invalid.Should().Throw<AddressOutOfRange>();
        }

        [Test]
        public void NextAddressInInvalidMemory()
        {
            m_BankService.ResolveAddress(new MemoryBankAddress(0))
                .Store(127, new MemoryAddress(0));
            m_Fetcher.SetFetchFromAddress(new MemoryAddress(0), new MemoryBankAddress(0));
        }

        [Test]
        public void ReadUntilInvalidMemory()
        {
            var t_OpBank = new MemoryBank(1);
            m_BankService.Attach(t_OpBank);

            m_BankService.ResolveAddress(new MemoryBankAddress(0)).Store(1, new MemoryAddress(4));
            m_Fetcher.SetFetchFromAddress(new MemoryAddress(0), new MemoryBankAddress(0));

            Action t_InvalidMemory = () =>
            {
                m_Fetcher.FetchOperation();
                m_Fetcher.FetchOperation();
                m_Fetcher.FetchOperation();
            };

            t_InvalidMemory.Should().Throw<AddressOutOfRange>();
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
                { 1, new OpLoadConstConverter(m_BankService) }
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
