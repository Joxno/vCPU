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
    public class PlusOperatorRuleTests
    {
        [Test]
        public void ParsePlusOperator()
        {
            var t_Rule = new PlusOperatorRule();
            var t_Expression = t_Rule.Match(CreateState(new Token(TokenType.Operator, "+")));

            t_Expression
                .HasError()
                .Should()
                .BeFalse();

            t_Expression.Value.Expressions.First()
                .Should()
                .BeOfType<PlusOperatorExpression>();

            ((PlusOperatorExpression) t_Expression.Value.Expressions.First())
                .Operator
                .Text
                .Should()
                .Be("+");
        }
    }
}