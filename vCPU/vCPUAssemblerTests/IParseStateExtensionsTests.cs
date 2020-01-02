using System.Collections.Generic;
using System.Linq;
using Core.Architecture.vCPU.Assembler.Expressions;
using Core.Architecture.vCPU.Assembler.Interface;
using Core.Architecture.vCPU.Assembler.Models;
using Core.Architecture.vCPU.Assembler.Utility;
using FluentAssertions;
using NUnit.Framework;
using static vCPUAssemblerTests.Factories.StateFactory;

namespace vCPUAssemblerTests
{
    [TestFixture]
    public class IParseStateExtensionsTests
    {
        [Test]
        public void GetDiffTokens()
        {
            var t_CommonToken = new Token(TokenType.Identifier, "Foo");

            var t_State = CreateState(t_CommonToken, 
                new Token(TokenType.Identifier, "Bar"));
            var t_CopyState = CreateState(t_CommonToken);

            var t_Tokens = t_State.DiffTokens(t_CopyState);

            t_Tokens.Should().HaveCount(1);
            t_Tokens.First().Text.Should().Be("Bar");
        }

        [Test]
        public void GetDiffExpressions()
        {
            var t_CommonExpression = new IdentifierExpression();
            var t_State = new ParseState(new Stack<Token>(), 
                new List<IExpression>{ t_CommonExpression });
            var t_CopyState = new ParseState(new Stack<Token>(), 
                new List<IExpression>{ t_CommonExpression, new OperatorExpression() });

            var t_Expressions = t_CopyState.DiffExpressions(t_State);

            t_Expressions.Should().HaveCount(1);
            t_Expressions.Should().OnlyContain(E => E.GetType() == typeof(OperatorExpression));
        }
    }
}