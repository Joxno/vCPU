using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DTO;
using Core.Interfaces;
using Core.Models;

namespace Core.Operations.Readers
{
    public class NoOpReader : IOperationDTOReader
    {
        public OperationDTO ReadMemory(MemoryAddress Address, IMemoryBank Bank)
        {
            return new OperationDTO(_ReadOpCode(Address, Bank));
        }

        private byte _ReadOpCode(MemoryAddress Address, IMemoryBank Bank)
        {
            return Bank.Load<byte>(Address);
        }
    }
}
