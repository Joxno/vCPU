using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;

namespace Core.Models
{
    public class MemoryLocation
    {
        private MemoryAddress m_Address = null;
        private IMemoryBank m_Bank = null;

        public MemoryLocation(MemoryAddress Address, IMemoryBank Bank)
        {
            m_Address = Address;
            m_Bank = Bank;
        }

        public T Load<T>() where T : struct
        {
            return m_Bank.Load<T>(m_Address);
        }

        public void Store<T>(T Value) where T : struct
        {
            m_Bank.Store(Value, m_Address);
        }

        public override bool Equals(object Obj)
        {
            MemoryLocation t_Location = Obj as MemoryLocation;
            if (ReferenceEquals(t_Location, null))
                return false;

            return 
                m_Address == t_Location.m_Address &&
                m_Bank == t_Location.m_Bank;
        }

        public bool Equals(MemoryLocation Location)
        {
            if (ReferenceEquals(Location, null))
                return false;

            if (ReferenceEquals(this, Location))
                return true;

            return
                m_Address == Location.m_Address &&
                m_Bank == Location.m_Bank;
        }

        public override int GetHashCode()
        {
            return m_Address.GetHashCode() + m_Bank.GetHashCode();
        }

        public static bool operator ==(MemoryLocation A, MemoryLocation B)
        {
            if (ReferenceEquals(A, null))
                return false;

            if (ReferenceEquals(B, null))
                return false;

            if (ReferenceEquals(A, B))
                return true;

            return
                A.m_Address == B.m_Address &&
                A.m_Bank == B.m_Bank;
        }

        public static bool operator !=(MemoryLocation A, MemoryLocation B)
        {
            return !(A == B);
        }


    }
}
