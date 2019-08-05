using System;
using Core.DTO;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoreTests
{
    [TestClass]
    public class OperationDTOTests
    {
        [TestMethod]
        public void RetrieveSizeOfNoOp()
        {
            var t_DTO = new OperationDTO(0, new byte[] { });

            t_DTO.Size.Should().Be(1);
        }

        [TestMethod]
        public void RetrieveSizeOfOpLoadValue()
        {
            var t_DTO = new OperationDTO(1, new byte[] 
            {
                10, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 0
            });

            t_DTO.Size.Should().Be(1 + 4 * 3);
        }
    }
}
