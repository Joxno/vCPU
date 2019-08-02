using System;
using Core.DTO;
using Core.Operations;
using Core.Operations.Converters;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoreTests
{
    [TestClass]
    public class OperationConverterTests
    {
        [TestMethod]
        public void ConvertNoOp()
        {
            var t_Converter = new NoOpConverter();
            var t_NoOp = t_Converter.Convert(new OperationDTO(0));

            t_NoOp.Should().BeOfType<NoOp>();
        }

        //[TestMethod]
        //public void ConvertLoadValueOp()
        //{
        //    var t_Converter = new OpLoadConverter();
        //    var t_LoadOp = t_Converter.Convert(new OperationDTO(0, 0, 0, 0));

        //    t_LoadOp.Should().BeOfType<OpLoad<int>>();
        //}
    }
}
