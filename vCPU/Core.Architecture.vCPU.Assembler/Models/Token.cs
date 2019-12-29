using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Architecture.vCPU.Assembler.Models
{
    public class Token
    {
        public TokenType Type { get; } = TokenType.None;
        public string Text { get; } = "";

        public Token(TokenType Type, string Text)
        {
            this.Type = Type;
            this.Text = Text;
        }
    }
}
