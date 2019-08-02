using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
