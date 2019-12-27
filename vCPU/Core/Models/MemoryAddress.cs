namespace Core.Models
{
    public class MemoryAddress
    {
        public int Value { get; } = 0;
        public MemoryAddress(int Address)
        {
            Value = Address;
        }

        public override bool Equals(object Obj)
        {
            MemoryAddress t_Address = Obj as MemoryAddress;
            if (ReferenceEquals(t_Address, null))
                return false;

            return Value == t_Address.Value;
        }

        public bool Equals(MemoryAddress A)
        {
            if (ReferenceEquals(A, null))
                return false;

            if (ReferenceEquals(this, A))
                return true;

            return Value == A.Value;
        }

        public override int GetHashCode()
        {
            return Value;
        }

        public static bool operator ==(MemoryAddress A, MemoryAddress B)
        {
            if (ReferenceEquals(A, null))
                return ReferenceEquals(B, null);

            if (ReferenceEquals(A, B))
                return true;

            return A.Equals(B);
        }

        public static bool operator !=(MemoryAddress A, MemoryAddress B)
        {
            return !(A == B);
        }

        public static MemoryAddress operator +(MemoryAddress A, MemoryAddress B)
        {
            return new MemoryAddress(A.Value + B.Value);
        }
    }
}
