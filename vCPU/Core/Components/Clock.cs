using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;

namespace Core.Components
{
    public class Clock : IOscillator, ITickable
    {
        private List<ITickable> m_Tickables = new List<ITickable>();
        private TimeSpan m_Frequency = new TimeSpan(0);

        public int Ticks { get; private set; } = 0;
        public bool IsSuspended { get; private set; } = false;
        public void Add(ITickable Tickable)
        {
            m_Tickables.Add(Tickable);
        }

        public void Remove(ITickable Tickable)
        {
            m_Tickables.Remove(Tickable);
        }

        public void SetFrequency(TimeSpan Time)
        {
            m_Frequency = Time;
        }

        public void Tick()
        {
            if (!IsSuspended)
                _TickTickables();
            Ticks++;
        }

        public void ForceTick()
        {
            _TickTickables();
            Ticks++;
        }

        public void Suspend()
        {
            IsSuspended = true;
        }

        public void Resume()
        {
            IsSuspended = false;
        }

        private void _TickTickables()
        {
            foreach (var t_Tickable in m_Tickables)
                t_Tickable.Tick();
        }
    }
}
