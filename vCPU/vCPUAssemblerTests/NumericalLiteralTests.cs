using System.Collections.Generic;
using Core.Architecture.vCPU.Assembler.Expressions;
using Core.Architecture.vCPU.Assembler.Models;
using Core.Architecture.vCPU.Assembler.Rules;
using FluentAssertions;
using NUnit.Framework;

namespace vCPUAssemblerTests
{
    [TestFixture]
    public class NumericalLiteralTests
    {
        [Test]
        public void ParseNumericalLiteral()
        {
            var t_Rule = new NumericalLiteralRule();

            var t_Expression = t_Rule.Match(new Stack<Token>
            (
                new List<Token>()
                {
                    new Token(TokenType.Literal, "12345")
                }
            ));

            t_Expression
                .HasError()
                .Should()
                .BeFalse();

            t_Expression.Value
                .Should()
                .BeOfType<NumericalLiteralExpression>();

            ((NumericalLiteralExpression) t_Expression.Value)
                .Literal
                .Text
                .Should()
                .Be("12345");
        }
    }
}