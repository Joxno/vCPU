using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utility.Extensions
{
    public static class TimeSpanExtensions
    {
        public static TimeSpan FromMicroseconds(this TimeSpan S, int MicroSeconds)
        {
            return TimeSpan.FromTicks(MicroSeconds * 10);
        }
    }
}
