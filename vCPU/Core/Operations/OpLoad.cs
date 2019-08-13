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
        private MemoryLocation m_From = null;
        private MemoryLocation m_To = null;

        public OpLoad(
            MemoryLocation From,
            MemoryLocation To)
        {
            m_From = From;
            m_To = To;
        }
        public void Execute()
        {
            m_To.Store(m_From.Load<T>());
        }
    }
}
