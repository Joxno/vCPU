using System;
using Core.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoreTests
{
    [TestClass]
    public class MemoryAddressTests
    {
        [TestMethod]
        public void Equals()
        {
            var t_FirstAddress = new MemoryAddress(0);
            var t_SecondAddress = new MemoryAddress(0);

            t_FirstAddress.Equals(t_SecondAddress)
                .Should().BeTrue();
        }

        [TestMethod]
        public void EqualsObject()
        {
            var t_FirstAddress = new MemoryAddress(0);
            object t_SecondAddress = new MemoryAddress(0);

            t_FirstAddress.Equals(t_SecondAddress)
                .Should().BeTrue();
        }

        [TestMethod]
        public void EqualsNull()
        {
            var t_FirstAddress = new MemoryAddress(0);

            t_FirstAddress.Equals(null)
                .Should().BeFalse();
        }

        [TestMethod]
        public void EqualsIncorrectType()
        {
            var t_FirstAddress = new MemoryAddress(0);

            t_FirstAddress.Equals(new int())
                .Should().BeFalse();
        }

        [TestMethod]
        public void HashCode()
        {
            var t_FirstAddress = new MemoryAddress(0);
            var t_SecondAddress = new MemoryAddress(0);

            (t_FirstAddress.GetHashCode() == t_SecondAddress.GetHashCode())
                .Should().BeTrue();
        }

        [TestMethod]
        public void OperatorEquals()
        {
            var t_FirstAddress = new MemoryAddress(0);
            var t_SecondAddress = new MemoryAddress(0);

            (t_FirstAddress == t_SecondAddress)
                .Should().BeTrue();
        }

        [TestMethod]
        public void OperatorNotEquals()
        {
            var t_FirstAddress = new MemoryAddress(0);
            var t_SecondAddress = new MemoryAddress(1);

            (t_FirstAddress != t_SecondAddress)
                .Should().BeTrue();
        }

        [TestMethod]
        public void AddressAddition()
        {
            var t_Address = new MemoryAddress(0) + 
                            new MemoryAddress(3);
            var t_CompareAddress = new MemoryAddress(3);

            t_Address.Value.Should().Be(3);
            t_Address.Equals(t_CompareAddress).Should().BeTrue();
        }
    }
}
