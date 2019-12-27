using System;
using System.Runtime.InteropServices;

namespace Core.Utility
{
    public class ValueTypeConverter
    {
        /* https://stackoverflow.com/a/3278956 */
        public Byte[] ConvertValueTypeToBytes<T>(T Value) where T : struct
        {
            int t_Size = Marshal.SizeOf(Value);
            Byte[] t_Buffer = new byte[t_Size];

            IntPtr t_Pointer = Marshal.AllocHGlobal(t_Size);
            Marshal.StructureToPtr(Value, t_Pointer, false);
            Marshal.Copy(t_Pointer, t_Buffer, 0, t_Size);
            Marshal.FreeHGlobal(t_Pointer);

            return t_Buffer;
        }

        public T ConvertBytesToValueType<T>(Byte[] Bytes) where T : struct
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
