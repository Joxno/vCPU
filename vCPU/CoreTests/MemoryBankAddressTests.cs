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
                .Should()
                .BeTrue();
        }

        [Test]
        public void EqualsNull()
        {
            var t_Address = new MemoryBankAddress(0);

            t_Address.Equals(null)
                .Should()
                .BeFalse();
        }

        [Test]
        public void EqualsObjectNull()
        {
            var t_Address = new MemoryBankAddress(0);
            t_Address.Equals((object) null)
                .Should()
                .BeFalse();
        }

        [Test]
        public void EqualsObjectOfCorrectType()
        {
            var t_FirstAddress = new MemoryBankAddress(0);
            object t_SecondAddress = new MemoryBankAddress(0);

            t_FirstAddress.Equals(t_SecondAddress)
                .Should()
                .BeTrue();
        }

        [Test]
        public void EqualsObjectIncorrectType()
        {
            var t_Address = new MemoryBankAddress(0);

            t_Address.Equals(new int())
                .Should()
                .BeFalse();
        }

        [Test]
        public void OperatorEqualsSameInstance()
        {
            var t_Address = new MemoryBankAddress(0);
            (t_Address == t_Address)
                .Should()
                .BeTrue();
        }

        [Test]
        public void OperatorNotEqualsSameInstance()
        {
            var t_Address = new MemoryBankAddress(0);
            (t_Address != t_Address)
                .Should()
                .BeFalse();
        }

        [Test]
        public void OperatorEqualsNull()
        {
            var t_Address = new MemoryBankAddress(0);
            (t_Address == null)
                .Should()
                .BeFalse();
        }

        [Test]
        public void OperatorEqualsInstanceWithSameValue()
        {
            var t_FirstAddress = new MemoryBankAddress(0);
            var t_SecondAddress = new MemoryBankAddress(0);

            (t_FirstAddress == t_SecondAddress)
                .Should()
                .BeTrue();
        }

        [Test]
        public void OperatorEqualsInstanceWithDifferentValue()
        {
            var t_FirstAddress = new MemoryBankAddress(0);
            var t_SecondAddress = new MemoryBankAddress(1);

            (t_FirstAddress != t_SecondAddress)
                .Should()
                .BeTrue();
        }

        [Test]
        public void OperatorBothInstancesEqualsNull()
        {
            MemoryLocationAddress t_First = null;
            MemoryLocationAddress t_Second = null;

            (t_First == t_Second)
                .Should().BeTrue();
        }

        [Test]
        public void HashCode()
        {
            var t_FirstAddress = new MemoryBankAddress(0);
            var t_SecondAddress = new MemoryBankAddress(0);

            (t_FirstAddress.GetHashCode() == t_SecondAddress.GetHashCode())
                .Should().BeTrue();
        }

        [Test]
        public void CompareToSameAddress()
        {
            var t_Address = new MemoryBankAddress(0);

            t_Address
                .CompareTo(t_Address)
                .Should()
                .Be(0);
        }

        [Test]
        public void CompareToHigherAddress()
        {
            var t_Address = new MemoryBankAddress(0);

            t_Address
                .CompareTo(new MemoryBankAddress(10))
                .Should()
                .Be(-1);
        }

        [Test]
        public void CompareToLowerAddress()
        {
            var t_Address = new MemoryBankAddress(10);

            t_Address
                .CompareTo(new MemoryBankAddress(0))
                .Should()
                .Be(1);
        }
    }
}
