using System.Collections.Generic;
using System.Linq;
using Core.Architecture.vCPU.Assembler.Expressions;
using Core.Architecture.vCPU.Assembler.Models;

namespace Core.Architecture.vCPU.Assembler.Rules
{
    public class KeywordRule : BaseRule
    {
        public KeywordRule(string TokenText = null) : base(new List<Token>
            {
                new Token(TokenType.Keyword, TokenText)
            },
            (Tokens) => new KeywordExpression { Keyword = Tokens.First() })
        {
        }
    }
}