﻿using System;
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
    public class OpAddConverter : IOperationConverter
    {
        private AnyConverter m_TypeConverter = new AnyConverter();
        private IMemoryBankService m_BankService = null;

        public OpAddConverter(IMemoryBankService BankService)
        {
            m_BankService = BankService;
        }

        public IOperation Convert(OperationDTO DTO)
        {
            return _CreateOperation(
                _ConvertData(DTO.Data));
        }

        private OpAddData _ConvertData(byte[] Data)
        {
            return m_TypeConverter.ConvertBytesToValueType<OpAddData>(Data);
        }

        private OpAdd _CreateOperation(OpAddData Data)
        {
            return new OpAdd
            (
                new MemoryLocation(new MemoryAddress(Data.FirstAddress),
                    _LookUpMemoryBank(new MemoryBankAddress(Data.FirstBankAddress))),
                new MemoryLocation(new MemoryAddress(Data.SecondAddress),
                    _LookUpMemoryBank(new MemoryBankAddress(Data.SecondBankAddress))),
                new MemoryLocation(new MemoryAddress(Data.DestinationAddress),
                    _LookUpMemoryBank(new MemoryBankAddress(Data.DestinationBankAddress)))
            );
        }

        private IMemoryBank _LookUpMemoryBank(MemoryBankAddress Address)
        {
            return m_BankService.ResolveAddress(Address);
        }

        internal struct OpAddData
        {
            public int FirstAddress;
            public int FirstBankAddress;
            public int SecondAddress;
            public int SecondBankAddress;
            public int DestinationAddress;
            public int DestinationBankAddress;
        }
    }
}