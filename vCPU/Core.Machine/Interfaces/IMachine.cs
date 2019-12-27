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
