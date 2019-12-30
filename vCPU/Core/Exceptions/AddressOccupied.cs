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

        protected AddressOccupied(SerializationInfo Info, StreamingContext Context) : base(Info, Context)
        {
        }
    }
}
