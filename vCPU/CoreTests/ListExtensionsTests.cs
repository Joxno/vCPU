using System.Collections.Generic;
using Core.Utility.Extensions;
using FluentAssertions;
using NUnit.Framework;

namespace CoreTests
{
    [TestFixture]
    public class ListExtensionsTests
    {
        [Test]
        public void ToStack()
        {
            var t_List = new List<int>
            {
                1, 2, 3, 4
            };

            var t_Stack = t_List.ToStack();
            _VerifyStackOrder(t_Stack);
        }

        private void _VerifyStackOrder(Stack<int> Stack)
        {
            for (int i = 1; i <= 4; i++)
                Stack.Pop().Should().Be(i);
        }
    }
}