using System.Collections.Generic;
using Core.Architecture.vCPU.Assembler.Models;
using Core.Architecture.vCPU.Assembler.Rules;
using FluentAssertions;
using NUnit.Framework;

namespace vCPUAssemblerTests
{
    [TestFixture]
    public class BaseRuleTests
    {
        [Test]
        public void ParseToken()
        {
            var t_Rule = new BaseRule();

            var t_Expression = t_Rule.Match(new Stack<Token>(new List<Token>()
            {
                new Token(TokenType.None, "Foobar")
            }));

            t_Expression
                .HasError()
                .Should()
                .BeTrue();
        }

        [Test]
        public void CreateExpressionFromBaseRule()
        {
            var t_MockBase = new MockBaseRule();

            var t_Expression = t_MockBase.Match(new Stack<Token>(new List<Token>()
            {
                new Token(TokenType.None, "Foobar")
            }));

            t_Expression
                .HasError()
                .Should()
                .BeTrue();
        }

        internal class MockBaseRule : BaseRule
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