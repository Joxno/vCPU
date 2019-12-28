using Core.Models;
using FluentAssertions;
using NUnit.Framework;

namespace CoreTests
{
    [TestFixture]
    public class MemoryLocationAddressTests
    {
        private MemoryLocationAddress m_LocationAddress = null;

        [Test]
        public void Equals()
        {
            m_LocationAddress
            .Equals(new MemoryLocationAddress(new MemoryAddress(0), new MemoryBankAddress(0)))
            .Should()
            .BeTrue();
        }

        [Test]
        public void EqualsNull()
        {
            m_LocationAddress.Equals(null)
                .Should().BeFalse();
        }

        [Test]
        public void EqualsObjectOfIncorrectType()
        {
            m_LocationAddress.Equals(new int())
                .Should().BeFalse();
        }

        [Test]
        public void EqualsObjectOfCorrectType()
        {
            m_LocationAddress.Equals((object) m_LocationAddress)
                .Should().BeTrue();
        }

        [Test]
        public void EqualsSameInstance()
        {
            m_LocationAddress
            .Equals(m_LocationAddress)
            .Should()
            .BeTrue();
        }

        [Test]
        public void OperatorEqualsSameInstance()
        {
            (m_LocationAddress == m_LocationAddress)
                .Should().BeTrue();
        }

        [Test]
        public void OperatorNotEqualsSameInstance()
        {
            (m_LocationAddress != m_LocationAddress)
                .Should().BeFalse();
        }

        [Test]
        public void OperatorEqualsNull()
        {
            (m_LocationAddress == null)
                .Should().BeFalse();
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
            (m_LocationAddress.GetHashCode()
             ==
             new MemoryLocationAddress(new MemoryAddress(0), new MemoryBankAddress(0)).GetHashCode())
            .Should().BeTrue();
        }

        [Test]
        public void CompareToSameLocation()
        {
            var t_Location = new MemoryLocationAddress(new MemoryAddress(0), new MemoryBankAddress(0));

            t_Location
                .CompareTo(t_Location)
                .Should()
                .Be(0);
        }

        [Test]
        public void CompareToSameBankLowerAddress()
        {
            var t_Lower = new MemoryLocationAddress(new MemoryAddress(0), new MemoryBankAddress(0));
            var t_Higher = new MemoryLocationAddress(new MemoryAddress(10), new MemoryBankAddress(0));

            t_Higher
                .CompareTo(t_Lower)
                .Should()
                .Be(1);
        }

        [Test]
        public void CompareToSameBankHigherAddress()
        {
            var t_Lower = new MemoryLocationAddress(new MemoryAddress(0), new MemoryBankAddress(0));
            var t_Higher = new MemoryLocationAddress(new MemoryAddress(10), new MemoryBankAddress(0));

            t_Lower
                .CompareTo(t_Higher)
                .Should()
                .Be(-1);
        }

        [Test]
        public void CompareToOutsideBank()
        {
            var t_FirstBankAddress = new MemoryLocationAddress(new MemoryAddress(0), new MemoryBankAddress(0));
            var t_SecondBankAddress = new MemoryLocationAddress(new MemoryAddress(10), new MemoryBankAddress(1));

            t_FirstBankAddress
                .CompareTo(t_SecondBankAddress)
                .Should()
                .Be(-1);
        }

        [SetUp]
        public void Initialize()
        {
            m_LocationAddress = new MemoryLocationAddress(new MemoryAddress(0), new MemoryBankAddress(0));
        }
    }
}
