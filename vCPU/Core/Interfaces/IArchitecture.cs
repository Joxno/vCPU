using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DTO;

namespace Core.Interfaces
{
    public interface IArchitecture
    {
        IEnumerable<int> SupportedCodes();
        IOperationConverter GetConverterForCode(int Code);
        OperationDefinition GetDefinitionForCode(int Code);
        bool HasConverterForCode(int Code);
        bool HasDefinitionForCode(int Code);
    }
}
