using System.Collections.Generic;
using Core.Architecture.vCPU.Converters;
using Core.DTO;
using Core.Interfaces;
using Core.Operations.Converters;

namespace Core.Architecture.vCPU.Architecture
{
    public class vCPUArchitecture : Models.Architecture
    {
        public vCPUArchitecture(IMemoryBankService BankService)
        : base("vCPU", _CreateConverters(BankService), _CreateDefinitions())
        {
        }

        private static Dictionary<int, IOperationConverter> _CreateConverters(IMemoryBankService BankService)
        {
            return new Dictionary<int, IOperationConverter>
            {
                { 0, new NoOpConverter() },
                { 1, new OpAddConverter(BankService) },
                { 5, new OpSubConverter(BankService) },
                { 10, new OpLoadConstConverter(BankService) },
                { 11, new OpLoadConverter(BankService) }
            };
        }

        private static List<OperationDefinition> _CreateDefinitions()
        {
            return new List<OperationDefinition>
            {
                new OperationDefinition(0, 0)
            };
        }
    }
}
