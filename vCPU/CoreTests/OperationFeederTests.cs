using System;
using System.Collections.Generic;
using System.Linq;
using Core.Components;
using Core.DTO;
using Core.Interfaces;
using Core.Operations;
using Core.Operations.Converters;
using Core.Services;
using CoreTests.Factories;
using CoreTests.Mocks;
using FluentAssertions;
using NUnit.Framework;

namespace CoreTests
{
    [TestFixture]
    public class OperationFeederTests
    {
        private OperationFeeder m_Feeder = null;
        private ICPU m_CPU = null;
        private IOperationFetcher m_Fetcher = null;
        private MemoryBank m_Bank = null;
        private MemoryBankService m_BankService = null;

        [Test]
        public void FeedOperation()
        {
            var t_Consumer = new MockOperationConsumer();
            m_Feeder.AddConsumer(t_Consumer);
            m_Feeder.Feed();

            t_Consumer.OperationsCount
                .Should().Be(1);
            t_Consumer.Operations.First()
                .Should().BeOfType<NoOp>();
        }

        [SetUp]
        public void Initialize()
        {
            m_CPU = new CPU();
            m_Bank = new MemoryBank(64);
            m_BankService = new MemoryBankService(new List<IMemoryBank> { m_Bank });
            m_Fetcher = _CreateFetcher(m_BankService);
            m_Feeder = new OperationFeeder(m_Fetcher);
        }

        private IOperationFetcher _CreateFetcher(IMemoryBankService BankService)
        {
            return new OperationFetcher(_CreateReader(), BankService, new MemoryLocationAddressReader());
        }

        private IOperationReader _CreateReader()
        {
            return new OperationReader(new Dictionary<int, IOperationConverter>
            {
                { 0, new NoOpConverter() }
            }, new OperationDTOReader(ArchitectureFactory.CreateArchitecture(m_BankService)) );
        }
    }
}
