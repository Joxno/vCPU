﻿using System;
using Core.Interfaces;

namespace Core.Models
{
    public class MemoryLocation : IComparable<MemoryLocation>
    {
        private readonly MemoryAddress m_Address = null;
        private readonly IMemoryBank m_Bank = null;

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
                return ReferenceEquals(B, null);

            if (ReferenceEquals(A, B))
                return true;

            return A.Equals(B);
        }

        public static bool operator !=(MemoryLocation A, MemoryLocation B)
        {
            return !(A == B);
        }


        public int CompareTo(MemoryLocation Other)
        {
            if (this == Other)
                return 0;

            if (this.m_Bank == Other.m_Bank &&
                this.m_Address.Value > Other.m_Address.Value)
                return 1;

            return -1;
        }
    }
}
