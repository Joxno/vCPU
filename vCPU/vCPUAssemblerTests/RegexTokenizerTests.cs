using System;
using System.Collections.Generic;
using System.Linq;
using Core.Architecture.vCPU.Assembler.Interface;
using Core.Architecture.vCPU.Assembler.Models;
using Core.Architecture.vCPU.Assembler.Services;
using FluentAssertions;
using NUnit.Framework;

namespace vCPUAssemblerTests
{
    public class RegexTokenizerTests
    {
        private ITokenizer m_Tokenizer = null;

        [Test]
        public void TokenizeTokens()
        {
            var t_Tokens = m_Tokenizer.GenerateTokens("Add 1, 2");

            t_Tokens
                .HasError()
                .Should()
                .BeFalse();

            t_Tokens.Value
                .Should()
                .HaveCount(4);
            t_Tokens.Value
                .Where(T => T.Type == TokenType.Keyword)
                .Should()
                .HaveCount(1);
            t_Tokens.Value
                .Where(T => T.Type == TokenType.Literal)
                .Should()
                .HaveCount(2);
            t_Tokens.Value
                .Where(T => T.Type == TokenType.Separator)
                .Should()
                .HaveCount(1);
        }

        [Test]
        public void TokenizeUndefinedTokens()
        {
            m_Tokenizer
                .GenerateTokens("...")
                .HasError()
                .Should()
                .BeTrue();
        }

        [Test]
        public void TokenizeAmbiguousTokens()
        {
            var t_Tokens = m_Tokenizer.GenerateTokens("Add");

            t_Tokens
                .HasError()
                .Should()
                .BeFalse();

            t_Tokens.Value
                .Where(T => T.Type == TokenType.Keyword)
                .Should()
                .HaveCount(1);
        }

        [SetUp]
        public void Setup()
        {
            m_Tokenizer = new RegexTokenizer(
                new List<TokenPattern>()
                {
                    new TokenPattern(TokenType.Keyword, "^Add", 1),
                    new TokenPattern(TokenType.Separator, "^,"),
                    new TokenPattern(TokenType.Literal, "^[0-9]+"),
                    new TokenPattern(TokenType.Identifier, "^[a-zA-Z_]{1}[a-zA-Z0-9_]+")
                }
            );
        }
    }
}