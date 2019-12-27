using Core.Models;
using System;

namespace Core.Exceptions
{
    public class AddressOutOfRange : Exception
    {
        public AddressOutOfRange(MemoryAddress Address, int MinRange, int MaxRange) 
            : base($"Address: {Address.Value} is not within range {MinRange}->{MaxRange}")
        {
        }
    }
}
