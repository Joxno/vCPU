using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Core.Architecture.vCPU.Assembler.Expressions;
using Core.Architecture.vCPU.Assembler.Interface;
using Core.Architecture.vCPU.Assembler.Models;
using Core.Utility;

namespace Core.Architecture.vCPU.Assembler.Rules
{
    public class NumericalLiteralRule : BaseRule
    {
        protected override List<Token> _GetPattern()
        {
            return new List<Token>()
            {
                new Token(TokenType.Literal, null)
            };
        }

        protected override bool _CompareTokens(Token PatternToken, Token InputToken)
        {
            return base._CompareTokens(PatternToken, InputToken) &&
                Regex.IsMatch(InputToken.Text, "[0-9]{1,}");
        }

        protected override Either<IExpression> _CreateExpression(IEnumerable<Token> Tokens)
        {
            return new NumericalLiteralExpression
            {
                Literal = Tokens.First()
            };
        }
    }
}