using System;
using System.Collections.Generic;
using System.Linq;
using Core.Architecture.vCPU.Assembler.Expressions;
using Core.Architecture.vCPU.Assembler.Interface;
using Core.Architecture.vCPU.Assembler.Models;
using Core.Architecture.vCPU.Assembler.Rules;
using Core.Architecture.vCPU.Assembler.Utility;
using FluentAssertions;
using NUnit.Framework;
using static vCPUAssemblerTests.Factories.StateFactory;

namespace vCPUAssemblerTests
{
    using CreateExprFunc = Func<IEnumerable<Token>, IEnumerable<IExpression>, IExpression>;

    [TestFixture]
    public class CreateExpressionRuleTests
    {
        [Test]
        public void CreateConstAddressExpression()
        {
            var t_Rule = new CreateExpressionRule(
                _CreateAddressParseRule(),
                _ConstAddressCreator());

            var t_Expression = t_Rule.Match(_CreateConstAddressState());

            t_Expression.HasError().Should().BeFalse();

            var t_Expressions = t_Expression.Value.Expressions.ToList();
            t_Expressions[0].Should().BeOfType<ConstAddressExpression>();
            t_Expressions[0].ToType<ConstAddressExpression>()
                .Literal.Literal.Text.Should().Be("200");
        }

        [Test]
        public void CreateVariantAddressExpression()
        {
            var t_Rule = new CreateExpressionRule(
                _CreateAddressParseRule(),
                _CreateCreators());

            var t_Expression = t_Rule.Match(_CreateConstAddressState());
            t_Expression.HasError().Should().BeFalse();
            t_Expression.Value.Expressions.Should().AllBeOfType<ConstAddressExpression>();
            t_Expression.Value.Expressions.Should()
                .Contain(E => E.ToType<ConstAddressExpression>().Literal.Literal.Text == "200");

            t_Expression = t_Rule.Match(_CreateRelativeAddress());
            t_Expression.HasError().Should().BeFalse();
            t_Expression.Value.Expressions.Should().AllBeOfType<RelativeAddressExpression>();

            var t_Relative = t_Expression.Value.Expressions.First().ToType<RelativeAddressExpression>();
            t_Relative.Identifier.Identifier.Text.Should().Be("RAM");
            t_Relative.Operator.Operator.Text.Should().Be("+");
            t_Relative.Literal.Literal.Text.Should().Be("4");
        }

        [Test]
        public void UnmatchedCreationFunction()
        {
            var t_Rule = new CreateExpressionRule(
                _CreateAddressParseRule(),
                _ConstAddressCreator());

            var t_State = t_Rule.Match(_CreateRelativeAddress());
            t_State.HasError().Should().BeTrue();
        }

        [Test]
        public void InvalidInput()
        {
            var t_Rule = new CreateExpressionRule(
                _CreateAddressParseRule(),
                _ConstAddressCreator());

            var t_Expression = t_Rule.Match(CreateState(new Token(TokenType.Keyword, "Foo")));
            t_Expression.HasError().Should().BeTrue();
        }

        private IParseState _CreateConstAddressState()
        {
            return CreateState(new Token(TokenType.Literal, "200"));
        }

        private IParseState _CreateRelativeAddress()
        {
            return CreateState(
                new Token(TokenType.Identifier, "RAM"),
                new Token(TokenType.Operator, "+"),
                new Token(TokenType.Literal, "4")
            );
        }

        private IEnumerable<CreateExprFunc> _CreateCreators()
        {
            return new List<CreateExprFunc>
            {
                _ConstAddressCreator(),
                _RelativeAddressCreator()
            };
        }

        private CreateExprFunc _ConstAddressCreator()
        {
            return (Tokens, Expr) => new ConstAddressExpression
            {
                Literal = Expr.First().ToType<NumericalLiteralExpression>()
            };
        }

        private CreateExprFunc _RelativeAddressCreator()
        {
            return (Tokens, Expr) => new RelativeAddressExpression
            {
                Identifier = Expr.ToList()[0].ToType<IdentifierExpression>(),
                Operator = Expr.ToList()[1].ToType<OperatorExpression>(),
                Literal = Expr.ToList()[2].ToType<NumericalLiteralExpression>()
            };
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