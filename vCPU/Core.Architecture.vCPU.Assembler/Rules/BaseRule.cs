using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Architecture.vCPU.Assembler.Expressions;
using Core.Architecture.vCPU.Assembler.Interface;
using Core.Architecture.vCPU.Assembler.Models;
using Core.Utility;

namespace Core.Architecture.vCPU.Assembler.Rules
{
    public class BaseRule : IParseRule
    {

        public virtual Either<IExpression> Match(Stack<Token> Tokens)
        {
            var t_Tokens = _PopAndMatch(new Stack<Token>(_GetPattern()), new Stack<Token>(Tokens.ToArray()));

            return 
                t_Tokens.HasError() ? 
                new Exception("Unable to find Match", t_Tokens.Error) : 
                _CreateExpression(t_Tokens.Value);
        }

        protected virtual List<Token> _GetPattern()
        {
            return new List<Token>();
        }

        protected virtual Either<IEnumerable<Token>> _PopAndMatch(Stack<Token> PatternTokens, Stack<Token> Tokens)
        {
            var t_Tokens = new List<Token>();
            while (PatternTokens.Count != 0)
            {
                var t_PatternToken = PatternTokens.Pop();
                var t_Token = Tokens.Pop();

                if (!_CompareTokens(t_PatternToken, t_Token))
                    return new Exception("Pattern Tokens do not match input Tokens.",
                        new Exception($"Expected: {t_PatternToken.Type} {t_PatternToken.Text} Found: {t_Token.Type} {t_Token.Text}"));

                t_Tokens.Add(t_Token);
            }

            return
                t_Tokens.Count != 0 ?
                    t_Tokens :
                    new Either<IEnumerable<Token>>(new Exception("No Tokens Matched"));
        }

        protected virtual bool _CompareTokens(Token PatternToken, Token InputToken)
        {
            if (PatternToken.Type == InputToken.Type &&
                (PatternToken.Text == null ||
                 PatternToken.Text == InputToken.Text))
                return true;
            return false;
        }

        protected virtual Either<IExpression> _CreateExpression(IEnumerable<Token> Tokens)
        {
            return new Exception("This class is unable to create an expression");
        }
    }
}
