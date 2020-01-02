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
    public class KeywordRuleTests
    {
        [Test]
        public void ParseAddKeyword()
        {
            var t_Rule = new KeywordRule();

            var t_Expression = t_Rule.Match(CreateState(new Token(TokenType.Keyword, "Add")));

            t_Expression
                .HasError()
                .Should()
                .BeFalse();

            t_Expression.Value.Expressions.First()
                .Should()
                .BeOfType<KeywordExpression>();

            ((KeywordExpression) t_Expression.Value.Expressions.First())
                .Keyword
                .Text
                .Should()
                .Be("Add");
        }

        [Test]
        public void ParseSpecificKeyword()
        {
            var t_Rule = new KeywordRule("Jmp");

            var t_Expression = t_Rule.Match(CreateState(new Token(TokenType.Keyword, "Jmp")));

            ((KeywordExpression)t_Expression.Value.Expressions.First())
                .Keyword
                .Text
                .Should()
                .Be("Jmp");
        }

        [Test]
        public void ParseInvalidSpecificKeyword()
        {
            var t_Rule = new KeywordRule("Jmp");

            var t_Expression = t_Rule.Match(CreateState(new Token(TokenType.Keyword, "Add")));

            t_Expression
                .HasError()
                .Should()
                .BeTrue();
        }
    }
}