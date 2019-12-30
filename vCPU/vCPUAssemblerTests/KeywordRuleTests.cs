using System.Collections.Generic;
using Core.Architecture.vCPU.Assembler.Expressions;
using Core.Architecture.vCPU.Assembler.Models;
using Core.Architecture.vCPU.Assembler.Rules;
using FluentAssertions;
using NUnit.Framework;

namespace vCPUAssemblerTests
{
    [TestFixture]
    public class KeywordRuleTests
    {
        [Test]
        public void ParseAddKeyword()
        {
            var t_Rule = new KeywordRule();

            var t_Expression = t_Rule.Match(new Stack<Token>(new List<Token>
            {
                new Token(TokenType.Keyword, "Add")
            }));

            t_Expression
                .HasError()
                .Should()
                .BeFalse();

            t_Expression.Value
                .Should()
                .BeOfType<KeywordExpression>();

            ((KeywordExpression) t_Expression.Value)
                .Keyword
                .Text
                .Should()
                .Be("Add");
        }

        [Test]
        public void ParseSpecificKeyword()
        {
            var t_Rule = new KeywordRule("Jmp");

            var t_Expression = t_Rule.Match(new Stack<Token>(new List<Token>
            {
                new Token(TokenType.Keyword, "Jmp")
            }));

            ((KeywordExpression)t_Expression.Value)
                .Keyword
                .Text
                .Should()
                .Be("Jmp");
        }

        [Test]
        public void ParseInvalidSpecificKeyword()
        {
            var t_Rule = new KeywordRule("Jmp");

            var t_Expression = t_Rule.Match(new Stack<Token>(new List<Token>
            {
                new Token(TokenType.Keyword, "Add")
            }));

            t_Expression
                .HasError()
                .Should()
                .BeTrue();
        }
    }
}