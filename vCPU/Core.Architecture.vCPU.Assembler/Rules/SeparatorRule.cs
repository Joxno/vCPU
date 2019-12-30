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
        private string m_TokenText = null;

        public SeparatorRule()
        {

        }

        public SeparatorRule(string TokenText)
        {
            m_TokenText = TokenText;
        }

        protected override List<Token> _GetPattern()
        {
            return new List<Token>
            {
                new Token(TokenType.Separator, m_TokenText)
            };
        }

        protected override Either<IExpression> _CreateExpression(IEnumerable<Token> Tokens)
        {
            return new SeparatorExpression
            {
                Separator = Tokens.First()
            };
        }
    }
}