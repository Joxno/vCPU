using System.Collections.Generic;
using Core.Architecture.vCPU.Assembler.Expressions;
using Core.Architecture.vCPU.Assembler.Models;
using Core.Architecture.vCPU.Assembler.Rules;
using FluentAssertions;
using NUnit.Framework;

namespace vCPUAssemblerTests
{
    [TestFixture]
    public class AddressRuleTests
    {
        [Test]
        public void ParseBankAddress()
        {
            var t_Rule = new BankAddressRule();
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
                .BeOfType<BankAddressExpression>();
            ((BankAddressExpression) t_Expression.Value)
                .BankAddress
                .Text
                .Should()
                .Be("RAM");
        }

        [Test]
        public void ParseInvalidBankAddress()
        {
            var t_Rule = new BankAddressRule();
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