using Core.Interfaces;
using Core.Models;

namespace Core.Architecture.vCPU.Operations
{
    public class OpLoad<T> : IOperation where T : struct
    {
        private readonly MemoryLocation m_From = null;
        private readonly MemoryLocation m_To = null;

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
