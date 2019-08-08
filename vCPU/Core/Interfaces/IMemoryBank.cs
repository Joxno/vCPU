using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IMemoryBank
    {
        int Size { get; }
        void Store<T>(T Value, MemoryAddress Address) where T : struct;
        T Load<T>(MemoryAddress Address) where T : struct;
        bool IsValid(MemoryAddress Address);
    }
}
