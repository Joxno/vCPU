using System.Runtime.InteropServices;

namespace Core.DTO
{
    public class OperationDTO
    {
        public byte OpCode { get; private set; } = 0;
        public byte[] Data { get; private set; } = { };

        public int Size => Marshal.SizeOf(OpCode) + 
                           ((Data == null || Data.Length == 0) ? 
                           0 : 
                           Data.Length);

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
