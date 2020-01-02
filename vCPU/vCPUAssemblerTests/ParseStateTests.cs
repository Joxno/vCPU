using System.Collections.Generic;
using Core.Architecture.vCPU.Assembler.Interface;
using Core.Architecture.vCPU.Assembler.Models;
using Core.Utility.Extensions;
using FluentAssertions;
using NUnit.Framework;

namespace vCPUAssemblerTests
{
    [TestFixture]
    public class ParseStateTests
    {
        [Test]
        public void CreateCopyOfState()
        {
            var t_State = new ParseState(
                new List<Token> { new Token(TokenType.Keyword, "*") }.ToStack(),
                new List<IExpression>{});

            var t_Copy = t_State.ToCopy();

            ReferenceEquals(t_State, t_Copy)
                .Should().BeFalse();

            t_Copy.Tokens.Should().HaveCount(1);
        }

        [Test]
        public void CreateEmptyState()
        {
            var t_State = new ParseState();

            t_State.Expressions.Should().NotBeNull();
            t_State.Tokens.Should().NotBeNull();

            t_State.Expressions.Should().HaveCount(0);
            t_State.Tokens.Should().HaveCount(0);
        }
    }
}