using System.Collections.Generic;
using Core.Architecture.vCPU.Assembler.Expressions;
using Core.Architecture.vCPU.Assembler.Models;
using Core.Architecture.vCPU.Assembler.Rules;
using FluentAssertions;
using NUnit.Framework;

namespace vCPUAssemblerTests
{
    [TestFixture]
    public class SeparatorRuleTests
    {
        [Test]
        public void ParseSeparator()
        {
            var t_Rule = new SeparatorRule();

            var t_Expression = t_Rule.Match(new Stack<Token>(new List<Token>
            {
                new Token(TokenType.Separator, ",")
            }));

            t_Expression
                .HasError()
                .Should()
                .BeFalse();

            t_Expression.Value
                .Should()
                .BeOfType<SeparatorExpression>();

            ((SeparatorExpression) t_Expression.Value)
                .Separator
                .Text
                .Should()
                .Be(",");
        }

        [Test]
        public void ParseInvalidSeparator()
        {
            var t_Rule = new SeparatorRule();

            var t_Expression = t_Rule.Match(new Stack<Token>(new List<Token>
            {
                new Token(TokenType.Literal, "foo")
            }));

            t_Expression
                .HasError()
                .Should()
                .BeTrue();
        }

        [Test]
        public void ParseSpecificSeparator()
        {
            var t_Rule = new SeparatorRule(".");

            var t_Expression = t_Rule.Match(new Stack<Token>(new List<Token>
            {
                new Token(TokenType.Separator, ".")
            }));

            t_Expression
                .HasError()
                .Should()
                .BeFalse();

            t_Expression.Value
                .Should()
                .BeOfType<SeparatorExpression>();

            ((SeparatorExpression)t_Expression.Value)
                .Separator
                .Text
                .Should()
                .Be(".");
        }
    }
}