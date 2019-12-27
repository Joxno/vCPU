using Core.DTO;
using Core.Interfaces;
using Core.Exceptions;
using Core.Models;

namespace Core.Components
{
    public class OperationReader : IOperationReader
    {
        private IArchitecture m_Arch = null;
        private IOperationDTOReader m_DTOReader = null;
        
        public OperationReader(IArchitecture Architecture,
            IOperationDTOReader OperationDTOReader)
        {
            m_Arch = Architecture;
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
            return m_Arch.GetConverterForCode(Code);
        }

        private bool _HasConverterForOperation(int OpCode)
        {
            return m_Arch.HasConverterForCode(OpCode);
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
