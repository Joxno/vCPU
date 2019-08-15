using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DTO;
using Core.Interfaces;
using Core.Models;

namespace Core.Components
{
    public class OperationDTOReader : IOperationDTOReader
    {
        private IArchitecture m_Arch = null;

        public OperationDTOReader(IArchitecture Architecture)
        {
            m_Arch = Architecture;
        }

        public OperationDTO ReadMemory(MemoryAddress Address, IMemoryBank Bank)
        {
            var t_Definition = _LookupDefinition(_ReadOpCode(Address, Bank));
            return _CreateDTO(t_Definition, Address, Bank);
        }

        public bool CanRead(byte OpCode)
        {
            return m_Arch.HasDefinitionForCode(OpCode);
        }

        private OperationDefinition _LookupDefinition(byte OpCode)
        {
            return m_Arch.GetDefinitionForCode(OpCode);
        }

        private byte _ReadOpCode(MemoryAddress Address, IMemoryBank Bank)
        {
            return Bank.Load<byte>(Address);
        }

        private OperationDTO _CreateDTO(OperationDefinition Definition, MemoryAddress Address, IMemoryBank Bank)
        {
            return new OperationDTO
            (
                Definition.OpCode,
                Definition.Size == 0 ? 
                new byte[] {} :
                _ReadData(Definition.Size, Address + new MemoryAddress(1), Bank)
            );
        }

        private byte[] _ReadData(int Count, MemoryAddress Address, IMemoryBank Bank)
        {
            var t_CurrentAddress = Address;
            var t_Data = new List<byte>();
            for (int i = 0; i < Count; i++, t_CurrentAddress = _IncrementAddress(t_CurrentAddress))
                t_Data.Add(Bank.Load<byte>(t_CurrentAddress));

            return t_Data.ToArray();
        }

        private MemoryAddress _IncrementAddress(MemoryAddress Address)
        {
            return Address + new MemoryAddress(1);
        }
    }
}
