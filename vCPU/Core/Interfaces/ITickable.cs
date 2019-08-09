using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ITickable
    {
        int Ticks { get; }
        bool IsSuspended { get; }
        void Tick();
        void ForceTick();
        void Suspend();
        void Resume();
    }
}
