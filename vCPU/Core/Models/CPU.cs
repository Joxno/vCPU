using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class CPU : ICPU
    {
        private int m_Ticks = 0;
        public int Ticks => m_Ticks;

        public void Tick()
        {
            m_Ticks++;
        }
    }
}
