using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Core.Architecture.vCPU.Assembler.Expressions;
using Core.Architecture.vCPU.Assembler.Models;

namespace Core.Architecture.vCPU.Assembler.Rules
{
    public class NumericalLiteralRule : BaseRule
    {

        public NumericalLiteralRule() : base(new List<Token>
        {
            new Token(TokenType.Literal, null)
        },
        (Tokens) => new NumericalLiteralExpression
        {
            Literal = Tokens.First()
        })
        {
        }

        protected override bool _CompareTokens(Token PatternToken, Token InputToken)
        {
            return base._CompareTokens(PatternToken, InputToken) &&
                Regex.IsMatch(InputToken.Text, "[0-9]{1,}");
        }
    }
}