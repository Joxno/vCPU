using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;

namespace CoreTests.Mocks
{
    internal class MockTickable : ITickable
    {
        public int Ticks { get; set; } = 0;
        public bool IsSuspended { get; set; } = false;

        public void Tick()
        {
            if (!IsSuspended)
                Ticks++;
        }

        public void ForceTick()
        {
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
    }
}
