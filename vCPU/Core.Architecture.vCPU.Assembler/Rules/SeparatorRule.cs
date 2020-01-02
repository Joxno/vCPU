using System.Collections.Generic;
using System.Linq;
using Core.Architecture.vCPU.Assembler.Expressions;
using Core.Architecture.vCPU.Assembler.Interface;
using Core.Architecture.vCPU.Assembler.Models;
using Core.Utility;

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