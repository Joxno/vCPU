using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Interfaces
{
    public interface IOperationFetcher
    {
        void SetFetchAddress(MemoryAddress MemoryAddress, MemoryBankAddress BankAddress);
        MemoryAddress CurrentAddress { get; }
        IOperation FetchOperation();
    }
}
