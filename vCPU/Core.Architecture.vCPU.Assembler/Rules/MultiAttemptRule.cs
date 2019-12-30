using System;
using System.Collections.Generic;
using System.Linq;
using Core.Architecture.vCPU.Assembler.Interface;
using Core.Architecture.vCPU.Assembler.Models;
using Core.Utility;

namespace Core.Architecture.vCPU.Assembler.Rules
{
    public class MultiAttemptRule : IParseRule
    {
        private IEnumerable<IParseRule> m_Rules = null;

        public MultiAttemptRule(IEnumerable<IParseRule> Rules)
        {
            m_Rules = Rules;
        }

        public bool Repeat { get; }

        public Either<IExpression> Match(Stack<Token> Tokens)
        {
            return m_Rules
                .Select(R => R.Match(new Stack<Token>(Tokens.ToArray())))
                .FirstOrDefault(E => !E.HasError())
                ??
                new Exception("No match was found");
        }
    }
}