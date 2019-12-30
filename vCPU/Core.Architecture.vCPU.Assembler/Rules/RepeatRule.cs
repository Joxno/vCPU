using System;
using System.Collections.Generic;
using System.Linq;
using Core.Architecture.vCPU.Assembler.Interface;
using Core.Architecture.vCPU.Assembler.Models;
using Core.Utility;

namespace Core.Architecture.vCPU.Assembler.Rules
{
    public class RepeatRule : RulePatternRule
    {
        public RepeatRule(IEnumerable<IParseRule> Rules, bool Repeat = false) : base(Rules, Repeat)
        {
        }

        public override Either<IExpression> Match(Stack<Token> Tokens)
        {
            var t_CurrentExpressions = new List<IExpression>();
            var t_Tokens = new Stack<Token>(Tokens.ToArray());
            while (t_Tokens.Count != 0)
            {
                var t_Expression = _MatchRules(t_Tokens);
                if (t_Expression.HasError())
                    break;

                t_CurrentExpressions.Add(t_Expression.Value);
            }

            return t_CurrentExpressions.Count != 0 ?
                _CreateExpression(t_CurrentExpressions) :
                new Exception("No Rules Matched");
        }

        protected virtual Either<IExpression> _MatchRules(Stack<Token> Tokens)
        {
            var t_CurrentExpressions = new List<IExpression>();
            var t_CurrentTokens = Tokens;//new Stack<Token>(Tokens.ToArray().Reverse());
            foreach (var t_Rule in m_Rules)
            {
                var t_MatchData = _PopUntilMatchOrFail(t_Rule, t_CurrentTokens);
                if (t_MatchData.HasError())
                    return new Exception("Rule Match Error", t_MatchData.Error);

                t_CurrentTokens = t_MatchData.Value.TokensRemaining;
                t_CurrentExpressions.Add(t_MatchData.Value.Expression);
            }

            return base._CreateExpression(t_CurrentExpressions);
        }
    }
}