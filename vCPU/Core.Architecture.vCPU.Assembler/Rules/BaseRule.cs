using System;
using System.Collections.Generic;
using System.Linq;
using Core.Architecture.vCPU.Assembler.Interface;
using Core.Architecture.vCPU.Assembler.Models;
using Core.Utility;
using Core.Utility.Extensions;

namespace Core.Architecture.vCPU.Assembler.Rules
{
    public class BaseRule : IParseRule
    {
        protected readonly List<Token> m_Pattern = new List<Token>();
        protected readonly Func<IEnumerable<Token>, IExpression> m_CreateExpression = null;
        public bool Repeat { get; } = false;

        public BaseRule()
        {
        }

        public BaseRule(List<Token> Pattern, Func<IEnumerable<Token>, IExpression> CreateExpression)
        {
            m_Pattern = Pattern;
            m_CreateExpression = CreateExpression;
        }

        public virtual Either<IParseState> Match(IParseState State)
        {
            var t_NewState = _PopAndMatch(_GetPattern().ToStack(), State);

            return 
                t_NewState.HasError() ? 
                new Exception("Unable to find Match", t_NewState.Error) : 
                t_NewState;
        }

        protected virtual List<Token> _GetPattern()
        {
            return m_Pattern;
        }

        protected virtual Either<IParseState> _PopAndMatch(Stack<Token> PatternTokens, IParseState State)
        {
            var t_CurrentTokens = State.Tokens;
            var t_MatchedTokens = new List<Token>();
            while (PatternTokens.Count != 0)
            {
                var t_PatternToken = PatternTokens.Pop();
                var t_Token = t_CurrentTokens.Pop();

                if (!_CompareTokens(t_PatternToken, t_Token))
                    return new Exception("Pattern Tokens do not match input Tokens.",
                        new Exception($"Expected: {t_PatternToken.Type} {t_PatternToken.Text} Found: {t_Token.Type} {t_Token.Text}"));

                t_MatchedTokens.Add(t_Token);
            }

            var t_CreatedExpression = _CreateExpression(t_MatchedTokens);

            return t_CreatedExpression.HasError() ?
                new Either<IParseState>(new Exception("No Tokens Matched")) :
                new Either<IParseState>(State.ToCopy(t_CurrentTokens, State.Expressions.Append(t_CreatedExpression.Value)));
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
            var t_Result = Try.Call(() => m_CreateExpression(Tokens));

            return t_Result.HasError() ?
                new Exception("This class is unable to create an expression", t_Result.Error) : 
                t_Result;
        }
    }
}
