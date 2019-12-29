using System;
using System.Collections.Generic;
using System.Text;
using Core.Architecture.vCPU.Assembler.Models;
using Core.Utility;

namespace Core.Architecture.vCPU.Assembler.Interface
{
    public interface ITokenizer
    {
        Either<IEnumerable<Token>> GenerateTokens(string Text);
    }
}
