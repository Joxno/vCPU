using System;
using Core.DTO;

namespace Core.Exceptions
{
    public class UnknownOperation : Exception
    {
        public UnknownOperation(OperationDTO DTO) 
            : base($"Unknown Operation with Code: {DTO.OpCode}")
        {

        }

        public UnknownOperation(int OpCode)
            : base($"Unknown Operation with Code: {OpCode}")
        {

        }
    }
}
