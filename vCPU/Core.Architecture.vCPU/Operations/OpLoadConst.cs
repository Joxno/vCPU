using Core.Interfaces;
using Core.Models;

namespace Core.Architecture.vCPU.Operations
{
    public class OpLoadConst<T> : IOperation where T : struct
    {
        private readonly T m_Value = default;
        private readonly MemoryLocation m_Destination = null;

        public OpLoadConst(T Value, MemoryLocation Destination)
        {
            m_Value = Value;
            m_Destination = Destination;
        }

        public void Execute()
        {
            m_Destination.Store(m_Value);
        }
    }
}
