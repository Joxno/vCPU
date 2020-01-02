using System;
using Core.Utility;
using FluentAssertions;
using NUnit.Framework;

namespace CoreTests
{
    [TestFixture]
    public class TryTests
    {
        [Test]
        public void TestCallThrow()
        {
            var t_Result = Try.Call(() =>
            {
                throw new Exception("Test");
            });

            t_Result.HasError().Should().BeTrue();
        }

        [Test]
        public void TestCall()
        {
            var t_Result = Try.Call(() => { });

            t_Result.HasError().Should().BeFalse();
        }

        [Test]
        public void TestCallWithReturn()
        {
            var t_Result = Try.Call(() => 1);

            t_Result.HasError().Should().BeFalse();
        }

        [Test]
        public void TestCallWithParameter()
        {
            var t_Result = Try.Call((i) => i * 2, 2);

            t_Result.HasError().Should().BeFalse();
            t_Result.Value.Should().Be(4);
        }

        [Test]
        public void TestThrowCallWithParameter()
        {
            var t_Result = Try.Call<int, int>((i) => throw new Exception(), 2);

            t_Result.HasError().Should().BeTrue();
        }
    }
}