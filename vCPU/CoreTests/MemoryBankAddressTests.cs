using System;
using Core.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoreTests
{
    [TestClass]
    public class MemoryBankAddressTests
    {
        [TestMethod]
        public void Equals()
        {
            var t_FirstAddress = new MemoryBankAddress(0);
            var t_SecondAddress = new MemoryBankAddress(0);

            t_FirstAddress.Equals(t_SecondAddress)
                .Should().BeTrue();
        }

        [TestMethod]
        public void HashCode()
        {
            var t_FirstAddress = new MemoryBankAddress(0);
            var t_SecondAddress = new MemoryBankAddress(0);

            (t_FirstAddress.GetHashCode() == t_SecondAddress.GetHashCode())
                .Should().BeTrue();
        }
    }
}
