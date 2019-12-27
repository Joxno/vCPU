using System;
using System.Collections.Generic;
using System.Linq;
using Core.DTO;
using Core.Interfaces;

namespace Core.Models
{
    public class Architecture : IArchitecture
    {
        private Dictionary<int, Tuple<IOperationConverter, OperationDefinition>> m_Operations =
            new Dictionary<int, Tuple<IOperationConverter, OperationDefinition>>();

        public string Name { get; private set; }

        public Architecture(
            string ArchitectureName,
            Dictionary<int, IOperationConverter> Converters, 
            List<OperationDefinition> Definitions)
        {
            Name = ArchitectureName;

            _ExtractCodes(Definitions)
                .ForEach(C =>
                    m_Operations[C] =
                        new Tuple<IOperationConverter, OperationDefinition>(Converters[C],
                            _FindDefinitionByCode(Definitions, C)));
        }

        public IEnumerable<int> SupportedCodes()
        {
            return m_Operations.Keys;
        }

        public IOperationConverter GetConverterForCode(int Code)
        {
            return m_Operations[Code].Item1;
        }

        public OperationDefinition GetDefinitionForCode(int Code)
        {
            return m_Operations[Code].Item2;
        }

        public bool HasConverterForCode(int Code)
        {
            return m_Operations.ContainsKey(Code);
        }

        public bool HasDefinitionForCode(int Code)
        {
            return m_Operations.ContainsKey(Code);
        }

        private List<int> _ExtractCodes(IEnumerable<OperationDefinition> Definitions)
        {
            return Definitions.Select(D => (int)D.OpCode).ToList();
        }

        private OperationDefinition _FindDefinitionByCode(List<OperationDefinition> Definitions, int Code)
        {
            return Definitions.Find(D => D.OpCode == Code);
        }
    }
}
