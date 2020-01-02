using System.Collections.Generic;
using System.Linq;
using Core.Architecture.vCPU.Assembler.Interface;
using Core.Utility.Extensions;

namespace Core.Architecture.vCPU.Assembler.Models
{
    public class ParseState : IParseState
    {
        private Stack<Token> m_Tokens = new Stack<Token>();
        private IEnumerable<IExpression> m_Expressions = new List<IExpression>();

        public ParseState()
        {
        }

        public ParseState(Stack<Token> Tokens)
        {
            m_Tokens = Tokens;
        }

        public ParseState(Stack<Token> Tokens, IEnumerable<IExpression> Expressions)
        {
            m_Tokens = Tokens;
            m_Expressions = Expressions;
        }

        public Stack<Token> Tokens => m_Tokens.ToStack();
        public IEnumerable<IExpression> Expressions => m_Expressions.ToList();
        public IParseState ToCopy()
        {
            return new ParseState(Tokens, Expressions);
        }

        public IParseState ToCopy(Stack<Token> Tokens, IEnumerable<IExpression> Expressions)
        {
            return new ParseState(Tokens, Expressions);
        }
    }
}