using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using Core.Operations;

namespace Core.Components
{
    public class OperationFetcher : IOperationFetcher
    {
        private IOperationReader m_Reader = null;
        private IMemoryBankService m_BankService = null;
        private IMemoryBank m_CurrentBank = null;
        private IMemoryLocationAddressReader m_LocationReader = null;

        private MemoryLocationAddress m_NextLocationAddress =
            new MemoryLocationAddress(new MemoryAddress(0), new MemoryBankAddress(0));

        private MemoryLocationAddress m_CurrentLocationAddress =
            new MemoryLocationAddress(new MemoryAddress(0), new MemoryBankAddress(0));

        public MemoryAddress FetchFromAddress { get; private set; } = new MemoryAddress(0);
        public MemoryAddress NextReadAddress => m_NextLocationAddress.Address;
        public MemoryAddress CurrentReadAddress => m_CurrentLocationAddress.Address;

        public OperationFetcher(IOperationReader Reader, 
            IMemoryBankService BankService, 
            IMemoryLocationAddressReader LocationReader)
        {
            m_Reader = Reader;
            m_BankService = BankService;
            m_LocationReader = LocationReader;
        }

        public IOperation FetchOperation()
        {
            if (m_CurrentBank != null && m_CurrentBank.IsValid(FetchFromAddress))
                return _ReadOperation();

            return new NoOp();
        }

        public void SetFetchFromAddress(MemoryAddress MemoryAddress, MemoryBankAddress BankAddress)
        {
            FetchFromAddress = MemoryAddress;
            m_CurrentBank = _ResolveMemoryBank(BankAddress);
        }

        private IMemoryBank _ResolveMemoryBank(MemoryBankAddress Address)
        {
            return m_BankService.ResolveAddress(Address);
        }

        private IOperation _ReadOperation()
        {
            m_CurrentLocationAddress = _ReadNextLocation();
            var t_Operation = _ReadOperationFromMemory(m_CurrentLocationAddress);
            _IncrementAddress();
            return t_Operation;
        }

        private MemoryLocationAddress _ReadNextLocation()
        {
            return m_LocationReader.ReadLocationAddressFromMemory(FetchFromAddress, m_CurrentBank);
        }

        private IOperation _ReadOperationFromMemory(MemoryLocationAddress LocationAddress)
        {
            return m_Reader.ReadOperationFromMemory(LocationAddress.Address,
                _ResolveMemoryBank(LocationAddress.BankAddress));
        }

        private void _IncrementAddress()
        {
            m_NextLocationAddress.Address = _ReadNextOperationAddress(m_CurrentLocationAddress);
            _WriteNextAddress(NextReadAddress);
        }

        private MemoryAddress _ReadNextOperationAddress(MemoryLocationAddress LocationAddress)
        {
            return m_Reader.ReadNextOperationAddress(LocationAddress.Address,
                _ResolveMemoryBank(LocationAddress.BankAddress));
        }

        private void _WriteNextAddress(MemoryAddress Address)
        {
            m_CurrentBank.Store<int>(Address.Value, FetchFromAddress);
        }
    }
}
