﻿using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Operations
{
    public class OpLoadAddress<T> : IOperation where T : struct
    {
        private MemoryLocation m_From = null;
        private MemoryLocation m_To = null;

        public OpLoadAddress(
            MemoryLocation From,
            MemoryLocation To)
        {
            m_From = From;
            m_To = To;
        }
        public void Execute()
        {
            m_To.Bank.Store<T>(
                m_From.Bank.Load<T>(m_From.Address), 
                m_To.Address
            );
        }
    }
}
