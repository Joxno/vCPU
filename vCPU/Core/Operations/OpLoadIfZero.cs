﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Models;

namespace Core.Operations
{
    public class OpLoadIfZero<T> : IOperation where T : struct
    {
        private T m_Value = default;
        private MemoryLocation m_Destination = null;
        private MemoryLocation m_CompareLocation = null;

        public OpLoadIfZero(T Value, MemoryLocation Destination, MemoryLocation CompareLocation)
        {
            m_Value = Value;
            m_Destination = Destination;
            m_CompareLocation = CompareLocation;
        }

        public void Execute()
        {
            if(m_CompareLocation.Load<byte>() == 0)
                m_Destination.Store(m_Value);
        }
    }
}
