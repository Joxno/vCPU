using Core.Architecture.vCPU.Assembler.Interface;
using Core.Architecture.vCPU.Assembler.Models;

namespace Core.Architecture.vCPU.Assembler.Expressions
{
    public class SeparatorExpression : IExpression
    {
        public Token Separator { get; set; } = null;
    }
}