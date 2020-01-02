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
    public class OperatorRuleTests
    {
        [Test]
        public void ParseMinusOperator()
        {
            var t_Rule = new OperatorRule();
            var t_Expression = t_Rule.Match(CreateState(new Token(TokenType.Operator, "-")));

            t_Expression
                .HasError()
                .Should()
                .BeFalse();

            t_Expression.Value.Expressions.First()
                .Should()
                .BeOfType<OperatorExpression>();

            ((OperatorExpression)t_Expression.Value.Expressions.First())
                .Operator
                .Text
                .Should()
                .Be("-");
        }

        [Test]
        public void ParseSpecificOperator()
        {
            var t_Rule = new OperatorRule("::");
            var t_Expression = t_Rule.Match(CreateState(new Token(TokenType.Operator, "::")));

            t_Expression
                .HasError()
                .Should()
                .BeFalse();

            t_Expression.Value.Expressions.First()
                .Should()
                .BeOfType<OperatorExpression>();

            ((OperatorExpression)t_Expression.Value.Expressions.First())
                .Operator
                .Text
                .Should()
                .Be("::");
        }
    }
}