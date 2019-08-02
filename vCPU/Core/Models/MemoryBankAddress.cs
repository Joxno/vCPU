using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class MemoryBankAddress
    {
        public int Value { get; private set; }

        public MemoryBankAddress(int Address)
        {
            Value = Address;
        }
    }
}
