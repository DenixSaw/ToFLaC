namespace ToFLaC.Model.Tokens
{
    public class Token
    {
        public string Value { get; set; }
        public TokenType Type { get; set; }
        public int Line { get; set; }
        public int LIndex { get; set; }

        public Token(int line, int lIndex, TokenType type, string value)
        {
            Line = line;
            LIndex = lIndex;
            Type = type;
            Value = value;
        }
    }
}
