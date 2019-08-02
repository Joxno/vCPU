using System;
using Core.Components;
using Core.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoreTests
{
    [TestClass]
    public class OperationFeederTests
    {
        private IOperationFeeder m_Feeder = null;

        [TestMethod]
        public void TestMethod1()
        {

        }

        [TestInitialize]
        public void Initialize()
        {
            m_Feeder = new OperationFeeder();
        }
    }
}
