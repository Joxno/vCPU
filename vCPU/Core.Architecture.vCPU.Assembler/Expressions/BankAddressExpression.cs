using Core.Architecture.vCPU.Assembler.Interface;
using Core.Architecture.vCPU.Assembler.Models;

namespace Core.Architecture.vCPU.Assembler.Expressions
{
    public class BankAddressExpression : IExpression
    {
        public Token BankAddress { get; set; } = null;
    }
}
