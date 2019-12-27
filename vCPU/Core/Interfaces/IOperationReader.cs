using Core.DTO;
using Core.Models;

namespace Core.Interfaces
{
    public interface IOperationReader
    {
        IOperation ReadOperation(OperationDTO DTO);
        IOperation ReadOperationFromMemory(MemoryAddress Address, IMemoryBank Bank);
        MemoryAddress ReadNextOperationAddress(MemoryAddress Address, IMemoryBank Bank);
    }
}
