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
        private MemoryLocation m_Destination = null;

        public OpLoad(T Value, MemoryLocation Destination)
        {
            m_Value = Value;
            m_Destination = Destination;
        }

        public void Execute()
        {
            m_Destination.Bank.Store<T>(m_Value, m_Destination.Address);
        }
    }
}
