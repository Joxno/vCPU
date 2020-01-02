using Core.Architecture.vCPU.Assembler.Interface;

namespace Core.Architecture.vCPU.Assembler.Expressions
{
    public class ConstAddressExpression : IExpression
    {
        public NumericalLiteralExpression Literal { get; set; }
    }
}