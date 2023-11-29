using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HULK
{
    public class Token
    {
        public Token(string value, TokenType type)
        {
            this.value = value;
            this.type = type;
        }

        public enum TokenType 
        {
            Number,
            Sum,
            Resta,
            Product,
            Div,
            Pow,
            OpenParenthesis,
            CloseParenthesis,
            KeyWord,
            ID,
            Igual,
            Text,
            PredFunction,
            Semicolon,
            Sin,
            Cos,
            Tan,
            Cot,
            Sqrt,
            Log,
            Print,
            If,
            Else,
            Comparar,
            Negation,
            Different,
            And,
            Or,
            Resto,
            Bool,
            MayorQ,
            Arrow,
            MenorQ,
            VarDeclarationKeyWord,
            VarInKeyWord,
            FunDeclarationKeyWord,
            Concat,
            Comma
        }

        //Propiedad de solo Lectura y almacena la cadena de texto del Token
        public string value { get; }
        //Propiedad de solo lectura y almacena el tipo de Token
        public TokenType type { get; }
    }
}
