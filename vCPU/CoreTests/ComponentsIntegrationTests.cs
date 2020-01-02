using System;
using System.Collections.Generic;
using Core.Components;
using Core.DTO;
using Core.Interfaces;
using Core.Models;
using Core.Services;
using FluentAssertions;
using NUnit.Framework;

namespace CoreTests
{
    [TestFixture]
    public class ComponentsIntegrationTests
    {
        private CPU m_CPU = null;
        private IOscillator m_Oscillator = null;
        private IMemoryBankService m_BankService = null;
        private IMemoryBank m_Bank = null;
        private IArchitecture m_Arch = null;
        private IOperationFeeder m_OpFeeder = null;
        private IOperationFetcher m_Fetcher = null;
        private IOperationDTOReader m_OpDtoReader = null;
        private IOperationReader m_OpReader = null;
        private IMemoryLocationAddressReader m_AddressReader = null;
        private IOperationConsumer m_CPUConsumer = null;

        [Test]
        public void FetchOperationFromMemory()
        {
            var t_Operation = m_Fetcher.FetchOperation();

            t_Operation
                .Should()
                .BeOfType<MockNoOp>();

            m_Fetcher.NextReadAddress
                .Should()
                .Be(new MemoryAddress(1));
        }

        [Test]
        public void FeedOperationFromMemory()
        {
            m_CPU.QueuedOperations
                .Should()
                .Be(0);

            m_OpFeeder.Feed();

            m_CPU.QueuedOperations
                .Should()
                .Be(1);
            m_Fetcher.NextReadAddress
                .Should()
                .Be(new MemoryAddress(1));
        }

        [Test]
        public void FeedAndTick()
        {
            m_OpFeeder.Feed();
            m_Oscillator.Oscillate();

            m_CPU.QueuedOperations
                .Should()
                .Be(0);
            m_CPU.ExecutedOperations
                .Should()
                .Be(1);
        }


        [SetUp]
        public void Initialize()
        {
            m_Arch = _CreateArchitecture();

            m_CPU = new CPU();
            m_Oscillator = new Clock();
            m_Bank = new MemoryBank(512);
            m_BankService = new MemoryBankService(new List<IMemoryBank>() { m_Bank });
            m_AddressReader = new MemoryLocationAddressReader();
            m_OpDtoReader = new OperationDTOReader(m_Arch);
            m_OpReader = new OperationReader(m_Arch, m_OpDtoReader);

            m_Fetcher = new OperationFetcher(m_OpReader, m_BankService, m_AddressReader);
            m_OpFeeder = new OperationFeeder(m_Fetcher);
            m_CPUConsumer = new CPUConsumer(m_CPU);

            SetupComponents();
        }

        private IArchitecture _CreateArchitecture()
        {
            return new Architecture("Foobar", new Dictionary<int, IOperationConverter>()
            {
                { 0, new MockNoOpConverter() }
            }, new List<OperationDefinition>()
            {
                new OperationDefinition(0, 0)
            });
        }

        private void SetupComponents()
        {
            m_Oscillator.Add(m_CPU);
            m_OpFeeder.AddConsumer(m_CPUConsumer);
            m_Fetcher.SetFetchFromAddress(new MemoryAddress(0), new MemoryBankAddress(0));
        }

        internal class MockNoOpConverter : IOperationConverter
        {
            public IOperation Convert(OperationDTO DTO)
            {
                if(DTO.OpCode == 0 && DTO.TotalSize == 1)
                    return new MockNoOp();

                throw new Exception("Invalid OpCode");
            }
        }

        internal class CPUConsumer : IOperationConsumer
        {
            private readonly ICPU m_CPU = null;
            public CPUConsumer(ICPU CPU)
            {
                m_CPU = CPU;
            }

            public void Consume(IOperation Operation)
            {
                m_CPU.QueueOperation(Operation);
            }
        }

        internal class MockNoOp : IOperation
        {
            public void Execute()
            {
                
            }
        }
    }
}