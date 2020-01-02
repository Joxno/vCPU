using System.Collections.Generic;
using System.Linq;
using Core.Architecture.vCPU.Assembler.Expressions;
using Core.Architecture.vCPU.Assembler.Models;

namespace Core.Architecture.vCPU.Assembler.Rules
{
    public class SeparatorRule : BaseRule
    {
        public SeparatorRule(string TokenText = null) : base(new List<Token>
        {
            new Token(TokenType.Separator, TokenText)
        },
        (Tokens) => new SeparatorExpression { Separator = Tokens.First() })
        {
        }
    }
}