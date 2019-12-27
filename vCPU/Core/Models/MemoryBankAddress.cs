namespace Core.Models
{
    public class MemoryBankAddress
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
    }
}
