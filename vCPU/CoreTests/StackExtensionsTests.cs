using System.Collections.Generic;
using Core.Utility.Extensions;
using FluentAssertions;
using NUnit.Framework;

namespace CoreTests
{
    [TestFixture]
    public class StackExtensionsTests
    {
        [Test]
        public void CopyStackWithPreserveOrder()
        {
            var t_Stack = new List<int>
            {
                1,2,3,4
            }.ToStack();

            var t_StackCopy = t_Stack.ToStack();

            _VerifyStackOrder(t_Stack);
            _VerifyStackOrder(t_StackCopy);

            t_Stack.Should()
                .NotBeSameAs(t_StackCopy);

        }

        private void _VerifyStackOrder(Stack<int> Stack)
        {
            for (int i = 1; i <= 4; i++)
                Stack.Pop().Should().Be(i);
        }
    }
}