using System;
using Core.Utility;
using FluentAssertions;
using NUnit.Framework;

namespace CoreTests
{
    [TestFixture]
    public class EitherTests
    {

        [Test]
        public void ConstructFromError()
        {
            var t_Either = new Either<object>(new Exception("Foobar"));

            t_Either
                .HasError()
                .Should()
                .BeTrue();
        }

        [Test]
        public void ConstructImplicitFromError()
        {
            Either<object> t_Either = new Exception("Foobar");

            t_Either
                .HasError()
                .Should()
                .BeTrue();
        }

        [Test]
        public void ConstructImplicitFromValue()
        {
            Either<int> t_Either = 5;

            t_Either
                .HasError()
                .Should()
                .BeFalse();
        }

        [Test]
        public void ConstructFromValue()
        {
            var t_Either = new Either<int>(5);

            t_Either
                .HasError()
                .Should()
                .BeFalse();

            t_Either
                .Value
                .Should()
                .Be(5);
        }

        [Test]
        public void AccessValueWithError()
        {
            var t_Either = new Either<object>(new Exception());

            Action t_AccessFunc = () =>
            {
                var t_Value = t_Either.Value;
            };

            t_AccessFunc
                .Should()
                .Throw<Exception>();
        }

        [Test]
        public void AccessErrorWithValue()
        {
            var t_Either = new Either<int>(5);

            Action t_AccessFunc = () =>
            {
                var t_Value = t_Either.Error;
            };

            t_AccessFunc
                .Should()
                .Throw<Exception>();
        }

    }
}