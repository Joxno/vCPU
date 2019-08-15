using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DTO;
using Core.Interfaces;
using Core.Models;
using Core.Operations.Converters;

namespace CoreTests.Factories
{
    public static class ArchitectureFactory
    {
        public static IArchitecture CreateArchitecture(IMemoryBankService BankService)
        {
            return new Architecture
            (
                new Dictionary<int, IOperationConverter>
                {
                    { 0, new NoOpConverter() },
                    { 1, new OpLoadConstConverter(BankService) },
                    { 2, new OpLoadConverter(BankService) }
                },
                new List<OperationDefinition>
                {
                    new OperationDefinition(0, 0),
                    new OperationDefinition(1, 4*3),
                    new OperationDefinition(2, 4*4)
                }
            );
        }
    }
}
