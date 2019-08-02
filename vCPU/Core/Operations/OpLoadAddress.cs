using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Operations
{
    public class OpLoadAddress<T> : IOperation where T : struct
    {
        private MemoryAddress m_FromAddress = null;
        private IMemoryBank m_FromBank = null;
        private MemoryAddress m_ToAddress = null;
        private IMemoryBank m_ToBank = null;

        public OpLoadAddress(
            MemoryAddress FromAddress, 
            IMemoryBank FromBank, 
            MemoryAddress ToAddress, 
            IMemoryBank ToBank)
        {
            m_FromAddress = FromAddress;
            m_FromBank = FromBank;
            m_ToAddress = ToAddress;
            m_ToBank = ToBank;
        }
        public void Execute()
        {
            m_ToBank.Store<T>(
                m_FromBank.Load<T>(m_FromAddress), 
                m_ToAddress
            );
        }
    }
}
