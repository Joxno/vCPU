using Core.Architecture.vCPU.Assembler.Interface;
using Core.Architecture.vCPU.Assembler.Models;

namespace Core.Architecture.vCPU.Assembler.Expressions
{
    public class KeywordExpression : IExpression
    {
        public Token Keyword { get; set; } = null;
    }
}