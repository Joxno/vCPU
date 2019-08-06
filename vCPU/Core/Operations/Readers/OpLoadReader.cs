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
    public class OpLoadReader : IOperationDTOReader
    {
        public OperationDTO ReadMemory(MemoryAddress Address, IMemoryBank Bank)
        {
            return new OperationDTO
            (
                _ReadOpCode(Address, Bank), 
                _ReadData(4*3, Address + new MemoryAddress(1), Bank)
            );
        }

        private byte _ReadOpCode(MemoryAddress Address, IMemoryBank Bank)
        {
            return Bank.Load<byte>(Address);
        }

        private byte[] _ReadData(int Count, MemoryAddress Address, IMemoryBank Bank)
        {
            var t_CurrentAddress = Address;
            var t_Data = new List<byte>();
            for(int i = 0; i < Count; i++, t_CurrentAddress += new MemoryAddress(1))
                t_Data.Add(Bank.Load<byte>(t_CurrentAddress));

            return t_Data.ToArray();
        }

        private MemoryAddress _IncrementAddress(MemoryAddress Address)
        {
            return Address + new MemoryAddress(1);
        }
    }
}
