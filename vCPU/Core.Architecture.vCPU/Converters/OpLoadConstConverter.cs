using Core.Architecture.vCPU.DTO;
using Core.DTO;
using Core.Interfaces;
using Core.Models;
using Core.Utility;
using Core.Architecture.vCPU.Operations;

namespace Core.Architecture.vCPU.Converters
{
    public class OpLoadConstConverter : IOperationConverter
    {
        private readonly ValueTypeConverter m_Converter = new ValueTypeConverter();
        private readonly IMemoryBankService m_BankService = null;

        public OpLoadConstConverter(IMemoryBankService Service)
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

        private OpLoadConst<int> _CreateOperation(OpLoadData Data)
        {
            return new OpLoadConst<int>(
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
    }
}
