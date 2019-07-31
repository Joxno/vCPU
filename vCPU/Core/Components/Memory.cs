using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Components
{
    public class Memory : IMemory
    {
        private int m_Size = 0;

        public int Size => m_Size;

        public Memory()
        {

        }

        public Memory(int Size)
        {
            m_Size = Size;
        }
    }
}
