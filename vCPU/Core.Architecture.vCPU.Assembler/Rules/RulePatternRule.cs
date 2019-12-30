using System;
using System.Collections;
using System.Collections.Generic;
using Core.Architecture.vCPU.Assembler.Expressions;
using Core.Architecture.vCPU.Assembler.Interface;
using Core.Architecture.vCPU.Assembler.Models;
using Core.Utility;

namespace Core.Architecture.vCPU.Assembler.Rules
{
    public class RulePatternRule : IParseRule
    {
        protected IEnumerable<IParseRule> m_Rules = null;

        public RulePatternRule(IEnumerable<IParseRule> Rules, bool Repeat = false)
        {
            m_Rules = Rules;
            this.Repeat = Repeat;
        }

        public bool Repeat { get; } = false;

        public virtual Either<IExpression> Match(Stack<Token> Tokens)
        {
            var t_CurrentExpressions = new List<IExpression>();
            var t_CurrentTokens = new Stack<Token>(Tokens.ToArray());
            foreach (var t_Rule in m_Rules)
            {
                do
                {
                    var t_MatchData = _PopUntilMatchOrFail(t_Rule, t_CurrentTokens);
                    if (t_MatchData.HasError() && t_Rule.Repeat)
                        break;
                    if (t_MatchData.HasError())
                        return new Exception("Rule Match Error", t_MatchData.Error);

                    t_CurrentTokens = t_MatchData.Value.TokensRemaining;
                    t_CurrentExpressions.Add(t_MatchData.Value.Expression);
                } while (t_Rule.Repeat);
            }

            return _CreateExpression(t_CurrentExpressions);
        }

        protected virtual Either<MatchData> _PopUntilMatchOrFail(IParseRule Rule, Stack<Token> Tokens)
        {
            var t_CurrentTokens = new List<Token>();
            while (Tokens.Count != 0)
            {
                t_CurrentTokens.Add(Tokens.Pop());
                var t_Expression = Rule
                    .Match(new Stack<Token>(t_CurrentTokens));

                if (!t_Expression.HasError())
                    return new MatchData
                    {
                        Expression = t_Expression.Value, 
                        TokensRemaining = Tokens
                    };

            }

            return new Exception($"Match not found for rule: {Rule}");
        }

        protected virtual Either<IExpression> _CreateExpression(IEnumerable<IExpression> Expressions)
        {
            return new CombinedExpression { Expressions = Expressions};
        }

        protected struct MatchData
        {
            public Stack<Token> TokensRemaining;
            public IExpression Expression;
        }
    }
}