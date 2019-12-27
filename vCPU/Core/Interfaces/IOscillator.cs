using System;

namespace Core.Interfaces
{
    public interface IOscillator
    {
        void Add(ITickable Tickable);
        void Remove(ITickable Tickable);
        void SetFrequency(TimeSpan Time);
        void Start();
        void Stop();
        void Oscillate();
    }
}
