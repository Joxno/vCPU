using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Components
{
    public class MemoryBank : IMemoryBank
    {
        private int m_Size;

        public MemoryBank(int Size)
        {
            m_Size = Size;
        }

        public int Size => m_Size;
    }
}
