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

        public override bool Equals(object Obj)
        {
            if (Obj == null)
                return false;

            MemoryAddress Adr = Obj as MemoryAddress;
            if (Adr == null)
                return false;

            return Value == Adr.Value;
        }

        public bool Equals(MemoryAddress A)
        {
            if (A == null)
                return false;

            return Value == A.Value;
        }

        public override int GetHashCode()
        {
            return Value;
        }
    }
}
