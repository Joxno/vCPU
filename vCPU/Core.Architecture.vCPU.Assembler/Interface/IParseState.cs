using System.Collections;
using System.Collections.Generic;
using Core.Architecture.vCPU.Assembler.Models;

namespace Core.Architecture.vCPU.Assembler.Interface
{
    public interface IParseState
    {
        Stack<Token> Tokens { get; }
        IEnumerable<IExpression> Expressions { get; }
        IParseState ToCopy();
        IParseState ToCopy(Stack<Token> Tokens, IEnumerable<IExpression> Expressions);
    }
}