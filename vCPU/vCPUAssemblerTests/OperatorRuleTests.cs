using System.Collections.Generic;
using Core.Architecture.vCPU.Assembler.Expressions;
using Core.Architecture.vCPU.Assembler.Models;
using Core.Architecture.vCPU.Assembler.Rules;
using FluentAssertions;
using NUnit.Framework;

namespace vCPUAssemblerTests
{
    [TestFixture]
    public class OperatorRuleTests
    {
        [Test]
        public void ParseMinusOperator()
        {
            var t_Rule = new OperatorRule();
            var t_Expression = t_Rule.Match(new Stack<Token>
            (
                new List<Token>()
                {
                    new Token(TokenType.Operator, "-")
                }
            ));

            t_Expression
                .HasError()
                .Should()
                .BeFalse();

            t_Expression.Value
                .Should()
                .BeOfType<OperatorExpression>();

            ((OperatorExpression)t_Expression.Value)
                .Operator
                .Text
                .Should()
                .Be("-");
        }

        [Test]
        public void ParseSpecificOperator()
        {
            var t_Rule = new OperatorRule("::");
            var t_Expression = t_Rule.Match(new Stack<Token>
            (
                new List<Token>()
                {
                    new Token(TokenType.Operator, "::")
                }
            ));

            t_Expression
                .HasError()
                .Should()
                .BeFalse();

            t_Expression.Value
                .Should()
                .BeOfType<OperatorExpression>();

            ((OperatorExpression)t_Expression.Value)
                .Operator
                .Text
                .Should()
                .Be("::");
        }
    }
}