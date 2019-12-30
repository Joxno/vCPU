using System.Collections.Generic;
using Core.Architecture.vCPU.Assembler.Expressions;
using Core.Architecture.vCPU.Assembler.Models;
using Core.Architecture.vCPU.Assembler.Rules;
using FluentAssertions;
using NUnit.Framework;

namespace vCPUAssemblerTests
{
    [TestFixture]
    public class IdentifierRuleTests
    {
        [Test]
        public void ParseIdentifier()
        {
            var t_Rule = new IdentifierRule();
            var t_Expression = t_Rule.Match(new Stack<Token>
            (
                new List<Token>()
                {
                    new Token(TokenType.Identifier, "RAM")
                }
            ));

            t_Expression
                .HasError()
                .Should()
                .BeFalse();
            t_Expression.Value
                .Should()
                .BeOfType<IdentifierExpression>();
            ((IdentifierExpression) t_Expression.Value)
                .Identifier
                .Text
                .Should()
                .Be("RAM");
        }

        [Test]
        public void ParseInvalidIdentifier()
        {
            var t_Rule = new IdentifierRule();
            var t_Expression = t_Rule.Match(new Stack<Token>
            (
                new List<Token>()
                {
                    new Token(TokenType.Operator, "+")
                }
            ));

            t_Expression
                .HasError()
                .Should()
                .BeTrue();
        }
    }
}