using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Models;

namespace Core.Architecture.vCPU.Operations
{
    public class OpSub : IOperation
    {
        private MemoryLocation m_First = null;
        private MemoryLocation m_Second = null;
        private MemoryLocation m_Destination = null;

        public OpSub(MemoryLocation FirstLocation, MemoryLocation SecondLocation, MemoryLocation DestinationLocation)
        {
            m_First = FirstLocation;
            m_Second = SecondLocation;
            m_Destination = DestinationLocation;
        }

        public void Execute()
        {
            var t_Value = m_First.Load<int>() -
                          m_Second.Load<int>();

            m_Destination.Store(t_Value);
        }
    }
}
