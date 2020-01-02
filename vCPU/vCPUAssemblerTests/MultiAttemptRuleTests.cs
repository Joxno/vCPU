using System.Collections.Generic;
using System.Linq;
using Core.Architecture.vCPU.Assembler.Expressions;
using Core.Architecture.vCPU.Assembler.Interface;
using Core.Architecture.vCPU.Assembler.Models;
using Core.Architecture.vCPU.Assembler.Rules;
using FluentAssertions;
using NUnit.Framework;
using static vCPUAssemblerTests.Factories.StateFactory;

namespace vCPUAssemblerTests
{
    [TestFixture]
    public class MultiAttemptRuleTests
    {
        [Test]
        public void ParseNumericalLiteral()
        {
            var t_Rule = new MultiAttemptRule(new List<IParseRule>()
            {
                new IdentifierRule(),
                new PlusOperatorRule(),
                new NumericalLiteralRule()
            });

            var t_Expression = t_Rule.Match(CreateState(new Token(TokenType.Literal, "123")));

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
                .Be("123");
        }
    }
}