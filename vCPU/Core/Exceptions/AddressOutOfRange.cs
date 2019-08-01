using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
