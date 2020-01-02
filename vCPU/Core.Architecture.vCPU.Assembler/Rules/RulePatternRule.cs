using System;
using System.Collections;
using System.Collections.Generic;
using Core.Architecture.vCPU.Assembler.Expressions;
using Core.Architecture.vCPU.Assembler.Interface;
using Core.Architecture.vCPU.Assembler.Models;
using Core.Utility;
using Core.Utility.Extensions;

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

        public virtual Either<IParseState> Match(IParseState State)
        {
            var t_CurrentExpressions = new List<IExpression>();
            var t_CurrentState = State;
            foreach (var t_Rule in m_Rules)
            {
                do
                {
                    var t_MatchData = _MatchRule(t_Rule, t_CurrentState);
                    if (t_MatchData.HasError() && t_Rule.Repeat)
                        break;
                    if (t_MatchData.HasError())
                        return new Exception("Rule Match Error", t_MatchData.Error);

                    t_CurrentState = t_MatchData.Value;
                } while (t_Rule.Repeat);
            }

            return new Either<IParseState>(t_CurrentState);
        }

        protected virtual Either<IParseState> _MatchRule(IParseRule Rule, IParseState State)
        {
            var t_NewState = Rule
                .Match(State);

            return !t_NewState.HasError() ? 
                t_NewState : 
                new Exception($"Match not found for rule: {Rule}");
        }
    }
}