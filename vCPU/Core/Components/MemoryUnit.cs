using Core.Interfaces;
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
            m_Storage = _GetBytesFromType(Value).ToList();
        }

        public T Retrieve<T>()
        {
            return _GetValueFromBytes<T>(m_Storage.ToArray());
        }

        /* https://stackoverflow.com/a/3278956 */
        private Byte[] _GetBytesFromType<T>(T Value) where T : struct
        {
            int t_Size = Marshal.SizeOf(Value);
            Byte[] t_Buffer = new byte[t_Size];

            IntPtr t_Pointer = Marshal.AllocHGlobal(t_Size);
            Marshal.StructureToPtr(Value, t_Pointer, false);
            Marshal.Copy(t_Pointer, t_Buffer, 0, t_Size);
            Marshal.FreeHGlobal(t_Pointer);

            return t_Buffer;
        }

        private T _GetValueFromBytes<T>(Byte[] Bytes)
        {
            int t_Size = Marshal.SizeOf(typeof(T));
            IntPtr t_Pointer = Marshal.AllocHGlobal(t_Size);

            Marshal.Copy(Bytes, 0, t_Pointer, t_Size);

            T t_Value = (T)Marshal.PtrToStructure(t_Pointer, typeof(T));
            Marshal.FreeHGlobal(t_Pointer);

            return t_Value;
        }
    }
}
