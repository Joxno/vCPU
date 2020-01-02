using System.Collections.Generic;
using System.Linq;
using Core.Architecture.vCPU.Assembler.Expressions;
using Core.Architecture.vCPU.Assembler.Interface;
using Core.Architecture.vCPU.Assembler.Models;
using Core.Utility;

namespace Core.Architecture.vCPU.Assembler.Rules
{
    public class IdentifierRule : BaseRule
    {
        public IdentifierRule() : base(new List<Token>
        {
            new Token(TokenType.Identifier, null)
        }, 
        (Tokens) => new IdentifierExpression { Identifier = Tokens.First()})
        {
        }
    }
}
