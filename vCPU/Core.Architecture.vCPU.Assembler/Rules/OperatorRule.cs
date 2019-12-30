using System.Collections.Generic;
using System.Linq;
using Core.Architecture.vCPU.Assembler.Expressions;
using Core.Architecture.vCPU.Assembler.Interface;
using Core.Architecture.vCPU.Assembler.Models;
using Core.Utility;

namespace Core.Architecture.vCPU.Assembler.Rules
{
    public class OperatorRule : BaseRule
    {
        private readonly string m_TokenText = null;

        public OperatorRule()
        {
        }

        public OperatorRule(string TokenText)
        {
            m_TokenText = TokenText;
        }

        protected override List<Token> _GetPattern()
        {
            return new List<Token>()
            {
                new Token(TokenType.Operator, m_TokenText)
            };
        }

        protected override Either<IExpression> _CreateExpression(IEnumerable<Token> Tokens)
        {
            return new OperatorExpression
            {
                Operator = Tokens.First()
            };
        }
    }
}