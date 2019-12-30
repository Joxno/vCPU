using Core.Architecture.vCPU.Assembler.Interface;
using Core.Architecture.vCPU.Assembler.Models;

namespace Core.Architecture.vCPU.Assembler.Expressions
{
    public class IdentifierExpression : IExpression
    {
        public Token Identifier { get; set; } = null;
    }
}
