using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Machine.Interfaces
{
    public interface IMachine
    {
        bool IsRunning { get; }
        bool IsSuspended { get; }
        void Start();
        void Stop();
        void Suspend();
        void Resume();
        byte InspectMemory(MemoryLocationAddress AddressLocation);
    }
}
