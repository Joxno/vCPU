using Core.DTO;
using Core.Models;

namespace Core.Interfaces
{
    public interface IOperationDTOReader
    {
        OperationDTO ReadMemory(MemoryAddress Address, IMemoryBank Bank);
        bool CanRead(byte OpCode);
    }
}
