using System.Collections.Generic;
using System.Linq;
using Core.Architecture.vCPU.Assembler.Expressions;
using Core.Architecture.vCPU.Assembler.Models;
using Core.Architecture.vCPU.Assembler.Rules;
using Core.Utility.Extensions;
using FluentAssertions;
using NUnit.Framework;
using static vCPUAssemblerTests.Factories.StateFactory;

namespace vCPUAssemblerTests
{
    [TestFixture]
    public class NumericalLiteralTests
    {
        [Test]
        public void ParseNumericalLiteral()
        {
            var t_Rule = new NumericalLiteralRule();

            var t_Expression = t_Rule.Match(CreateState(new Token(TokenType.Literal, "12345")));

            t_Expression
                .HasError()
                .Should()
                .BeFalse();

            t_Expression.Value.Expressions.First()
                .Should()
                .BeOfType<NumericalLiteralExpression>();

            ((NumericalLiteralExpression) t_Expression.Value.Expressions.First())
                .Literal
                .Text
                .Should()
                .Be("12345");
        }
    }
}