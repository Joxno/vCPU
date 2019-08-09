using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DTO;
using Core.Interfaces;
using Core.Models;
using Core.Utility;

namespace Core.Operations.Converters
{
    public class OpLoadAddressConverter : IOperationConverter
    {
        private AnyConverter m_Converter = new AnyConverter();
        private IMemoryBankService m_BankService = null;

        public OpLoadAddressConverter(IMemoryBankService BankService)
        {
            m_BankService = BankService;
        }

        public IOperation Convert(OperationDTO DTO)
        {
            return _CreateLoadAddressOperation(
                _ConvertDTOData(DTO));
        }

        private IOperation _CreateLoadAddressOperation(OpLoadAddressData Data)
        {
            return new OpLoadAddress<int>
            (
                new MemoryLocation(new MemoryAddress(Data.FromAddress),
                    _LookupMemoryBank(new MemoryBankAddress(Data.FromBankAddress))), 
                new MemoryLocation(new MemoryAddress(Data.ToAddress),
                    _LookupMemoryBank(new MemoryBankAddress(Data.ToBankAddress)))
            );
        }

        private OpLoadAddressData _ConvertDTOData(OperationDTO DTO)
        {
            return m_Converter
                .ConvertBytesToValueType<OpLoadAddressData>(DTO.Data);
        }

        private IMemoryBank _LookupMemoryBank(MemoryBankAddress Address)
        {
            return m_BankService.ResolveAddress(Address);
        }

        internal struct OpLoadAddressData
        {
            public int FromAddress;
            public int FromBankAddress;
            public int ToAddress;
            public int ToBankAddress;
        }
    }
}
