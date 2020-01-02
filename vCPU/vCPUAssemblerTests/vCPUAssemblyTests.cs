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
    public class vCPUAssemblyTests
    {
        [Test]
        public void ParseInstruction()
        {
            var t_InstructionRule = _CreateInstructionParseRule();
            var t_Assembly = new List<Token>
            {
                new Token(TokenType.Keyword, "halt")
            };

            var t_Expression = t_InstructionRule.Match(CreateState(t_Assembly.ToArray()));

            t_Expression.HasError().Should().BeFalse();
            var t_Expressions = t_Expression.Value.Expressions.ToList();
            t_Expressions[0].Should().BeOfType<KeywordExpression>();
        }

        [Test]
        public void ParseInstructionWithNumericalParameter()
        {
            var t_InstructionRule = _CreateInstructionParseRule();
            var t_Assembly = new List<Token>
            {
                new Token(TokenType.Keyword, "jmp"),
                new Token(TokenType.Literal, "4")
            };

            var t_Expression = t_InstructionRule.Match(CreateState(t_Assembly.ToArray()));
            t_Expression.HasError().Should().BeFalse();

            var t_Expressions = t_Expression.Value.Expressions.ToList();
            t_Expressions[0]
                .Should().BeOfType<KeywordExpression>();
            t_Expressions[1]
                .Should().BeOfType<NumericalLiteralExpression>();
        }

        [Test]
        public void ParseInstructionWithRelativeAddressParameter()
        {
            var t_InstructionRule = _CreateInstructionParseRule();
            var t_Assembly = new List<Token>
            {
                new Token(TokenType.Keyword, "jmp"),
                new Token(TokenType.Identifier, "RAM"),
                new Token(TokenType.Operator, "+"),
                new Token(TokenType.Literal, "4")
            };

            var t_Expression = t_InstructionRule.Match(CreateState(t_Assembly.ToArray()));
            t_Expression.HasError().Should().BeFalse();
            
            var t_Expressions = t_Expression.Value.Expressions.ToList();
            t_Expressions[0].Should().BeOfType<KeywordExpression>();
            t_Expressions[1].Should().BeOfType<IdentifierExpression>();
            t_Expressions[2].Should().BeOfType<OperatorExpression>();
            t_Expressions[3].Should().BeOfType<NumericalLiteralExpression>();
        }

        [Test]
        public void ParseInstructionWithMultipleParameters()
        {
            var t_InstructionRule = _CreateInstructionParseRule();
            var t_Assembly = new List<Token>
            {
                new Token(TokenType.Keyword, "Add"),
                new Token(TokenType.Literal, "4"),
                new Token(TokenType.Separator, ","),
                new Token(TokenType.Literal, "4")
            };

            var t_Expression = t_InstructionRule.Match(CreateState(t_Assembly.ToArray()));
            t_Expression.HasError().Should().BeFalse();

            var t_Expressions = t_Expression.Value.Expressions.ToList();
            t_Expressions[0].Should().BeOfType<KeywordExpression>();
            t_Expressions[1].Should().BeOfType<NumericalLiteralExpression>();
            t_Expressions[2].Should().BeOfType<SeparatorExpression>();
            t_Expressions[3].Should().BeOfType<NumericalLiteralExpression>();
        }

        [Test]
        public void ParseConstAddress()
        {
            var t_AddressRule = _CreateAddressParseRule();
            var t_Assembly = new List<Token>
            {
                new Token(TokenType.Literal, "4")
            };

            var t_Expression = t_AddressRule.Match(CreateState(t_Assembly.ToArray()));

            t_Expression.HasError().Should().BeFalse();

            var t_Expressions = t_Expression.Value.Expressions;
            t_Expressions.First().Should().BeOfType<NumericalLiteralExpression>();
        }

        [Test]
        public void ParseRelativeAddress()
        {
            var t_AddressRule = _CreateAddressParseRule();
            var t_Assembly = new List<Token>
            {
                new Token(TokenType.Identifier, "RAM"),
                new Token(TokenType.Operator, "+"),
                new Token(TokenType.Literal, "4")
            };

            var t_Expression = t_AddressRule.Match(CreateState(t_Assembly.ToArray()));

            t_Expression.HasError().Should().BeFalse();
        }

        [Test]
        public void ParseParameterWithConstAddress()
        {
            var t_ParameterParseRule = _CreateParameterParseRule();
            var t_Assembly = new List<Token>
            {
                new Token(TokenType.Literal, "4")
            };

            var t_Expression = t_ParameterParseRule.Match(CreateState(t_Assembly.ToArray()));

            t_Expression.HasError().Should().BeFalse();
            var t_Expressions = t_Expression.Value.Expressions;
            t_Expressions.First().Should().BeOfType<NumericalLiteralExpression>();
        }

        [Test]
        public void ParseParameterWithRelativeAddress()
        {
            var t_ParameterParseRule = _CreateParameterParseRule();
            var t_Assembly = new List<Token>
            {
                new Token(TokenType.Identifier, "RAM"),
                new Token(TokenType.Operator, "+"),
                new Token(TokenType.Literal, "4")
            };

            var t_Expression = t_ParameterParseRule.Match(CreateState(t_Assembly.ToArray()));

            t_Expression.HasError().Should().BeFalse();

            var t_Expressions = t_Expression.Value.Expressions.ToList();
            t_Expressions[0].Should().BeOfType<IdentifierExpression>();
            t_Expressions[1].Should().BeOfType<OperatorExpression>();
            t_Expressions[2].Should().BeOfType<NumericalLiteralExpression>();
        }

        private IParseRule _CreateInstructionParseRule()
        {
            return new MultiAttemptRule(new List<IParseRule>
            {
                new RulePatternRule(new List<IParseRule>
                {
                    new KeywordRule(),
                    _CreateParameterParseRule()
                }),
                new KeywordRule()
            });
        }

        private IParseRule _CreateParameterParseRule()
        {
            return new RepeatRule(
                new RulePatternRule(new List<IParseRule>
                {
                    new MultiAttemptRule(new List<IParseRule>
                    {
                        _CreateAddressParseRule(),
                        new RulePatternRule(new List<IParseRule>
                        {
                            _CreateAddressParseRule(),
                            new SeparatorRule(",")
                        })
                    })
                })
            );
        }

        private IParseRule _CreateAddressParseRule()
        {
            return new RulePatternRule(new List<IParseRule>
            {
                new MultiAttemptRule(new List<IParseRule>
                {
                    new NumericalLiteralRule(),
                    new IdentifierRule(),
                    new RulePatternRule(new List<IParseRule>
                    {
                        new IdentifierRule(),
                        new MultiAttemptRule(new List<IParseRule>
                        {
                            new OperatorRule("+"),
                            new OperatorRule("-")
                        }),
                        new NumericalLiteralRule()
                    })
                })
            });
        }
    }
}