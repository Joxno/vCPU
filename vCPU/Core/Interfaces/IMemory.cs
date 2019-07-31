using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IMemory
    {
        int Size { get; }
        void Store<T>(T Value) where T : struct;
        T Retrieve<T>();
    }
}
