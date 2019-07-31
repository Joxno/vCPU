using System;
using Core.Components;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoreTests
{
    [TestClass]
    public class MemoryTests
    {
        [TestMethod]
        public void RetrieveDefaultStorageSize()
        {
            var t_Memory = new Memory();

            t_Memory.Size.Should().Be(0, "No size specified.");
        }

        [TestMethod]
        public void RetrieveSpecifiedStorageSize()
        {
            var t_Memory = new Memory(100);

            t_Memory.Size.Should().Be(100, "Number of bytes specified");
        }
    }
}
