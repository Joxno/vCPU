﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Utility.Extensions;

namespace Core.Components
{
    public class Clock : IOscillator
    {
        private List<ITickable> m_Tickables = new List<ITickable>();
        private TimeSpan m_Frequency = new TimeSpan(0);
        private Stopwatch m_Stopwatch = new Stopwatch();

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

        public void Start()
        {
            m_Stopwatch.Start();
        }

        public void Stop()
        {
            m_Stopwatch.Reset();
        }

        public void Oscillate()
        {
            if(_CanOscillate())
                _TickTickables();
        }

        private void _TickTickables()
        {
            foreach (var t_Tickable in m_Tickables)
                t_Tickable.Tick();
        }

        private bool _CanOscillate()
        {
            return _CheckElapsedTimeAndReset();
        }

        private bool _CheckElapsedTimeAndReset()
        {
            if (m_Stopwatch.ElapsedTicks > m_Frequency.Ticks)
            {
                m_Stopwatch.Restart();
                return true;
            }

            return false;
        }
    }
}
