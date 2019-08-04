using Core.DTO;
using Core.Interfaces;
using Core.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Exceptions;

namespace Core.Components
{
    public class OperationReader : IOperationReader
    {
        private Dictionary<int, IOperationConverter> m_Converters = null;

        public OperationReader(Dictionary<int, IOperationConverter> Converters)
        {
            m_Converters = Converters;
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

        private IOperationConverter _RetrieveConverter(int Code)
        {
            return m_Converters[Code];
        }

        private bool _HasConverterForOperation(int OpCode)
        {
            return m_Converters.ContainsKey(OpCode);
        }
    }
}
