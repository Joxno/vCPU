using Core.DTO;
using Core.Interfaces;
using Core.Models;
using Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Operations.Converters
{
    public class OpLoadConverter : IOperationConverter
    {
        private AnyConverter m_Converter = new AnyConverter();
        private IMemoryBankService m_BankService = null;

        public OpLoadConverter(IMemoryBankService Service)
        {
            m_BankService = Service;
        }

        public IOperation Convert(OperationDTO DTO)
        {
            return _CreateOperation(_ConvertDTOData(DTO));
        }

        private OpLoadData _ConvertDTOData(OperationDTO DTO)
        {
            return m_Converter
                .ConvertBytesToValueType<OpLoadData>(DTO.Data);
        }

        private OpLoad<int> _CreateOperation(OpLoadData Data)
        {
            return new OpLoad<int>(
                Data.Value,
                new MemoryLocation
                (
                    new MemoryAddress(Data.MemoryAddress),
                    _LookupMemoryBank(new MemoryBankAddress(Data.BankAddress))
                )
            );
        }

        private IMemoryBank _LookupMemoryBank(MemoryBankAddress Address)
        {
            return m_BankService.ResolveAddress(Address);
        }

        internal struct OpLoadData
        {
            public int Value;
            public int MemoryAddress;
            public int BankAddress;
        }
    }
}
