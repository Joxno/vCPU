using Core.Models;
using System;
using System.Runtime.Serialization;

namespace Core.Exceptions
{
    [Serializable]
    public class AddressOutOfRange : Exception
    {
        public AddressOutOfRange(MemoryAddress Address, int MinRange, int MaxRange) 
            : base($"Address: {Address.Value} is not within range {MinRange}->{MaxRange}")
        {
        }

        public AddressOutOfRange(string Message) : base(Message)
        {
        }

        public AddressOutOfRange(string Message, Exception InnerException) : base(Message, InnerException)
        {
        }

        public AddressOutOfRange(SerializationInfo Info, StreamingContext Context) : base(Info, Context)
        {
        }
    }
}
