﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Architecture.vCPU.Assembler.Expressions;
using Core.Architecture.vCPU.Assembler.Interface;
using Core.Architecture.vCPU.Assembler.Models;
using Core.Utility;

namespace Core.Architecture.vCPU.Assembler.Rules
{
    public class PlusOperatorRule : BaseRule
    {
        protected override List<Token> _GetPattern()
        {
            return new List<Token>()
            {
                new Token(TokenType.Operator, "+")
            };
        }

        protected override Either<IExpression> _CreateExpression(IEnumerable<Token> Tokens)
        {
            return new PlusOperatorExpression
            {
                Operator = Tokens.First()
            };
        }
    }
}