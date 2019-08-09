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
        public MemoryAddress Address { get; private set; }
        public IMemoryBank Bank { get; private set;  }

        public MemoryLocation(MemoryAddress Address, IMemoryBank Bank)
        {
            this.Address = Address;
            this.Bank = Bank;
        }

        public T Load<T>() where T : struct
        {
            return Bank.Load<T>(Address);
        }

        public void Store<T>(T Value) where T : struct
        {
            Bank.Store(Value, Address);
        }


    }
}
