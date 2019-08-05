using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class OperationDTO
    {
        public byte OpCode { get; private set; } = 0;
        public byte[] Data { get; private set; } = { };

        public int Size
        {
            get
            {
                return Marshal.SizeOf(OpCode) + 
                ((Data == null || Data.Length == 0) ? 0 : Data.Length);
            }
        }

        public OperationDTO(byte Code, byte[] OperationData)
        {
            OpCode = Code;
            Data = OperationData;
        }

        public OperationDTO(byte Code)
        {
            OpCode = Code;
        }
    }
}
