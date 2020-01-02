using System;
using System.Runtime.Serialization;

namespace Core.Exceptions
{
    [Serializable]
    public class AddressOccupied : Exception
    {
        public AddressOccupied()
        {
        }

        public AddressOccupied(string Message) : base(Message)
        {
        }

        public AddressOccupied(string Message, Exception InnerException) : base(Message, InnerException)
        {
        }

        public AddressOccupied(SerializationInfo Info, StreamingContext Context) : base(Info, Context)
        {
        }
    }
}
