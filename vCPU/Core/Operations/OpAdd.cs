using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Models;

namespace Core.Operations
{
    public class OpAdd : IOperation
    {
        private MemoryLocation m_First = null;
        private MemoryLocation m_Second = null;
        private MemoryLocation m_Destination = null;

        public OpAdd(MemoryLocation FirstLocation, MemoryLocation SecondLocation, MemoryLocation DestinationLocation)
        {
            m_First = FirstLocation;
            m_Second = SecondLocation;
            m_Destination = DestinationLocation;
        }

        public void Execute()
        {
            var t_Value = _LoadValueFromLocation<int>(m_First) +
                          _LoadValueFromLocation<int>(m_Second);

            _StoreValue(t_Value, m_Destination);
        }

        private T _LoadValueFromLocation<T>(MemoryLocation Location) where T : struct
        {
            return Location.Bank.Load<T>(Location.Address);
        }

        private void _StoreValue<T>(T Value, MemoryLocation Destination) where T : struct
        {
            Destination.Bank.Store(Value, Destination.Address);
        }
    }
}
