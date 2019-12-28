using System;

namespace Core.Models
{
    public class MemoryLocationAddress : IComparable<MemoryLocationAddress>
    {
        public MemoryAddress Address { get; }
        public MemoryBankAddress BankAddress { get; }
        
        public MemoryLocationAddress(MemoryAddress Address, MemoryBankAddress BankAddress)
        {
            this.Address = Address;
            this.BankAddress = BankAddress;
        }

        public override bool Equals(object Obj)
        {
            MemoryLocationAddress t_Location = Obj as MemoryLocationAddress;
            if (ReferenceEquals(t_Location, null))
                return false;

            return
                Address == t_Location.Address &&
                BankAddress == t_Location.BankAddress;
        }

        public bool Equals(MemoryLocationAddress Location)
        {
            if (ReferenceEquals(Location, null))
                return false;

            if (ReferenceEquals(this, Location))
                return true;

            return
                Address.Equals(Location.Address) &&
                BankAddress.Equals(Location.BankAddress);
        }

        public override int GetHashCode()
        {
            return Address.GetHashCode() + BankAddress.GetHashCode();
        }

        public static bool operator ==(MemoryLocationAddress A, MemoryLocationAddress B)
        {
            if (ReferenceEquals(A, null))
                return ReferenceEquals(B, null);

            if (ReferenceEquals(A, B))
                return true;

            return A.Equals(B);
        }

        public static bool operator !=(MemoryLocationAddress A, MemoryLocationAddress B)
        {
            return !(A == B);
        }

        public int CompareTo(MemoryLocationAddress Other)
        {
            if (this == Other)
                return 0;

            if (this.BankAddress == Other.BankAddress &&
                this.Address.Value > Other.Address.Value)
                return 1;

            return -1;
        }
    }
}
