using System;

namespace Core.Models
{
    public class MemoryBankAddress : IComparable<MemoryBankAddress>
    {
        public int Value { get; }

        public MemoryBankAddress(int Address)
        {
            Value = Address;
        }

        public override bool Equals(object Obj)
        {
            MemoryBankAddress Adr = Obj as MemoryBankAddress;
            if (Adr == null)
                return false;

            return Value == Adr.Value;
        }

        public bool Equals(MemoryBankAddress A)
        {
            if (A == null)
                return false;

            return Value == A.Value;
        }

        public override int GetHashCode()
        {
            return Value;
        }

        public static bool operator ==(MemoryBankAddress A, MemoryBankAddress B)
        {
            if (ReferenceEquals(A, null))
                return ReferenceEquals(B, null);

            if (ReferenceEquals(A, B))
                return true;

            return A.Equals(B);
        }

        public static bool operator !=(MemoryBankAddress A, MemoryBankAddress B)
        {
            return !(A == B);
        }

        public int CompareTo(MemoryBankAddress Other)
        {
            if (this == Other)
                return 0;

            if (this.Value > Other.Value)
                return 1;

            return -1;
        }
    }
}
