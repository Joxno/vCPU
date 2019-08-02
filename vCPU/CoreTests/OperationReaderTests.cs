using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Interfaces;
using Core.Components;
using FluentAssertions;
using Core.Operations;
using Core.DTO;
using Core.Operations.Converters;

namespace CoreTests
{
    [TestClass]
    public class OperationReaderTests
    {
        private IOperationReader m_Reader = null;

        [TestMethod]
        public void ReadANoOp()
        {
            var t_Operation = m_Reader.ReadOperation(new OperationDTO(0));

            t_Operation.Should().BeOfType<NoOp>();
        }

        //[TestMethod]
        //public void ReadALoadOp()
        //{
        //    var t_Operation = m_Reader.ReadOperation(new OperationDTO(1, 0, 0, 0));

        //    t_Operation.Should().BeOfType<OpLoad<int>>();
        //}

        [TestInitialize]
        public void Initialize()
        {
            m_Reader = new OperationReader(new Dictionary<int, IOperationConverter> { { 0, new NoOpConverter() } });
        }
    }
}
