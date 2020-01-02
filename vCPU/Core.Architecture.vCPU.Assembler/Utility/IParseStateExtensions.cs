using System.Collections.Generic;
using System.Linq;
using Core.Architecture.vCPU.Assembler.Interface;
using Core.Architecture.vCPU.Assembler.Models;

namespace Core.Architecture.vCPU.Assembler.Utility
{
    public static class IParseStateExtensions
    {
        public static IEnumerable<Token> DiffTokens(this IParseState State, IParseState OtherState)
        {
            return 
                OtherState.Tokens.Count == 0 ?
                State.Tokens :
                State.Tokens.ToList().Except(
                OtherState.Tokens.ToList());
        }

        public static IEnumerable<IExpression> DiffExpressions(this IParseState State, IParseState OtherState)
        {
            return
                !OtherState.Expressions.Any() ?
                State.Expressions :
                State.Expressions.Except(
                OtherState.Expressions);
        }
    }
}