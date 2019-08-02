using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class MemoryBankService : IMemoryBankService
    {
        private List<IMemoryBank> m_Banks;

        public MemoryBankService(List<IMemoryBank> Banks)
        {
            m_Banks = Banks;
        }

        public IMemoryBank ResolveAddress(MemoryBankAddress Address)
        {
            return _RetrieveBankByAddress(Address);
        }

        public MemoryBankAddress Attach(IMemoryBank Bank)
        {
            m_Banks.Add(Bank);
            return _ResolveIndexToAddress(m_Banks.Count - 1);
        }

        public bool HasBankAtAddress(MemoryBankAddress Address)
        {
            return _IsAddressValid(Address);
        }

        private IMemoryBank _RetrieveBankByAddress(MemoryBankAddress Address)
        {
            return m_Banks[_ResolveAddressToIndex(Address)];
        }

        private int _ResolveAddressToIndex(MemoryBankAddress Address)
        {
            return Address.Value;
        }

        private MemoryBankAddress _ResolveIndexToAddress(int Index)
        {
            return new MemoryBankAddress(Index);
        }

        private bool _IsAddressValid(MemoryBankAddress Address)
        {
            var t_Index = _ResolveAddressToIndex(Address);
            return t_Index >= 0 && t_Index < m_Banks.Count;
        }
    }
}
