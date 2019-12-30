using System.Collections.Generic;
using Core.Architecture.vCPU.Assembler.Expressions;
using Core.Architecture.vCPU.Assembler.Models;
using Core.Architecture.vCPU.Assembler.Rules;
using FluentAssertions;
using NUnit.Framework;

namespace vCPUAssemblerTests
{
    [TestFixture]
    public class PlusOperatorRuleTests
    {
        [Test]
        public void ParsePlusOperator()
        {
            var t_Rule = new PlusOperatorRule();
            var t_Expression = t_Rule.Match(new Stack<Token>
            (
                new List<Token>()
                {
                    new Token(TokenType.Operator, "+")
                }
            ));

            t_Expression
                .HasError()
                .Should()
                .BeFalse();

            t_Expression.Value
                .Should()
                .BeOfType<PlusOperatorExpression>();

            ((PlusOperatorExpression) t_Expression.Value)
                .Operator
                .Text
                .Should()
                .Be("+");
        }
    }
}