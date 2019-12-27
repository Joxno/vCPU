using Core.Interfaces;
using Core.Machine.Interfaces;
using Core.Models;

namespace Core.Machine.Models
{
    public class Machine : IMachine
    {
        private readonly IMemoryBankService m_BankService = null;
        public bool IsRunning { get; private set; } = false;
        public bool IsSuspended { get; private set; } = false;

        public Machine(IMemoryBankService BankService)
        {
            m_BankService = BankService;
        }

        public void Start()
        {
            IsRunning = true;
        }

        public void Stop()
        {
            IsRunning = false;
        }

        public void Suspend()
        {
            IsSuspended = true;
        }

        public void Resume()
        {
            IsSuspended = false;
        }

        public byte InspectMemory(MemoryLocationAddress AddressLocation)
        {
            return _LookupMemoryBank(AddressLocation.BankAddress)
                .Load<byte>(AddressLocation.Address);
        }

        private IMemoryBank _LookupMemoryBank(MemoryBankAddress Address)
        {
            return m_BankService.ResolveAddress(Address);
        }
    }
}
