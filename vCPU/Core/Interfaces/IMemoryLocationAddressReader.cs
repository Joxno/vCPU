using Core.Models;

namespace Core.Interfaces
{
    public interface IMemoryLocationAddressReader
    {
        MemoryLocationAddress ReadLocationAddressFromMemory(MemoryAddress Address, IMemoryBank Bank);
    }
}
