using Core.Interfaces;
using Core.Models;

namespace Core.Components
{
    public class MemoryLocationAddressReader : IMemoryLocationAddressReader
    {
        public MemoryLocationAddress ReadLocationAddressFromMemory(MemoryAddress Address, IMemoryBank Bank)
        {
            return new MemoryLocationAddress(_ReadAddress(Address, Bank), _ReadBankAddress(Address, Bank));
        }

        private MemoryAddress _ReadAddress(MemoryAddress Address, IMemoryBank Bank)
        {
            return new MemoryAddress(Bank.Load<int>(Address));
        }

        private MemoryBankAddress _ReadBankAddress(MemoryAddress Address, IMemoryBank Bank)
        {
            return new MemoryBankAddress(Bank.Load<int>(Address + new MemoryAddress(4)));
        }
    }
}
