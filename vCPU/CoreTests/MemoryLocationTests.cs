using Core.Components;
using Core.Interfaces;
using Core.Models;
using FluentAssertions;
using NUnit.Framework;

namespace CoreTests
{
    [TestFixture]
    public class MemoryLocationTests
    {
        private IMemoryBank m_Bank = null;

        [Test]
        public void Equals()
        {
            new MemoryLocation(new MemoryAddress(0), m_Bank)
            .Equals(new MemoryLocation(new MemoryAddress(0), m_Bank))
            .Should()
            .BeTrue();
        }

        [Test]
        public void EqualsSameInstance()
        {
            var t_Location = new MemoryLocation(new MemoryAddress(0), m_Bank);
            t_Location
            .Equals(t_Location)
            .Should()
            .BeTrue();
        }

        [Test]
        public void EqualsNull()
        {
            var t_Location = new MemoryLocation(new MemoryAddress(0), m_Bank);

            t_Location.Equals(null)
            .Should()
            .BeFalse();
        }

        [Test]
        public void EqualsIncorrectType()
        {
            var t_Location = new MemoryLocation(new MemoryAddress(0), m_Bank);
            t_Location
            .Equals(new MemoryAddress(0))
            .Should()
            .BeFalse();
        }

        [Test]
        public void EqualsObject()
        {
            var t_Location = new MemoryLocation(new MemoryAddress(0), m_Bank);
            object t_AnotherLocation = new MemoryLocation(new MemoryAddress(1), m_Bank);

            t_Location
            .Equals(t_AnotherLocation)
            .Should()
            .BeFalse();
        }

        [Test]
        public void HashCode()
        {
            var t_Location = new MemoryLocation(new MemoryAddress(0), m_Bank);
            var t_AnotherLocation = new MemoryLocation(new MemoryAddress(0), m_Bank);

            (t_Location.GetHashCode() == t_AnotherLocation.GetHashCode())
            .Should()
            .BeTrue();
        }

        [Test]
        public void OperatorEquals()
        {
            (new MemoryLocation(new MemoryAddress(0), m_Bank)
             ==
             new MemoryLocation(new MemoryAddress(0), m_Bank))
            .Should()
            .BeTrue();
        }

        [Test]
        public void OperatorNotEquals()
        {
            (new MemoryLocation(new MemoryAddress(0), m_Bank)
             !=
             new MemoryLocation(new MemoryAddress(1), m_Bank))
            .Should()
            .BeTrue();
        }

        [Test]
        public void OperatorEqualsNull()
        {
            (new MemoryLocation(new MemoryAddress(0), m_Bank)
             ==
             null)
            .Should()
            .BeFalse();
        }

        [Test]
        public void OperatorBothInstancesEqualsNull()
        {
            MemoryLocation t_FirstLocation = null;
            MemoryLocation t_SecondLocation = null;

            (t_FirstLocation == t_SecondLocation)
            .Should()
            .BeTrue();
        }

        [Test]
        public void OperatorEqualsSameInstance()
        {
            var t_Location = new MemoryLocation(new MemoryAddress(0), m_Bank);
            (t_Location == t_Location)
            .Should()
            .BeTrue();
        }

        [Test]
        public void CompareToSameLocation()
        {
            var t_Location = new MemoryLocation(new MemoryAddress(0), m_Bank);

            t_Location
                .CompareTo(t_Location)
                .Should()
                .Be(0);
        }

        [Test]
        public void CompareToSameBankLowerAddress()
        {
            var t_Lower = new MemoryLocation(new MemoryAddress(0), m_Bank);
            var t_Higher = new MemoryLocation(new MemoryAddress(10), m_Bank);

            t_Higher
                .CompareTo(t_Lower)
                .Should()
                .Be(1);
        }

        [Test]
        public void CompareToSameBankHigherAddress()
        {
            var t_Lower = new MemoryLocation(new MemoryAddress(0), m_Bank);
            var t_Higher = new MemoryLocation(new MemoryAddress(10), m_Bank);

            t_Lower
                .CompareTo(t_Higher)
                .Should()
                .Be(-1);
        }

        [Test]
        public void CompareToOutsideBank()
        {
            var t_OtherBank = new MemoryLocation(new MemoryAddress(0), null);
            var t_DefaultBank = new MemoryLocation(new MemoryAddress(10), m_Bank);

            t_OtherBank
                .CompareTo(t_DefaultBank)
                .Should()
                .Be(-1);
        }

        [SetUp]
        public void Initialize()
        {
            m_Bank = new MemoryBank(32);
        }
    }
}
