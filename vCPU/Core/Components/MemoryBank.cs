﻿using Core.Exceptions;
using Core.Interfaces;
using Core.Models;
using Core.Utility;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Core.Components
{
    public class MemoryBank : IMemoryBank
    {
        private readonly ValueTypeConverter m_Converter = new ValueTypeConverter();
        private readonly List<MemoryUnit> m_Storage = new List<MemoryUnit>();
        private readonly int m_Size = 0;

        public int Size => m_Size;
        public MemoryBank(int Size)
        {
            m_Size = Size;
            _InitializeStorage(m_Size);
        }

        public void Store<T>(T Value, MemoryAddress Address) where T : struct
        {
            _HandleOutOfRange(Address);
            var t_Index = _TranslateAddressToIndex(Address);
            var t_BytesToStore = m_Converter.ConvertValueTypeToBytes(Value);
            _MapAndStoreBytes(t_BytesToStore, t_Index);
        }

        public T Load<T>(MemoryAddress Address) where T : struct
        {
            _HandleOutOfRange(Address);
            var t_Index = _TranslateAddressToIndex(Address);
            var t_RawData = _MapAndLoadBytes(t_Index, Marshal.SizeOf<T>());
            return m_Converter.ConvertBytesToValueType<T>(t_RawData);
        }

        public bool IsValid(MemoryAddress Address)
        {
            return _IsWithinRange(_TranslateAddressToIndex(Address));
        }

        private void _InitializeStorage(int Size)
        {
            for (int i = 0; i < Size; i++)
                m_Storage.Add(new MemoryUnit(1));
        }

        private int _TranslateAddressToIndex(MemoryAddress Address)
        {
            return Address.Value;
        }

        private void _MapAndStoreBytes(byte[] Data, int Index)
        {
            var t_EndIndex = Index + Data.Length;
            for (int i = Index, j = 0; i < t_EndIndex; i++, j++)
                m_Storage[i].Store(Data[j]);
        }

        private byte[] _MapAndLoadBytes(int Index, int Size)
        {
            var t_Data = new List<byte>();
            var t_EndIndex = Index + Size;
            for (int i = Index; i < t_EndIndex; i++)
                t_Data.Add(m_Storage[i].Load<byte>());
            return t_Data.ToArray();
        }

        private bool _IsWithinRange(int Index)
        {
            return Index >= 0 && Index < m_Size;
        }

        private void _HandleOutOfRange(MemoryAddress Address)
        {
            if (!IsValid(Address))
                throw new AddressOutOfRange(Address, 0, m_Size - 1);
        }
    }
}
