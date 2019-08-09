using System;
using NUnit.Framework;
using Core.Utility.Extensions;
using FluentAssertions;

namespace CoreTests
{
    [TestFixture]
    public class TimeSpanExtensionsTests
    {
        [Test]
        public void FromMicroseconds()
        {
            var t_TS = new TimeSpan().FromMicroseconds(1000);

            t_TS.Milliseconds.Should().Be(1);
        }
    }
}
