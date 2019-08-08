using System;
using Core.Components;
using Core.Interfaces;
using NUnit.Framework;

namespace CoreTests
{
    [TestFixture]
    public class OperationFeederTests
    {
        private IOperationFeeder m_Feeder = null;

        [Test]
        public void TestMethod1()
        {

        }

        [SetUp]
        public void Initialize()
        {
            m_Feeder = new OperationFeeder();
        }
    }
}
