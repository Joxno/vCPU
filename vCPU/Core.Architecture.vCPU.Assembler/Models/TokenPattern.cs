namespace Core.Architecture.vCPU.Assembler.Models
{
    public class TokenPattern
    {
        public TokenType Type { get; } = TokenType.None;
        public string Pattern { get; } = "";
        public int Priority { get; } = 0;

        public TokenPattern(TokenType Type, string Pattern, int Priority = 0)
        {
            this.Type = Type;
            this.Pattern = Pattern;
            this.Priority = Priority;
        }
        
    }
}
