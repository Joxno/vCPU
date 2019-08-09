using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;

namespace Core.Models
{
    public class MemoryLocation
    {
        private MemoryAddress m_Address = null;
        private IMemoryBank m_Bank = null;

        public MemoryLocation(MemoryAddress Address, IMemoryBank Bank)
        {
            m_Address = Address;
            m_Bank = Bank;
        }

        public T Load<T>() where T : struct
        {
            return m_Bank.Load<T>(m_Address);
        }

        public void Store<T>(T Value) where T : struct
        {
            m_Bank.Store(Value, m_Address);
        }


    }
}
