using Core.Architecture.vCPU.Assembler.Expressions;
using Core.Architecture.vCPU.Assembler.Interface;
using Core.Architecture.vCPU.Assembler.Models;
using Core.Architecture.vCPU.Assembler.Utility;
using FluentAssertions;
using NUnit.Framework;

namespace vCPUAssemblerTests
{
    [TestFixture]
    public class UtilityIExpressionExtensionsTests
    {
        [Test]
        public void CastFromExpressionToType()
        {
            IExpression t_Expression = new OperatorExpression { Operator = new Token(TokenType.Operator, "-")};
            var t_Operator = t_Expression.ToType<OperatorExpression>();

            t_Operator.Should().BeOfType<OperatorExpression>();
            t_Operator.Operator.Text.Should().Be("-");
            t_Operator.Operator.Type.Should().Be(TokenType.Operator);
        }
    }
}