using System;
using Core.Components;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoreTests
{
    [TestClass]
    public class MemoryBankTests
    {
        [TestMethod]
        public void InitializeMemoryBank()
        {
            var t_Bank = new MemoryBank(100);
        }

        [TestMethod]
        public void GetSizeOfMemoryBank()
        {
            var t_Bank = new MemoryBank(100);
            var t_Size = t_Bank.Size;

            t_Size.Should().Be(100, "We set MemoryBank to be able to hold 100 bytes in constructor.");
        }
    }
}
