using Core.DTO;
using Core.Interfaces;
using Core.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Exceptions;
using Core.Models;

namespace Core.Components
{
    public class OperationReader : IOperationReader
    {
        private Dictionary<int, IOperationConverter> m_Converters = null;
        private IOperationDTOReader m_DTOReader = null;

        public OperationReader(Dictionary<int, IOperationConverter> Converters,
            IOperationDTOReader OperationDTOReader)
        {
            m_Converters = Converters;
            m_DTOReader = OperationDTOReader;
        }

        public IOperation ReadOperation(OperationDTO DTO)
        {
            return 
                _HasConverterForOperation(DTO.OpCode) ?
                _RetrieveConverter(DTO.OpCode)
                .Convert(DTO) 
                :
                throw new UnknownOperation(DTO);
        }

        public IOperation ReadOperationFromMemory(MemoryAddress Address, IMemoryBank Bank)
        {
            return ReadOperation(_ReadOperationDTOFromMemory(Address, Bank));
        }

        public MemoryAddress ReadNextOperationAddress(MemoryAddress Address, IMemoryBank Bank)
        {
            var t_DTO = _ReadOperationDTOFromMemory(Address, Bank);
            return Address + new MemoryAddress(t_DTO.Size);
        }

        private IOperationConverter _RetrieveConverter(int Code)
        {
            return m_Converters[Code];
        }

        private bool _HasConverterForOperation(int OpCode)
        {
            return m_Converters.ContainsKey(OpCode);
        }

        private OperationDTO _ReadOperationDTOFromMemory(MemoryAddress Address, IMemoryBank Bank)
        {
            var t_OpCode = _InspectCode(Address, Bank);
            return 
                m_DTOReader.CanRead(t_OpCode) ?
                m_DTOReader.ReadMemory(Address, Bank) :
                throw new UnknownOperation(t_OpCode);
        }

        private byte _InspectCode(MemoryAddress Address, IMemoryBank Bank)
        {
            return Bank.Load<byte>(Address);
        }
    }
}
