﻿using System.Runtime.InteropServices;

namespace Core.DTO
{
    public class OperationDTO
    {
        public byte OpCode { get; } = 0;
        public byte[] Data { get; } = { };

        public int TotalSize => Marshal.SizeOf(OpCode) + 
                           ((Data == null || Data.Length == 0) ? 
                           0 : 
                           Data.Length);
        public int DataSize => (Data == null || Data.Length == 0) ? 0 : Data.Length;

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
