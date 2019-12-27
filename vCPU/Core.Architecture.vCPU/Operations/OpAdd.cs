using Core.Interfaces;
using Core.Models;

namespace Core.Architecture.vCPU.Operations
{
    public class OpAdd : IOperation
    {
        private readonly MemoryLocation m_First = null;
        private readonly MemoryLocation m_Second = null;
        private readonly MemoryLocation m_Destination = null;

        public OpAdd(MemoryLocation FirstLocation, MemoryLocation SecondLocation, MemoryLocation DestinationLocation)
        {
            m_First = FirstLocation;
            m_Second = SecondLocation;
            m_Destination = DestinationLocation;
        }

        public void Execute()
        {
            var t_Value = m_First.Load<int>() +
                          m_Second.Load<int>();

            m_Destination.Store(t_Value);
        }
    }
}
