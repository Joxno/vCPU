using Core.Interfaces;
using Core.Models;
using Core.Operations;

namespace Core.Components
{
    public class OperationFetcher : IOperationFetcher
    {
        private readonly IOperationReader m_Reader = null;
        private readonly IMemoryBankService m_BankService = null;
        private IMemoryBank m_CurrentBank = null;
        private readonly IMemoryLocationAddressReader m_LocationReader = null;

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
            _SetReadAndNextLocations();
        }

        private IOperation _ReadOperation()
        {
            m_CurrentLocationAddress = _ReadLocation();
            var t_Operation = _ReadOperationFromMemory(m_CurrentLocationAddress);
            _IncrementAddress();
            return t_Operation;
        }

        private IMemoryBank _ResolveMemoryBank(MemoryBankAddress Address)
        {
            return m_BankService.ResolveAddress(Address);
        }

        private void _SetReadAndNextLocations()
        {
            m_CurrentLocationAddress = _ReadLocation();
            m_NextLocationAddress = _ReadNextLocation();
        }

        private MemoryLocationAddress _ReadLocation()
        {
            return m_LocationReader.ReadLocationAddressFromMemory(FetchFromAddress, m_CurrentBank);
        }

        private MemoryLocationAddress _ReadNextLocation()
        {
            var t_Next = _ReadLocation();
            return new MemoryLocationAddress(
                _ReadNextOperationAddress(t_Next), 
                t_Next.BankAddress);
        }

        private IOperation _ReadOperationFromMemory(MemoryLocationAddress LocationAddress)
        {
            return m_Reader.ReadOperationFromMemory(LocationAddress.Address,
                _ResolveMemoryBank(LocationAddress.BankAddress));
        }

        private void _IncrementAddress()
        {
            m_NextLocationAddress = new MemoryLocationAddress(
                _ReadNextOperationAddress(m_CurrentLocationAddress), 
                m_NextLocationAddress.BankAddress
            );
            _WriteNextAddress(NextReadAddress);
        }

        private MemoryAddress _ReadNextOperationAddress(MemoryLocationAddress LocationAddress)
        {
            return m_Reader.ReadNextOperationAddress(LocationAddress.Address,
                _ResolveMemoryBank(LocationAddress.BankAddress));
        }

        private void _WriteNextAddress(MemoryAddress Address)
        {
            m_CurrentBank.Store(Address.Value, FetchFromAddress);
        }
    }
}
