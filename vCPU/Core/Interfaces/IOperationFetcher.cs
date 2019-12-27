using Core.Models;

namespace Core.Interfaces
{
    public interface IOperationFetcher
    {
        void SetFetchFromAddress(MemoryAddress MemoryAddress, MemoryBankAddress BankAddress);
        MemoryAddress FetchFromAddress { get; }
        MemoryAddress NextReadAddress { get; }
        MemoryAddress CurrentReadAddress { get; }
        IOperation FetchOperation();
    }
}
