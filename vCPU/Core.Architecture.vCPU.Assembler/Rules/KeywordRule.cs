using System.Collections.Generic;
using System.Linq;
using Core.Architecture.vCPU.Assembler.Expressions;
using Core.Architecture.vCPU.Assembler.Interface;
using Core.Architecture.vCPU.Assembler.Models;
using Core.Utility;

namespace Core.Architecture.vCPU.Assembler.Rules
{
    public class KeywordRule : BaseRule
    {
        private readonly string m_TokenText = null;

        public KeywordRule()
        {
        }

        public KeywordRule(string TokenText)
        {
            m_TokenText = TokenText;
        }

        protected override List<Token> _GetPattern()
        {
            return new List<Token>
            {
                new Token(TokenType.Keyword, m_TokenText)
            };
        }

        protected override Either<IExpression> _CreateExpression(IEnumerable<Token> Tokens)
        {
            return new KeywordExpression
            {
                Keyword = Tokens.First()
            };
        }
    }
}