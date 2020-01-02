using System.Collections.Generic;
using System.Linq;
using Core.Architecture.vCPU.Assembler.Interface;
using Core.Architecture.vCPU.Assembler.Models;
using Core.Utility.Extensions;

namespace vCPUAssemblerTests.Factories
{
    public static class StateFactory
    {
        public static IParseState CreateState(Token Token)
        {
            return new ParseState(new List<Token> { Token }.ToStack());
        }

        public static IParseState CreateState(params Token[] Tokens)
        {
            return new ParseState(Tokens.ToList().ToStack());
        }
    }
}