using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Core.Architecture.vCPU.Assembler.Interface;
using Core.Architecture.vCPU.Assembler.Models;
using Core.Utility;

namespace Core.Architecture.vCPU.Assembler.Services
{
    public class RegexTokenizer : ITokenizer
    {
        private readonly IEnumerable<TokenPattern> m_Patterns = null;

        public RegexTokenizer(IEnumerable<TokenPattern> Patterns)
        {
            m_Patterns = Patterns;
        }

        public Either<IEnumerable<Token>> GenerateTokens(string Text)
        {
            var t_Tokens = new List<Token>();
            var t_CurrentText = Text;
            while (t_CurrentText.Length != 0)
            {
                var t_TokenAdded = false;
                var t_Groups = _GetPatternsGroupedByPriority();

                foreach (var t_Group in t_Groups)
                {
                    var t_Patterns = t_Group
                        .Select(G => G);

                    var t_Token = _PatternMatch(0, t_CurrentText.Length, t_CurrentText, t_Patterns);

                    if (!t_Token.HasError())
                    {
                        t_CurrentText = t_CurrentText
                            .Substring(t_Token.Value.Text.Length)
                            .TrimStart();

                        t_TokenAdded = true;
                        t_Tokens.Add(t_Token.Value);
                        break;
                    }
                }

                if (!t_TokenAdded)
                    return new Exception($"Unable to tokenize text: {t_CurrentText}");
            }

            return t_Tokens;
        }

        private IEnumerable<IGrouping<int, TokenPattern>> _GetPatternsGroupedByPriority()
        {
            return m_Patterns
                .GroupBy(P => P.Priority);
        }

        private Either<Token> _PatternMatch(int Start, int End, string Text, IEnumerable<TokenPattern> Patterns)
        {
            for (int i = Start; i <= End; i++)
            {
                var t_TempText = Text.Substring(0, i);
                if (_HasMatchCount(t_TempText, Patterns) == 1)
                {
                    var t_Pattern = _GetMatch(t_TempText, Patterns);
                    return _GenerateToken(t_Pattern, t_TempText);
                }
            }

            return new Exception("No Pattern Matched");
        }

        private int _HasMatchCount(string Text, IEnumerable<TokenPattern> Patterns)
        {
            return Patterns
                .Select(P => Regex.IsMatch(Text, P.Pattern))
                .Count(R => R);
        }

        private TokenPattern _GetMatch(string Text, IEnumerable<TokenPattern> Patterns)
        {
            return Patterns
                .FirstOrDefault(P => Regex.IsMatch(Text, P.Pattern));
        }

        private Token _GenerateToken(TokenPattern Pattern, string Text)
        {
            return new Token(Pattern.Type, Text);
        }
    }
}
