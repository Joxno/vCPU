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

        private int m_ExecutedOperations = 0;
        public int ExecutedOperations => m_ExecutedOperations;

        private int m_QueuedOperations = 0;
        public int QueuedOperations => m_QueuedOperations;

        public void ExecuteOP(IOperation Operation)
        {
            m_ExecutedOperations++;
        }

        public void Tick()
        {
            m_Ticks++;
        }

        public void QueueOperation(IOperation Operation)
        {
            m_QueuedOperations++;
        }
    }
}
