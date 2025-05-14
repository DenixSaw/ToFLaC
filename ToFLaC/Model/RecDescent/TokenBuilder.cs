using System.Text.RegularExpressions;
using ToFLaC.Model.Tokens;

namespace ToFLaC.Model.RecDescent
{
    public class TokenBuilder
    {
        public static List<Token> FindTokens(string text)
        {
            Regex regex = new(@"\d+|[a-zA-Z]+|\+|\-|\*|\/|\(|\)|[^a-zA-Z0-9]");
            List<Token> tokens = new();
            int line = 1;
            int index = 0;

            MatchCollection matches = regex.Matches(text);
            if(matches.Count > 0)
            {
                foreach(Match match in matches)
                {
                    if (int.TryParse(match.Value, out int number))
                    {
                        tokens.Add(new(line, index, TokenType.Number, match.Value));
                        index += match.Value.Length;
                    }
                    else if (Regex.IsMatch(match.Value, @"^[a-zA-Z]+$"))
                    {
                        tokens.Add(new(line, index, TokenType.Id, match.Value));
                        index += match.Value.Length;
                    }
                    else if (match.Value == "+")
                    {
                        tokens.Add(new(line, index, TokenType.Plus, match.Value));
                        index += match.Value.Length;
                    }
                    else if (match.Value == "-")
                    {
                        tokens.Add(new(line, index, TokenType.Minus, match.Value));
                        index += match.Value.Length;
                    }
                    else if (match.Value == "*")
                    {
                        tokens.Add(new(line, index, TokenType.Multiply, match.Value));
                        index += match.Value.Length;
                    }
                    else if (match.Value == "/")
                    {
                        tokens.Add(new(line, index, TokenType.Divide, match.Value));
                        index += match.Value.Length;
                    }
                    else if (match.Value == "(")
                    {
                        tokens.Add(new(line, index, TokenType.OpenBracket, match.Value));
                        index += match.Value.Length;
                    }
                    else if (match.Value == ")")
                    {
                        tokens.Add(new(line, index, TokenType.CloseBracket, match.Value));
                        index += match.Value.Length;
                    }
                    else if (match.Value == "\n")
                    {
                        line++;
                        index = 0;
                    }
                    else if (match.Value == "\r")
                    {
                        continue;
                    }
                    else if (string.IsNullOrWhiteSpace(match.Value))
                    {
                        index += match.Value.Length;
                    }
                    else
                    {
                        tokens.Add(new(line, index, TokenType.Invalid, match.Value));
                    }
                }
            }

            return tokens;
        }
    }
}
