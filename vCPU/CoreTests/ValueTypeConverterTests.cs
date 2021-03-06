﻿using System;
using Core.Utility;
using FluentAssertions;
using NUnit.Framework;

namespace CoreTests
{
    [TestFixture]
    public class ValueTypeConverterTests
    {
        [Test]
        public void ConvertIntegerToBytes()
        {
            var t_Converter = new ValueTypeConverter();
            var t_Bytes = t_Converter.ConvertValueTypeToBytes(10);

            t_Bytes.Length.Should().Be(4, "We converted an integer.");
            t_Bytes[0].Should().Be(10);
        }

        [Test]
        public void ConvertBytesToInteger()
        {
            var t_Converter = new ValueTypeConverter();
            var t_Integer = t_Converter.ConvertBytesToValueType<int>(new Byte[] { 10, 0, 0, 0 });

            t_Integer.Should().Be(10);
        }
    }
}
