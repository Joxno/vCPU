using System;
using Core.Components;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoreTests
{
    [TestClass]
    public class MemoryUnitTests
    {
        [TestMethod]
        public void RetrieveDefaultStorageSize()
        {
            var t_Memory = new MemoryUnit();

            t_Memory.Size.Should().Be(0, "No size specified.");
        }

        [TestMethod]
        public void RetrieveSpecifiedStorageSize()
        {
            var t_Memory = new MemoryUnit(100);

            t_Memory.Size.Should().Be(100, "Number of bytes specified.");
        }

        [TestMethod]
        public void StoreAndRetrieveIntegerValue()
        {
            var t_Memory = new MemoryUnit(4);

            t_Memory.Store(10);
            var t_Value = t_Memory.Retrieve<int>();

            t_Value.Should().Be(10, "We stored the number 10.");
        }
    }
}
