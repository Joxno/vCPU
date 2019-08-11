using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using Core.Operations;

namespace Core.Components
{
    public class OperationFetcher : IOperationFetcher
    {
        private IOperationReader m_Reader = null;
        private IMemoryBankService m_BankService = null;
        private IMemoryBank m_CurrentBank = null;
        public MemoryAddress CurrentAddress { get; private set; } = new MemoryAddress(0);

        public OperationFetcher(IOperationReader Reader, IMemoryBankService BankService)
        {
            m_Reader = Reader;
            m_BankService = BankService;
        }

        public IOperation FetchOperation()
        {
            if (m_CurrentBank != null && m_CurrentBank.IsValid(CurrentAddress))
                return _ReadOperation();

            return new NoOp();
        }

        public void SetFetchAddress(MemoryAddress MemoryAddress, MemoryBankAddress BankAddress)
        {
            CurrentAddress = MemoryAddress;
            m_CurrentBank = m_BankService.ResolveAddress(BankAddress);
        }

        private IOperation _ReadOperation()
        {
            var t_Operation = m_Reader.ReadOperationFromMemory(CurrentAddress, m_CurrentBank);
            _IncrementAddress();
            return t_Operation;
        }

        private void _IncrementAddress()
        {
            CurrentAddress = m_Reader.ReadNextOperationAddress(CurrentAddress, m_CurrentBank);
        }
    }
}
