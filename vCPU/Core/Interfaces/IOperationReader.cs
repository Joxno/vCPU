using Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Interfaces
{
    public interface IOperationReader
    {
        IOperation ReadOperation(OperationDTO DTO);
        IOperation ReadOperationFromMemory(MemoryAddress Address, IMemoryBank Bank);
    }
}
