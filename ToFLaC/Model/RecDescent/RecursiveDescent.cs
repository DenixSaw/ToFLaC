using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToFLaC.Model.Tokens;

namespace ToFLaC.Model.RecDescent
{
    public class RecursiveDescent
    {
        public List<Token> Tokens { get; set; }
        public List<string> ParseSequence { get; set; }
        public int Index { get; set; }

        public RecursiveDescent(List<Token> tokens)
        {
            Tokens = tokens;
            ParseSequence = new List<string>();
        }

        public void Start()
        {
            Index = 0;
            E();
        }

        private void E()
        {
            ParseSequence.Add("E");
            T();
            A();
        }

        private void T()
        {
            ParseSequence.Add("T");
            O();
            B();
        }

        private void A()
        {
            ParseSequence.Add("A");
            if (Index < Tokens.Count && (Tokens[Index].Type == TokenType.Plus || Tokens[Index].Type == TokenType.Minus))
            {
                if (Tokens[Index].Type == TokenType.Plus)
                    ParseSequence.Add("+");
                else
                    ParseSequence.Add("-");

                Index++;
                T();
                A();
            }
            else
            {
                ParseSequence.Add("ε");
            }
        }

        private void O()
        {
            ParseSequence.Add("O");
            if (Index >= Tokens.Count) // Нет Id или Числа на конце строки.
            {
                ParseSequence.Add("ERROR");
                return;
            }

            if (Tokens[Index].Type == TokenType.OpenBracket)
            {
                ParseSequence.Add("(");
                Index++;
                E();

                if (Index >= Tokens.Count) // Нет ')' на строки.
                {
                    ParseSequence.Add("ERROR");
                    return;
                }
                if (Tokens[Index].Type != TokenType.CloseBracket) // Вместо ')' неожиданный токен.
                {
                    ParseSequence.Add("ERROR");
                    return;
                }
                else
                {
                    ParseSequence.Add(")");
                    Index++;
                    return;
                }
            }

            if (Index < Tokens.Count && (Tokens[Index].Type != TokenType.Number && Tokens[Index].Type != TokenType.Id)) // Не Id и не число.
            {
                ParseSequence.Add("ERROR");
                return;
            }

            if (Tokens[Index].Type == TokenType.Number)
            {
                ParseSequence.Add("num");
            }
            else if (Tokens[Index].Type == TokenType.Id)
            {
                ParseSequence.Add("id");
            }
            Index++;
        }

        private void B()
        {
            ParseSequence.Add("B");
            if (Index < Tokens.Count && (Tokens[Index].Type == TokenType.Multiply || Tokens[Index].Type == TokenType.Divide))
            {
                if (Tokens[Index].Type == TokenType.Multiply)
                    ParseSequence.Add("*");
                else
                    ParseSequence.Add("/");

                Index++;
                O();
                B();
            }
            else
            {
                ParseSequence.Add("ε");
            }
        }
    }
}
