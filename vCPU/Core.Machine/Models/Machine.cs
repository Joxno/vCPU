using Core.Interfaces;
using Core.Machine.Interfaces;
using Core.Models;

namespace Core.Machine.Models
{
    public class Machine : IMachine
    {
        private readonly IMemoryBankService m_BankService = null;
        private readonly ICPU m_CPU = null;
        private readonly IOscillator m_Oscillator = null;
        public bool IsRunning { get; private set; } = false;
        public bool IsSuspended { get; private set; } = false;

        public Machine(
            ICPU CPU,
            IOscillator MachineOscillator,
            IMemoryBankService BankService)
        {
            m_Oscillator = MachineOscillator;
            m_CPU = CPU;
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

        public T InspectMemory<T>(MemoryLocationAddress AddressLocation) where T : struct
        {
            return _LookupMemoryBank(AddressLocation.BankAddress)
                .Load<T>(AddressLocation.Address);
        }

        private IMemoryBank _LookupMemoryBank(MemoryBankAddress Address)
        {
            return m_BankService.ResolveAddress(Address);
        }
    }
}
