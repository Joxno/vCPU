using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                { 0, new NoOpConverter() }
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
