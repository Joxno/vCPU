﻿using System;
using System.Collections.Generic;
using System.Linq;
using Core.Architecture.vCPU.Assembler.Interface;
using Core.Utility;

namespace Core.Architecture.vCPU.Assembler.Rules
{
    public class RepeatRule : RulePatternRule
    {
        public RepeatRule(IParseRule Rule) 
            : base(new List<IParseRule> { Rule })
        {
        }

        public RepeatRule(IEnumerable<IParseRule> Rules) : base(Rules)
        {
        }

        public override Either<IParseState> Match(IParseState State)
        {
            var t_CurrentState = State;
            while (true)
            {
                var t_NewState = _MatchRules(t_CurrentState);
                if (t_NewState.HasError())
                    break;

                t_CurrentState = t_NewState.Value;
            }

            return 
                State.Expressions.Count() == t_CurrentState.Expressions.Count() ?
                new Exception("No Rules Matched") :
                new Either<IParseState>(t_CurrentState);
        }

        protected virtual Either<IParseState> _MatchRules(IParseState State)
        {
            var t_CurrentState = State;
            foreach (var t_Rule in m_Rules)
            {
                var t_TryCall = Try.Call(() => _MatchRule(t_Rule, t_CurrentState));
                if (t_TryCall.HasError())
                    return t_TryCall.Error;

                var t_NewState = t_TryCall.Value;
                if (t_NewState.HasError())
                    return new Exception("Rule Match Error", t_NewState.Error);

                t_CurrentState = t_NewState.Value;
            }

            return new Either<IParseState>(t_CurrentState);
        }
    }
}