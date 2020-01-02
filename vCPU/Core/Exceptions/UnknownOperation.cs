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

        public UnknownOperation(string Message) : base(Message)
        {
        }

        public UnknownOperation(string Message, Exception InnerException) : base(Message, InnerException)
        {
        }

        public UnknownOperation(SerializationInfo Info, StreamingContext Context) : base(Info, Context)
        {
        }
    }
}
