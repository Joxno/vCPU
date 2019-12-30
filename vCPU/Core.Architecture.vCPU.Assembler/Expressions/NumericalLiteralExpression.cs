using System;
using System.Collections.Generic;
using System.Text;
using Core.Architecture.vCPU.Assembler.Interface;
using Core.Architecture.vCPU.Assembler.Models;

namespace Core.Architecture.vCPU.Assembler.Expressions
{
    public class NumericalLiteralExpression : IExpression
    {
        public Token Literal { get; set; } = null;
    }
}
