using System;
using System.Collections.Generic;
using System.Linq;
using Core.Architecture.vCPU.Assembler.Expressions;
using Core.Architecture.vCPU.Assembler.Models;
using Core.Architecture.vCPU.Assembler.Rules;
using FluentAssertions;
using NUnit.Framework;
using static vCPUAssemblerTests.Factories.StateFactory;

namespace vCPUAssemblerTests
{
    [TestFixture]
    public class BaseRuleTests
    {
        [Test]
        public void ParseToken()
        {
            var t_Rule = new BaseRule();

            var t_Expression = t_Rule.Match(CreateState(new Token(TokenType.None, "Foobar")));

            t_Expression.HasError().Should().BeTrue();
        }

        [Test]
        public void CreateExpressionFromBaseRule()
        {
            var t_MockBase = new MockInheritedBaseRule();

            var t_Expression = t_MockBase.Match(CreateState(new Token(TokenType.None, "Foobar")));

            t_Expression.HasError().Should().BeTrue();
        }

        [Test]
        public void CreateSpecificExpressionFromBaseRule()
        {
            var t_Rule = new BaseRule(new List<Token>
            {
                new Token(TokenType.Keyword, null)
            }, (Tokens) => new KeywordExpression { Keyword = Tokens.First() });

            var t_Expression = t_Rule.Match(CreateState(new Token(TokenType.Keyword, "*")));

            t_Expression.HasError().Should().BeFalse();
        }

        [Test]
        public void ThrowInExpressionCreationFunction()
        {
            var t_Rule = new BaseRule(new List<Token>
            {
                new Token(TokenType.Keyword, null)
            }, (Tokens) => throw new Exception());

            var t_Expression = t_Rule.Match(CreateState(new Token(TokenType.Keyword, "*")));

            t_Expression.HasError().Should().BeTrue();
        }

        internal class MockInheritedBaseRule : BaseRule
        {
            protected override List<Token> _GetPattern()
            {
                return new List<Token>()
                {
                    new Token(TokenType.None, "Foobar")
                };
            }
        }
    }
}