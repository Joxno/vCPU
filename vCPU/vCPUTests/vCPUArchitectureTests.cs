using System.Collections.Generic;
using Core.Architecture.vCPU.Architecture;
using Core.Interfaces;
using Core.Services;
using FluentAssertions;
using NUnit.Framework;

namespace vCPUTests
{
    [TestFixture]
    public class vCPUArchitectureTests
    {
        private IArchitecture m_Arch = null;

        [Test]
        public void CheckNameOfArchitecture()
        {
            m_Arch.Name
                .Should().Be("vCPU");
        }

        [SetUp]
        public void Initialize()
        {
            m_Arch = new vCPUArchitecture(new MemoryBankService(new List<IMemoryBank>()));
        }
    }
}
