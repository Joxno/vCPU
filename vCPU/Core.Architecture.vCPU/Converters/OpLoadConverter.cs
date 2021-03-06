﻿using Core.Architecture.vCPU.DTO;
using Core.Architecture.vCPU.Operations;
using Core.DTO;
using Core.Interfaces;
using Core.Models;
using Core.Utility;

namespace Core.Architecture.vCPU.Converters
{
    public class OpLoadConverter : IOperationConverter
    {
        private readonly ValueTypeConverter m_Converter = new ValueTypeConverter();
        private readonly IMemoryBankService m_BankService = null;

        public OpLoadConverter(IMemoryBankService BankService)
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
            return new OpLoad<int>
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
    }
}
