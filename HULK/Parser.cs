using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HULK
{
    public class Parser
    {
        private int current;
        private List<Token> tokens;
        public MyExpression tree;
        private Context context;

        public Parser(List<Token> tokens, Context? context = null)
        {

            context ??= new Context();
            this.context = context;
            this.tokens = tokens;
            current = 0;
            if (tokens[current].type == Token.TokenType.FunDeclarationKeyWord)
            {
                current++;
                ParseFunDeclaration();
            }
            else
                tree = Parse();
        }

        public string Evaluate()
        {
            return tree.Evaluate();
        }
        MyExpression Parse()
        {
            if (tokens[current].type == Token.TokenType.VarDeclarationKeyWord)
            {
                return ParseVarDeclaration();
            }

            if (tokens[current].type == Token.TokenType.If)
            {
                current++;
                return ParseIfDeclaration();
            }


            MyExpression e = ParseAndOr();
            return e;
        }

        private void ParseFunDeclaration()
        {
            string funName;
            List<Token> funBody;
            List<string> funParams = new List<string>();

            // Verificar si el token es de tipo ID
            if (tokens[current].type == Token.TokenType.ID)
                funName = tokens[current].value;
            else
                throw new SyntaxException("FuncName expected after " + tokens[current - 1] + " in function declaration");
            // Verificar si el token siguiente es "("
            if (tokens[++current].type == Token.TokenType.OpenParenthesis)
            {
                funParams = GetFunDeclParams();
            }
            else
                throw new SyntaxException("Open Parenthesis expected after " + tokens[current - 1] + " in function declaration");

            // Verificar si el siguiente token es "=>"
            if (tokens[++current].type != Token.TokenType.Arrow)
                throw new SyntaxException("'=>' expected after " + tokens[current - 1] + " in function declaration");

            current++;
            funBody = new List<Token>();
            // Agregar los tokens a la lista de "funBody"
            while (tokens[current].type != Token.TokenType.Semicolon)
            {
                funBody.Add(tokens[current++]);
            }
            // Agregar una nueva función 
            UserFunction.AddFunction(new UserFunction(funName, funParams, funBody));

        }

        private List<string> GetFunDeclParams()
        {
            List<string> varNames = new List<string>();
            current++; 

            /*hequear los token hasta que sea de tipo ")"
                mientras no lo sea se van agregando a la lista "varNames" */
            while (tokens[current].type != Token.TokenType.CloseParenthesis)
            {
                varNames.Add(tokens[current].value);
                current++;
                if (tokens[current].type != Token.TokenType.Comma && tokens[current].type != Token.TokenType.CloseParenthesis)
                    throw new SyntaxException("Missing Comma Function Params");
            }

            return varNames;

        }

        private MyExpression ParseIfDeclaration()
        {
            if (tokens[current].type == Token.TokenType.OpenParenthesis)
            {
                string coditionResult = SolveParenthesis().Evaluate();  // Se resuelve la operación dentro del paréntesis y el valor se almacena en "coditionResult"
                
                List<Token> t1 = new List<Token>(); // lista para almacenar la expresión que se evaluará

                /* Si el resultado de la expresión es verdadera se añaden los tokens a una lista
                  y se verifica si se encuentra la palabra "else" */
                if (coditionResult.ToLower() == "true")
                {
                    while (tokens[current].type != Token.TokenType.Else)
                    {
                        t1.Add(tokens[current++]);
                        if (current == tokens.Count) throw new SyntaxException("Missing 'else' KeyWord on if-else expression");
                    }

                    current++;
                    return new Parser(t1, context).tree;
                }
                while (tokens[current].type != Token.TokenType.Else)
                {
                    current++;
                }

                current++;
                t1.Clear();
                /* se agregan los tokens restantes a t1. 
                    Luego, construye y devuelve el árbol de análisis sintáctico a partir de t1 */
                while (current < tokens.Count)
                {
                    t1.Add(tokens[current++]);
                }

                return new Parser(t1, context).tree;
            }

            throw new SyntaxException("If");

        }

        // se encarga del análisis y declaración de variables
        private MyExpression ParseVarDeclaration()
        {
            while (tokens[current].type != Token.TokenType.VarInKeyWord)
            {
                string varName;
                MyExpression varValue;
                if (tokens[++current].type == Token.TokenType.ID)
                    varName = tokens[current].value;
                else
                    throw new SyntaxException("VarName expected after " + tokens[current - 1] + " in let-in expression");
                if (tokens[++current].type != Token.TokenType.Igual)
                    throw new SyntaxException("'=' expected after " + tokens[current - 1] + " in let-in expression");

                current++;
                List<Token> t1 = new List<Token>();
                while (tokens[current].type != Token.TokenType.VarInKeyWord)
                {
                    t1.Add(tokens[current++]);
                    if (current == tokens.Count)
                        throw new SyntaxException("Missing 'in' KeyWord on let-in expression");

                    // asignar valores 
                    if (tokens[current].type == Token.TokenType.Comma || tokens[current].type == Token.TokenType.VarInKeyWord)
                    {
                        varValue = new Parser(t1, context).tree;
                        context.AddVar(new Variable(varName, varValue));
                        break;
                    }
                }
            }
            current++;
            List<Token> t2 = new List<Token>();
            while (current < tokens.Count)
                t2.Add(tokens[current++]);
            return new Parser(t2, context).tree;
        }

        /* evaluar expresiones que contengan los operadores
          "and" y "or" */
        private MyExpression ParseAndOr()
        {
            MyExpression left = Compare();

            while (current < tokens.Count && (
                      tokens[current].type == Token.TokenType.And ||
                      tokens[current].type == Token.TokenType.Or)
                 )
            {
                Token.TokenType currentOp = tokens[current].type;
                current++;
                MyExpression right = Compare();
                switch (currentOp)
                {
                    case Token.TokenType.And:
                        left = new And(left, right);
                        break;
                    case Token.TokenType.Or:
                        left = new Or(left, right);
                        break;
                }
            }
            return left;
        }

        // parte del código encargada de realizar la comparación de expresiones 
        private MyExpression Compare()
        {
            MyExpression left = ParseSum();

            while (current < tokens.Count && (
                      tokens[current].type == Token.TokenType.Comparar ||
                      tokens[current].type == Token.TokenType.Different ||
                      tokens[current].type == Token.TokenType.MayorQ ||
                      tokens[current].type == Token.TokenType.MenorQ
                 ))
            {
                Token.TokenType currentOp = tokens[current].type;
                current++;
                MyExpression right = ParseSum();
                switch (currentOp)
                {
                    case Token.TokenType.Comparar:
                        left = new Compare(left, right);
                        break;
                    case Token.TokenType.Different:
                        left = new Different(left, right);
                        break;
                    case Token.TokenType.MayorQ:
                        left = new MayorQ(left, right);
                        break;
                    case Token.TokenType.MenorQ:
                        left = new MenorQ(left, right);
                        break;
                }
            }
            return left;
        }

        // realiza el análisis de las expresiones que contengan las operaciones: suma y resta
        private MyExpression ParseSum()
        {
            MyExpression left = Concat();

            while (current < tokens.Count && (tokens[current].type == Token.TokenType.Sum || tokens[current].type == Token.TokenType.Resta))
            {
                Token.TokenType currentOp = tokens[current].type;
                current++;
                MyExpression right = Concat();
                if (currentOp == Token.TokenType.Sum)
                    left = new Addition(left, right);
                else
                    left = new Subtraction(left, right);
            }
            return left;
        }

        // realiza el análisis sintáctico de una expresión matemática
        private MyExpression Concat()
        {
            MyExpression left = ParseMult();

            while (current < tokens.Count && tokens[current].type == Token.TokenType.Concat)
            {
                Token.TokenType currentOp = tokens[current].type;
                current++;
                MyExpression right = ParseMult();
                if (currentOp == Token.TokenType.Concat)
                    left = new Concat(left, right);
            }
            return left;
        }


        /* realiza el análisis de las expresiones que contengan las operaciones 
            multiplicación, división, resto */
        private MyExpression ParseMult()
        {
            MyExpression left = ParsePow();
            while (current < tokens.Count && (
                      tokens[current].type == Token.TokenType.Product ||
                      tokens[current].type == Token.TokenType.Div ||
                      tokens[current].type == Token.TokenType.Resto
                      ))
            {
                Token.TokenType currentOp = tokens[current].type;
                current++;
                MyExpression right = ParsePow();
                if (currentOp == Token.TokenType.Product)
                    left = new Product(left, right);
                else if (currentOp == Token.TokenType.Div)
                    left = new Division(left, right);
                else
                    left = new Resto(left, right);
            }

            return left;
        }

        // procesa los tokens de una opeación matematica que contenga la operación de potenciación
        private MyExpression ParsePow()
        {
            MyExpression left = ParseTerm();
            while (current < tokens.Count && tokens[current].type == Token.TokenType.Pow)
            {
                Token.TokenType currentOp = tokens[current].type;
                current++;
                MyExpression right = ParseTerm();
                if (currentOp == Token.TokenType.Pow)
                    left = new Power(left, right);
            }

            return left;
        }

        private MyExpression ParseTerm()
        {
            // comprobar el tipo actual del token y realiza una acción según el mismo
            switch (tokens[current].type)
            {
                case Token.TokenType.Number:
                    return new Number(tokens[current++].value);
                case Token.TokenType.Text:
                    return new Text(tokens[current++].value);
                case Token.TokenType.Bool:
                    return new Bool(tokens[current++].value);
                case Token.TokenType.Sin:
                    return new Sin(GetParams());
                case Token.TokenType.Cos:
                    return new Cos(GetParams());
                case Token.TokenType.Log:
                    return new Log(GetParams());
                case Token.TokenType.Sqrt:
                    return new Sqrt(GetParams());
                case Token.TokenType.Print:
                    return new Print(GetParams());
                case Token.TokenType.OpenParenthesis:
                    return SolveParenthesis();
                case Token.TokenType.Resta:
                    current++;
                    return new Number((-double.Parse(ParseTerm().Evaluate())).ToString());
                case Token.TokenType.Negation:
                    current++;
                    return new Bool((!bool.Parse(ParseTerm().Evaluate())).ToString());

            }

            // comprobar si es una variable
            Variable? v = context.FindVar(tokens[current].value);
            if (v != null)
            {
                current++;
                return v.VarTree;
            }
            // comprobar si es una función
            UserFunction? f = UserFunction.FindFunction(tokens[current].value);
            if (f != null)
            {
                Context c = new Context();
                List<MyExpression> list = GetParams();
                for (int i = 0; i < list.Count; i++)
                {
                    string vName = f.ParamNames[i];
                    MyExpression vTree = list[i];

                    c.AddVar(new Variable(vName, vTree));
                }

                current--;
                return new Parser(f.FunBody, c).tree;
            }
            throw new SyntaxException("Invalid Expression '" + tokens[current].value + "'");
        }


        // obtener todas las expresiones de parámetros entre paréntesis en una lista de tokens 
        private List<MyExpression> GetParams()
        {
            List<MyExpression> paramExpressions = new List<MyExpression>(); // almacenar expresiones de los parámetros
            List<Token> paramTokens = new List<Token>(); // almacenar los tokens de los paámetros
            if (tokens[++current].type == Token.TokenType.OpenParenthesis)
            {
                int parentCount = 1;
                current++;
                while (parentCount != 0)
                {
                    if (tokens[current].type == Token.TokenType.OpenParenthesis)
                        parentCount++;
                    else if (tokens[current].type == Token.TokenType.CloseParenthesis)
                        parentCount--;
                    paramTokens.Add(tokens[current++]);
                    if (parentCount == 1 && tokens[current].type == Token.TokenType.Comma)
                    {
                        paramExpressions.Add(new Parser(paramTokens, context).tree);
                        paramTokens.Clear();
                        current++;
                    }
                }

                paramTokens.Remove(paramTokens.Last()); 
                paramExpressions.Add(new Parser(paramTokens, context).tree);
                current++;
                return paramExpressions;
            }
            throw new SyntaxException("Missing Parenthesis after function Declaration");
        }

        // resolver expresiones dentro de pararéntesis
        private MyExpression SolveParenthesis()
        {
            List<Token> paramTokens = new List<Token>();
            int parentCount = 1;
            current++;
            while (parentCount != 0)
            {
                if (tokens[current].type == Token.TokenType.OpenParenthesis)
                    parentCount++;
                else if (tokens[current].type == Token.TokenType.CloseParenthesis)
                    parentCount--;
                if (parentCount == 0) break;
                paramTokens.Add(tokens[current++]);
            }

            current++;
            return new Parser(paramTokens, context).tree;
        }
    }
}
