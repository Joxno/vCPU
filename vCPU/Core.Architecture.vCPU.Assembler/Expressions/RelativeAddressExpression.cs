using Core.Architecture.vCPU.Assembler.Interface;

namespace Core.Architecture.vCPU.Assembler.Expressions
{
    public class RelativeAddressExpression : IExpression
    {
        public IdentifierExpression Identifier { get; set; } = null;
        public OperatorExpression Operator { get; set; } = null;
        public NumericalLiteralExpression Literal { get; set; } = null;
    }
}