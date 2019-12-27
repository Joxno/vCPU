using Core.Models;
using FluentAssertions;
using NUnit.Framework;

namespace CoreTests
{
    [TestFixture]
    public class MemoryAddressTests
    {
        [Test]
        public void Equals()
        {
            var t_FirstAddress = new MemoryAddress(0);
            var t_SecondAddress = new MemoryAddress(0);

            t_FirstAddress.Equals(t_SecondAddress)
                .Should().BeTrue();
        }

        [Test]
        public void EqualsObject()
        {
            var t_FirstAddress = new MemoryAddress(0);
            object t_SecondAddress = new MemoryAddress(0);

            t_FirstAddress.Equals(t_SecondAddress)
                .Should().BeTrue();
        }

        [Test]
        public void EqualsNull()
        {
            var t_FirstAddress = new MemoryAddress(0);

            t_FirstAddress.Equals(null)
                .Should().BeFalse();
        }

        [Test]
        public void EqualsIncorrectType()
        {
            var t_FirstAddress = new MemoryAddress(0);

            t_FirstAddress.Equals(new int())
                .Should().BeFalse();
        }

        [Test]
        public void EqualsSameInstance()
        {
            var t_FirstAddress = new MemoryAddress(0);
            t_FirstAddress.Equals(t_FirstAddress)
                .Should().BeTrue();
        }

        [Test]
        public void HashCode()
        {
            var t_FirstAddress = new MemoryAddress(0);
            var t_SecondAddress = new MemoryAddress(0);

            (t_FirstAddress.GetHashCode() == t_SecondAddress.GetHashCode())
                .Should().BeTrue();
        }

        [Test]
        public void OperatorEquals()
        {
            var t_FirstAddress = new MemoryAddress(0);
            var t_SecondAddress = new MemoryAddress(0);

            (t_FirstAddress == t_SecondAddress)
                .Should().BeTrue();
        }

        [Test]
        public void OperatorNotEquals()
        {
            var t_FirstAddress = new MemoryAddress(0);
            var t_SecondAddress = new MemoryAddress(1);

            (t_FirstAddress != t_SecondAddress)
                .Should().BeTrue();
        }

        [Test]
        public void OperatorEqualsSameInstance()
        {
            var t_Address = new MemoryAddress(0);

            (t_Address == t_Address)
                .Should().BeTrue();
        }

        [Test]
        public void OperatorEqualsNull()
        {
            var t_Address = new MemoryAddress(0);

            (t_Address == null)
                .Should().BeFalse();
        }

        [Test]
        public void OperatorBothInstancesEqualsNull()
        {
            MemoryAddress t_FirstAddress = null;
            MemoryAddress t_SecondAddress = null;

            (t_FirstAddress == t_SecondAddress)
                .Should().BeTrue();
        }

        [Test]
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
