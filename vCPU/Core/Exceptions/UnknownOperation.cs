using System;
using System.Runtime.Serialization;
using Core.DTO;

namespace Core.Exceptions
{
    [Serializable]
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

        protected UnknownOperation(SerializationInfo Info, StreamingContext Context) : base(Info, Context)
        {
        }
    }
}
