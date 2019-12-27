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

        [SetUp]
        public void Initialize()
        {
            m_LocationAddress = new MemoryLocationAddress(new MemoryAddress(0), new MemoryBankAddress(0));
        }
    }
}
