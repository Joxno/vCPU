using Core.Architecture.vCPU.Assembler.Interface;
using Core.Architecture.vCPU.Assembler.Models;

namespace Core.Architecture.vCPU.Assembler.Expressions
{
    public class OperatorExpression : IExpression
    { 
        public Token Operator { get; set; } = null;
    }
}