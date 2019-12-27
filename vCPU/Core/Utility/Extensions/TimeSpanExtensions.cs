using System;

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
