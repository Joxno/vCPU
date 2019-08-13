using System;
using System.Collections.Generic;
using Core.DTO;
using Core.Interfaces;
using Core.Models;
using Core.Operations.Converters;
using FluentAssertions;
using NUnit.Framework;

namespace CoreTests
{
    [TestFixture]
    public class ArchitectureTests
    {
        private IArchitecture m_Arch;

        [Test]
        public void SupportedOpCodes()
        {
            var t_Supported = m_Arch.SupportedCodes();

            t_Supported.Should().Contain(0);
        }

        [Test]
        public void GetConverterForCode()
        {
            var t_Converter = m_Arch.GetConverterForCode(0);

            t_Converter.Should().BeOfType<NoOpConverter>();
        }

        [Test]
        public void GetDefinitionForCode()
        {
            var t_Definition = m_Arch.GetDefinitionForCode(0);

            t_Definition.OpCode.Should().Be(0);
            t_Definition.Size.Should().Be(0);
        }

        [SetUp]
        public void Initialize()
        {
            m_Arch = new Architecture(new Dictionary<int, IOperationConverter>
            {
                { 0, new NoOpConverter() }
            },
            new List<OperationDefinition>
            {
                new OperationDefinition(0, 0)
            });
        }
    }
}
