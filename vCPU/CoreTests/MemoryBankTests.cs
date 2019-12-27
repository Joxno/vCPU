using System;
using Core.Components;
using Core.Exceptions;
using Core.Models;
using FluentAssertions;
using NUnit.Framework;

namespace CoreTests
{
    [TestFixture]
    public class MemoryBankTests
    {
        [Test]
        public void InitializeMemoryBank()
        {
            var t_Bank = new MemoryBank(100);
        }

        [Test]
        public void GetSizeOfMemoryBank()
        {
            var t_Bank = new MemoryBank(100);
            var t_Size = t_Bank.Size;

            t_Size.Should().Be(100, "We set MemoryBank to be able to hold 100 bytes in constructor.");
        }

        [Test]
        public void StoreAndLoadValueAtAddress()
        {
            var t_Bank = new MemoryBank(100);
            t_Bank.Store(10, new MemoryAddress(0));

            var t_Value = t_Bank.Load<int>(new MemoryAddress(0));

            t_Value.Should().Be(10, "We stored the number 10 into the MemoryBank.");
        }

        [Test]
        public void ValidateAddress()
        {
            var t_Bank = new MemoryBank(100);
            var t_Valid = t_Bank.IsValid(new MemoryAddress(0));
            t_Valid.Should().Be(true);
        }

        [Test]
        public void ValidateInvalidAddress()
        {
            var t_Bank = new MemoryBank(100);
            var t_Valid = t_Bank.IsValid(new MemoryAddress(-1));
            t_Valid.Should().Be(false);
        }

        [Test]
        public void LoadValueOutOfRangeAddress()
        {
            var t_Bank = new MemoryBank(100);
            Action t_LoadAction = () => t_Bank.Load<int>(new MemoryAddress(200));
            t_LoadAction.Should().Throw<AddressOutOfRange>();
        }

        [Test]
        public void StoreValueOutOfRangeAddress()
        {
            var t_Bank = new MemoryBank(100);
            Action t_StoreAction = () => t_Bank.Store(10, new MemoryAddress(200));
            t_StoreAction.Should().Throw<AddressOutOfRange>();
        }
    }
}
