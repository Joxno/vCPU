using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Operations
{
    public class OpLoad<T> : IOperation where T : struct
    {
        private T m_Value = default;
        private MemoryAddress m_Address = null;
        private IMemoryBank m_Bank = null;

        public OpLoad(T Value, MemoryAddress Address, IMemoryBank Bank)
        {
            m_Value = Value;
            m_Address = Address;
            m_Bank = Bank;
        }

        public void Execute()
        {
            m_Bank.Store<T>(m_Value, m_Address);
        }
    }
}
