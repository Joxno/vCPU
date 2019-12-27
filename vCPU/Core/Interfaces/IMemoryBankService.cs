using Core.Models;

namespace Core.Interfaces
{
    public interface IMemoryBankService
    {
        IMemoryBank ResolveAddress(MemoryBankAddress Address);
        MemoryBankAddress Attach(IMemoryBank Bank);
        void AttachAtAddress(IMemoryBank Bank, MemoryBankAddress Address);
        void Detach(MemoryBankAddress Address);
        bool HasBankAtAddress(MemoryBankAddress Address);
    }
}
