using Core.Interfaces;
using Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Components
{
    public class MemoryUnit : IMemoryUnit
    {
        private ValueTypeConverter m_Converter = new ValueTypeConverter();
        private List<Byte> m_Storage = new List<byte>();
        private int m_Size = 0;

        public int Size => m_Size;

        public MemoryUnit()
        {
        }

        public MemoryUnit(int Size)
        {
            _InitializeStorage(Size);
        }

        public void Store<T>(T Value) where T : struct
        {
            m_Storage = m_Converter.ConvertValueTypeToBytes(Value).ToList();
        }

        public T Load<T>() where T : struct
        {
            return m_Converter.ConvertBytesToValueType<T>(m_Storage.ToArray());
        }

        private void _InitializeStorage(int Size)
        {
            m_Size = Size;
            for(int i = 0; i < m_Size; i++)
                m_Storage.Add(0);
        }
    }
}
