using System.Collections.Generic;
using System.Linq;
using Core.Architecture.vCPU.Assembler.Expressions;
using Core.Architecture.vCPU.Assembler.Interface;
using Core.Architecture.vCPU.Assembler.Models;
using Core.Architecture.vCPU.Assembler.Rules;
using Core.Architecture.vCPU.Assembler.Utility;
using Core.Utility.Extensions;
using FluentAssertions;
using NUnit.Framework;
using static vCPUAssemblerTests.Factories.StateFactory;

namespace vCPUAssemblerTests
{
    [TestFixture]
    public class RepeatRuleTests
    {
        [Test]
        public void ParseMultipleOperators()
        {
            var t_Rule = new RepeatRule(new List<IParseRule>
            {
                new OperatorRule("+"),
                new OperatorRule("-")
            });

            var t_Expression = t_Rule.Match(CreateState(
                new Token(TokenType.Operator, "+"),
                new Token(TokenType.Operator, "-"),
                new Token(TokenType.Operator, "+"),
                new Token(TokenType.Operator, "-"),
                new Token(TokenType.Operator, "+"),
                new Token(TokenType.Operator, "-")));

            t_Expression
                .HasError()
                .Should()
                .BeFalse();

            var t_Expressions = t_Expression.Value.Expressions;
            t_Expressions.Should().HaveCount(6);
        }

        [Test]
        public void ParseTrivialMathExpression()
        {
            var t_RepeatRule = new RepeatRule(new List<IParseRule>
            {
                new MultiAttemptRule(new List<IParseRule>
                {
                    new OperatorRule("+"),
                    new OperatorRule("-"),
                    new OperatorRule("*"),
                    new OperatorRule("/"),
                    new OperatorRule("^")
                }),
                new NumericalLiteralRule()
            });

            var t_PatternRule = new RulePatternRule(new List<IParseRule>
            {
                new NumericalLiteralRule(),
                t_RepeatRule
            });

            var t_Expression = t_PatternRule.Match(CreateState(
                new Token(TokenType.Literal, "2"),
                new Token(TokenType.Operator, "+"),
                new Token(TokenType.Literal, "3"),
                new Token(TokenType.Operator, "-"),
                new Token(TokenType.Literal, "4"),
                new Token(TokenType.Operator, "*"),
                new Token(TokenType.Literal, "5"),
                new Token(TokenType.Operator, "/"),
                new Token(TokenType.Literal, "6")));

            t_Expression.HasError().Should().BeFalse();

            var t_Expressions = t_Expression.Value.Expressions;
            t_Expressions.Should().HaveCount(9);

        }
    }
}