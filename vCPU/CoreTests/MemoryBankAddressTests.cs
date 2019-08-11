using System;
using Core.Models;
using FluentAssertions;
using NUnit.Framework;

namespace CoreTests
{
    [TestFixture]
    public class MemoryBankAddressTests
    {
        [Test]
        public void Equals()
        {
            var t_FirstAddress = new MemoryBankAddress(0);
            var t_SecondAddress = new MemoryBankAddress(0);

            t_FirstAddress.Equals(t_SecondAddress)
                .Should().BeTrue();
        }

        [Test]
        public void EqualsNull()
        {
            var t_Address = new MemoryBankAddress(0);

            t_Address.Equals(null)
                .Should().BeFalse();
        }

        [Test]
        public void EqualsObjectNull()
        {
            var t_Address = new MemoryBankAddress(0);
            t_Address.Equals((object) null)
                .Should().BeFalse();
        }

        [Test]
        public void EqualsObjectOfCorrectType()
        {
            var t_FirstAddress = new MemoryBankAddress(0);
            object t_SecondAddress = new MemoryBankAddress(0);

            t_FirstAddress.Equals(t_SecondAddress)
                .Should().BeTrue();
        }

        [Test]
        public void EqualsObjectIncorrectType()
        {
            var t_Address = new MemoryBankAddress(0);

            t_Address.Equals(new int())
                .Should().BeFalse();
        }

        [Test]
        public void HashCode()
        {
            var t_FirstAddress = new MemoryBankAddress(0);
            var t_SecondAddress = new MemoryBankAddress(0);

            (t_FirstAddress.GetHashCode() == t_SecondAddress.GetHashCode())
                .Should().BeTrue();
        }
    }
}
