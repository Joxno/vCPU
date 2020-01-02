using System;
using System.Collections.Generic;
using Core.Architecture.vCPU.Assembler.Interface;
using Core.Utility;

namespace Core.Architecture.vCPU.Assembler.Rules
{
    public class RulePatternRule : IParseRule
    {
        protected IEnumerable<IParseRule> m_Rules = null;

        public RulePatternRule(IEnumerable<IParseRule> Rules)
        {
            m_Rules = Rules;
        }

        public virtual Either<IParseState> Match(IParseState State)
        {
            var t_CurrentState = State;
            foreach (var t_Rule in m_Rules)
            {
                var t_MatchData = _MatchRule(t_Rule, t_CurrentState);
                if (t_MatchData.HasError())
                    return new Exception("Rule Match Error", t_MatchData.Error);

                t_CurrentState = t_MatchData.Value;
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