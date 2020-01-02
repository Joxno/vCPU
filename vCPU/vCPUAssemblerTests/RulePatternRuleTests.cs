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
    public class RulePatternRuleTests
    {
        [Test]
        public void ParseAddressWithOffset()
        {
            var t_Rule = new RulePatternRule(new List<IParseRule>
            {
                new IdentifierRule(),
                new PlusOperatorRule(),
                new NumericalLiteralRule()
            });

            var t_Expression = t_Rule.Match(CreateState(
                new Token(TokenType.Identifier, "RAM"),
                new Token(TokenType.Operator, "+"),
                new Token(TokenType.Literal, "4")));

            t_Expression
                .HasError()
                .Should()
                .BeFalse();

            var t_Expressions = t_Expression.Value.Expressions;

            t_Expressions
                .Should()
                .HaveCount(3);

            t_Expressions.ToList()[0].Should()
                .BeOfType<IdentifierExpression>();
            t_Expressions.ToList()[1].Should()
                .BeOfType<PlusOperatorExpression>();
            t_Expressions.ToList()[2].Should()
                .BeOfType<NumericalLiteralExpression>();
        }

        [Test]
        public void ParseInvalidAddressWithOffset()
        {
            var t_Rule = new RulePatternRule(new List<IParseRule>
            {
                new IdentifierRule(),
                new PlusOperatorRule(),
                new NumericalLiteralRule()
            });

            var t_Expression = t_Rule.Match(CreateState(
                new Token(TokenType.Identifier, "RAM"),
                new Token(TokenType.Operator, ","),
                new Token(TokenType.Literal, "4")));

            t_Expression
                .HasError()
                .Should()
                .BeTrue();
        }

        [Test]
        public void ParseJump()
        {
            var t_Rule = _CreateJumpParser();

            var t_Expression = t_Rule.Match(CreateState(
                new Token(TokenType.Keyword, "Jmp"),
                new Token(TokenType.Identifier, "RAM"),
                new Token(TokenType.Operator, "+"),
                new Token(TokenType.Literal, "4")));

            t_Expression
                .HasError()
                .Should()
                .BeFalse();

            t_Expression = t_Rule.Match(CreateState(
                new Token(TokenType.Keyword, "Jmp"),
                new Token(TokenType.Literal, "4")));

            t_Expression
                .HasError()
                .Should()
                .BeFalse();
        }

        private IParseRule _CreateJumpParser()
        {
            return new RulePatternRule(new List<IParseRule>
            {
                new KeywordRule(),
                new MultiAttemptRule(new List<IParseRule>
                {
                    new NumericalLiteralRule(),
                    new RulePatternRule(new List<IParseRule>
                    {
                        new IdentifierRule(),
                        new PlusOperatorRule(),
                        new NumericalLiteralRule()
                    })
                })
            });
        }
    }
}