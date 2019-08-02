using Core.Exceptions;
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
        private Dictionary<MemoryBankAddress, IMemoryBank> m_Banks = new Dictionary<MemoryBankAddress, IMemoryBank>();

        public MemoryBankService(List<IMemoryBank> Banks)
        {
            _InitializeBanksFromList(Banks);
        }

        public IMemoryBank ResolveAddress(MemoryBankAddress Address)
        {
            return m_Banks[Address];
        }

        public MemoryBankAddress Attach(IMemoryBank Bank)
        {
            var t_Address = _GenerateNewAddress();
            m_Banks[t_Address] = Bank;
            return t_Address;
        }

        public void AttachAtAddress(IMemoryBank Bank, MemoryBankAddress Address)
        {
            if (!HasBankAtAddress(Address))
                m_Banks.Add(Address, Bank);
            else
                throw new AddressOccupied();
        }

        public void Detach(MemoryBankAddress Address)
        {
            m_Banks.Remove(Address);
        }

        public bool HasBankAtAddress(MemoryBankAddress Address)
        {
            return m_Banks.ContainsKey(Address);
        }

        private void _InitializeBanksFromList(List<IMemoryBank> Banks)
        {
            var t_RawAddress = 0;
            foreach (var t_Bank in Banks)
                m_Banks[new MemoryBankAddress(t_RawAddress++)] = t_Bank;
        }

        private MemoryBankAddress _RetrieveLargestAddress()
        {
            return m_Banks.Keys.OrderByDescending(A => A.Value).First();
        }

        private MemoryBankAddress _GenerateNewAddress()
        {
            return new MemoryBankAddress(_RetrieveLargestAddress().Value + 1);
        }
    }
}
