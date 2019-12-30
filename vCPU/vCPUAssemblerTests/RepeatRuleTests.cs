using System.Collections.Generic;
using System.Linq;
using Core.Architecture.vCPU.Assembler.Expressions;
using Core.Architecture.vCPU.Assembler.Interface;
using Core.Architecture.vCPU.Assembler.Models;
using Core.Architecture.vCPU.Assembler.Rules;
using Core.Architecture.vCPU.Assembler.Utility;
using FluentAssertions;
using NUnit.Framework;

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

            var t_Expression = t_Rule.Match(new Stack<Token>(new List<Token>
            {
                new Token(TokenType.Operator, "+"),
                new Token(TokenType.Operator, "-"),
                new Token(TokenType.Operator, "+"),
                new Token(TokenType.Operator, "-"),
                new Token(TokenType.Operator, "+"),
                new Token(TokenType.Operator, "-")
            }));

            t_Expression
                .HasError()
                .Should()
                .BeFalse();

            t_Expression.Value
                .Should()
                .BeOfType<CombinedExpression>();

            var t_Combined = ((CombinedExpression) t_Expression.Value);
            t_Combined.Expressions
                .Should()
                .HaveCount(3);
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
            }, true);

            var t_PatternRule = new RulePatternRule(new List<IParseRule>
            {
                new NumericalLiteralRule(),
                t_RepeatRule
            });

            var t_Expression = t_PatternRule.Match(new Stack<Token>(new List<Token>
            {
                new Token(TokenType.Literal, "2"),
                new Token(TokenType.Operator, "+"),
                new Token(TokenType.Literal, "3"),
                new Token(TokenType.Operator, "-"),
                new Token(TokenType.Literal, "4"),
                new Token(TokenType.Operator, "*"),
                new Token(TokenType.Literal, "5"),
                new Token(TokenType.Operator, "/"),
                new Token(TokenType.Literal, "6")
            }));

            t_Expression.HasError().Should().BeFalse();
            t_Expression.Value.Should().BeOfType<CombinedExpression>();

            var t_Combined = t_Expression.Value.ToType<CombinedExpression>();
            t_Combined.Expressions.Should().HaveCount(5);
            t_Combined.Expressions.ToList()[0].ToType<NumericalLiteralExpression>()
                .Literal.Text.Should().Be("2");
        }
    }
}