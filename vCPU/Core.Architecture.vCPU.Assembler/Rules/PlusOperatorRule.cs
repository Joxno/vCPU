using System.Collections.Generic;
using System.Linq;
using Core.Architecture.vCPU.Assembler.Expressions;
using Core.Architecture.vCPU.Assembler.Models;

namespace Core.Architecture.vCPU.Assembler.Rules
{
    public class PlusOperatorRule : BaseRule
    {
        public PlusOperatorRule() : base(new List<Token>
        {
            new Token(TokenType.Operator, "+")
        },
        (Tokens) => new PlusOperatorExpression { Operator = Tokens.First() })
        {
        }
    }
}
