using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IAssembler
    {
        IOperation GenerateOperation(string Assembly);
        IMemoryBank GenerateOperationBank(string Assembly);
        IEnumerable<IOperation> GenerateOperations(IEnumerable<string> Assembly);
        IMemoryBank GenerateBankWithOperations(IEnumerable<string> Assembly);
    }
}
