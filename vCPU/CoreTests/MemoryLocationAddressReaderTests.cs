using Core.Components;
using Core.Interfaces;
using Core.Models;
using FluentAssertions;
using NUnit.Framework;

namespace CoreTests
{
    [TestFixture]
    public class MemoryLocationAddressReaderTests
    {
        private IMemoryLocationAddressReader m_Reader = null;
        private IMemoryBank m_Bank = null;

        [Test]
        public void ReadMemoryLocationAddressFromMemory()
        {
            m_Bank.Store(10, new MemoryAddress(0));
            m_Bank.Store(20, new MemoryAddress(4));

            var t_LocationAddress = m_Reader.ReadLocationAddressFromMemory(new MemoryAddress(0), m_Bank);

            (
            t_LocationAddress
            ==
            new MemoryLocationAddress(new MemoryAddress(10), new MemoryBankAddress(20))
            )
            .Should()
            .BeTrue();
        }

        [SetUp]
        public void Initialize()
        {
            m_Reader = new MemoryLocationAddressReader();
            m_Bank = new MemoryBank(8);
        }
    }
}
