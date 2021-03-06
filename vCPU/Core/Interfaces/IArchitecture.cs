﻿using System.Collections.Generic;
using Core.DTO;

namespace Core.Interfaces
{
    public interface IArchitecture
    {
        string Name { get; }
        IEnumerable<int> SupportedCodes();
        IOperationConverter GetConverterForCode(int Code);
        OperationDefinition GetDefinitionForCode(int Code);
        bool HasConverterForCode(int Code);
        bool HasDefinitionForCode(int Code);
    }
}
