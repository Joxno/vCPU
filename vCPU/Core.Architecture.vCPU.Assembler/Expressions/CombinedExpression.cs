using System.Collections;
using System.Collections.Generic;
using Core.Architecture.vCPU.Assembler.Interface;

namespace Core.Architecture.vCPU.Assembler.Expressions
{
    public class CombinedExpression : IExpression
    {
        public IEnumerable<IExpression> Expressions { get; set; } = null;
    }
}