using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class MemoryAddress
    {
        public int Value { get; private set; } = 0;
        public MemoryAddress(int Address)
        {
            Value = Address;
        }
    }
}
