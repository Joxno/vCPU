﻿using System;
using System.Collections.Generic;
using System.Linq;
using Core.Architecture.vCPU.Assembler.Interface;
using Core.Utility;

namespace Core.Architecture.vCPU.Assembler.Rules
{
    public class MultiAttemptRule : IParseRule
    {
        private readonly IEnumerable<IParseRule> m_Rules = null;

        public MultiAttemptRule(IEnumerable<IParseRule> Rules)
        {
            m_Rules = Rules;
        }

        public Either<IParseState> Match(IParseState State)
        {
            return m_Rules
                .Select(R => Try.Call(() => R.Match(State)))
                .Where(E => !E.HasError())
                .Select(E => E.Value)
                .Where(E => !E.HasError())
                .OrderBy(S => S.Value.Tokens.Count)
                .FirstOrDefault(E => !E.HasError())
                ??
                new Exception("No match was found");
        }
    }
}