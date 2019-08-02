using Core.Interfaces;
using Core.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Core.Components
{
    public class MemoryUnit : IMemoryUnit
    {
        private AnyConverter m_Converter = new AnyConverter();
        private List<Byte> m_Storage = new List<byte>();
        private int m_Size = 0;

        public int Size => m_Size;

        public MemoryUnit()
        {
        }

        public MemoryUnit(int Size)
        {
            m_Size = Size;
        }

        public void Store<T>(T Value) where T : struct
        {
            m_Storage = m_Converter.ConvertValueTypeToBytes(Value).ToList();
        }

        public T Load<T>() where T : struct
        {
            return m_Converter.ConvertBytesToValueType<T>(m_Storage.ToArray());
        }
    }
}
