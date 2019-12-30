﻿using System.Collections.Generic;
using Core.Architecture.vCPU.Assembler.Models;
using Core.Utility;

namespace Core.Architecture.vCPU.Assembler.Interface
{
    public interface IParseRule
    {
        public bool Repeat { get; }
        Either<IExpression> Match(Stack<Token> Tokens);
    }
}