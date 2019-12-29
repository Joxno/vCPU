using System;
using System.Collections.Generic;
using System.Text;
using Core.Architecture.vCPU.Assembler.Models;
using Core.Utility;

namespace Core.Architecture.vCPU.Assembler.Interface
{
    public interface IParser
    {
        Either<IEnumerable<IExpression>> Parse(IEnumerable<Token> Tokens);
    }
}
