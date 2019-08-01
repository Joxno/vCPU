using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICPU
    {
        void Tick();
        int Ticks { get; }
        void ExecuteOperation(IOperation Operation);
        int ExecutedOperations { get; }
        void QueueOperation(IOperation Operation);
        int QueuedOperations { get; }
        void Suspend();
    }
}
