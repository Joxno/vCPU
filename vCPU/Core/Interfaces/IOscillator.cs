using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
