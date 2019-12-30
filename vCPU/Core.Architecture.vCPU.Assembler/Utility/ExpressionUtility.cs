using Core.Architecture.vCPU.Assembler.Interface;

namespace Core.Architecture.vCPU.Assembler.Utility
{
    public static class ExpressionUtility
    {
        public static T ToType<T>(this IExpression Expression)
        {
            return (T) Expression;
        }
    }
}