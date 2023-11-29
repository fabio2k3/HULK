using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HULK
{
    #region Clases Expresiones Binarias
    class Addition : BinaryMyExpression
    {

        public Addition(MyExpression left, MyExpression right): base(left, right)
        {
        }

        public override string value => "+";
        //Suma los VAlores de las expresiones Izquierda y Derecha
        public override string Evaluate()
        {
            try
            {
                return (double.Parse(LeftMyExpression.Evaluate()) + double.Parse(RightMyExpression.Evaluate())).ToString();
            }
            catch (FormatException)
            {
                throw new SemanticException("No se puede sumar 2 Strings, el operador de concatenacion es '@'.");
            }
        }
    }
    class Subtraction : BinaryMyExpression
    {
        public Subtraction(MyExpression left, MyExpression right)
            : base(left, right)
        {
        }

        public override string value => "-";
        //Realiza la resta de las Expresiones Izquierda y Derecha
        public override string Evaluate()
        {
            try
            {
                return (double.Parse(LeftMyExpression.Evaluate()) - double.Parse(RightMyExpression.Evaluate())).ToString();
            }
            catch (FormatException)
            {
                throw new SemanticException("No se puede restar 2 Strings, el operador de concatenacion es '@'.");
            }
        }
    }

    class Product : BinaryMyExpression
    {
        public Product(MyExpression left, MyExpression right): base(left, right)
        {
        }

        public override string value => "*";
        //Multiplica el resultado de las expresiones Izquierda y Derecha
        public override string Evaluate()
        {
            try
            {
                return (double.Parse(LeftMyExpression.Evaluate()) * double.Parse(RightMyExpression.Evaluate())).ToString();
            }
            catch (FormatException)
            {
                throw new SemanticException("No se puede multiplicar 2 Strings, el operador de concatenacion es '@'.");
            }
        }
    }

    class Division : BinaryMyExpression
    {
        public Division(MyExpression left, MyExpression right): base(left, right)
        {
        }

        public override string value => "/";
        //Retorna el valor de la division de la expresion IZquierda y Derecha
        public override string Evaluate()
        {
            try
            {
                return (double.Parse(LeftMyExpression.Evaluate()) / double.Parse(RightMyExpression.Evaluate())).ToString();
            }
            catch (FormatException)
            {
                throw new SemanticException("No se puede dividir 2 Strings, el operador de concatenacion es '@'.");
            }
        }
    }

    class Power : BinaryMyExpression
    {
        public Power(MyExpression left, MyExpression right): base(left, right)
        {
        }

        public override string value => "^";
        //Devuelve el resultado de la expresion Izquierda elevada a la potencia de la Derecha
        public override string Evaluate()
        {
            try
            {
                return Math.Pow(double.Parse(LeftMyExpression.Evaluate()), double.Parse(RightMyExpression.Evaluate())).ToString();
            }
            catch (FormatException)
            {
                throw new SemanticException(
                    "No se puede realizar la potenciacion a 2 Strings, el operador de concatenacion es '@'.");
            }
        }
    }

    class Resto : BinaryMyExpression
    {
        public Resto(MyExpression left, MyExpression right): base(left, right)
        {
        }

        public override string value => "%";
        // Devuelve el resto de la division entre la Expresion Izquierda y l Derecha
        public override string Evaluate()
        {
            try
            {
                return (double.Parse(LeftMyExpression.Evaluate()) % double.Parse(RightMyExpression.Evaluate())).ToString();
            }
            catch (FormatException)
            {
                throw new SemanticException("No se puede hacer operacion resto entre 2 Strings, el operador de concatenacion es '@'.");
            }
        }
    }
    class And : BinaryMyExpression
    {

        public And(MyExpression left, MyExpression right): base(left, right)
        {
        }

        public override string value => "&";
        // Realiza la Operacion "and" entre el valor de la expresion IZquierda y Derecha
        public override string Evaluate()
        {
            try
            {
                return (bool.Parse(LeftMyExpression.Evaluate()) && bool.Parse(RightMyExpression.Evaluate())).ToString();
            }
            catch (FormatException)
            {
                throw new SemanticException("La operacion " + value + " solo esta concebida para booleans");
            }
        }
    }

    class Or : BinaryMyExpression
    {

        public Or(MyExpression left, MyExpression right): base(left, right)
        {
        }

        public override string value => "|";
        // Realiza la Operacion "or" entre el valor de la expresion IZquierda y Derecha
        public override string Evaluate()
        {
            try
            {
                return (bool.Parse(LeftMyExpression.Evaluate()) || bool.Parse(RightMyExpression.Evaluate())).ToString();
            }
            catch (FormatException)
            {
                throw new SemanticException("La operacion " + value + " solo esta concebida para booleans");
            }
        }
    }

    class MenorQ : BinaryMyExpression
    {

        public MenorQ(MyExpression left, MyExpression right): base(left, right)
        {
        }

        public override string value => "<";
        // Realiza la Operacion "Menor que..." entre el valor de la expresion IZquierda y Derecha
        public override string Evaluate()
        {
            try
            {
                return (double.Parse(LeftMyExpression.Evaluate()) > double.Parse(RightMyExpression.Evaluate())).ToString();
            }
            catch (FormatException)
            {
                throw new SemanticException("La operacion " + value + " solo esta concebida para números");
            }
        }
    }

    class MayorQ : BinaryMyExpression
    {

        public MayorQ(MyExpression left, MyExpression right): base(left, right)
        {
        }

        public override string value => ">";
        // Realiza la Operación "Mayor que..." entre el valor de la expresion IZquierda y Derecha
        public override string Evaluate()
        {
            try
            {
                return (double.Parse(LeftMyExpression.Evaluate()) > double.Parse(RightMyExpression.Evaluate())).ToString();
            }
            catch (FormatException)
            {
                throw new SemanticException("La operacion " + value + " solo esta concebida para números");
            }
        }
    }

    class Different : BinaryMyExpression
    {

        public Different(MyExpression left, MyExpression right): base(left, right)
        {
        }
        // Realiza la Operación "Diferencia" entre el valor de la expresion IZquierda y Derecha
        public override string value => "!=";

        public override string Evaluate()
        {
            return (LeftMyExpression.Evaluate() != RightMyExpression.Evaluate()).ToString();
        }
    }

    class Compare : BinaryMyExpression
    {

        public Compare(MyExpression left, MyExpression right)
            : base(left, right)
        {
        }
        // Realiza la Operación "Igualdad" entre el valor de la expresion IZquierda y Derecha
        public override string value => "==";

        public override string Evaluate()
        {
            return (LeftMyExpression.Evaluate() == RightMyExpression.Evaluate()).ToString();
        }
    }

    public class Concat : BinaryMyExpression
    {

        public Concat(MyExpression left, MyExpression right)
            : base(left, right)
        {
        }

        public override string value => "@";
        // Operador para unir dos cadenas de texto
        public override string Evaluate()
        {
            return LeftMyExpression.Evaluate() + RightMyExpression.Evaluate();
        }
    }
    #endregion

    #region Clases Funciones Predeterminadas
    class Cos : PredFunction
    {
        public Cos(List<MyExpression> args) : base("Cos", args)
        {
        }

        public override int CantArgs => 1;
        //Calcular el coseno despues de Recibir el Valor de la expresion
        public override string Evaluate()
        {
            return Math.Cos(double.Parse(Args[0].Evaluate())).ToString();
        }

        public override string ToString()
        {
            string str = value + "(";
            foreach (MyExpression e in Args)
            {
                str += e + ", ";
            }

            return str.Substring(0, str.Length - 2) + ")";
        }


    }
    class Sin : PredFunction
    {
        public Sin(List<MyExpression> args) : base("Sin", args)
        {
        }

        public override int CantArgs => 1;
        //Calcular el seno despues de Recibir el Valor de la expresion
        public override string Evaluate()
        {
            return Math.Sin(double.Parse(Args[0].Evaluate())).ToString();
        }

        public override string ToString()
        {
            string str = value + "(";
            foreach (MyExpression e in Args)
            {
                str += e + ", ";
            }

            return str.Substring(0, str.Length - 2) + ")";
        }
    }
    class Log : PredFunction
    {
        public Log(List<MyExpression> args) : base("log", args)
        {
        }

        public override int CantArgs => 2;
        //Calcular el logaritmo despues de Recibir el Valor de la expresion
        public override string Evaluate()
        {
            return Math.Log(double.Parse(Args[1].Evaluate()), double.Parse(Args[0].Evaluate())).ToString();
        }

        public override string ToString()
        {
            string str = value + "(";
            foreach (MyExpression e in Args)
            {
                str += e + ", ";
            }

            return str.Substring(0, str.Length - 2) + ")";
        }
    }
    class Sqrt : PredFunction
    {
        public Sqrt(List<MyExpression> args) : base("Sqrt", args)
        {
        }

        public override int CantArgs => 1;
        //Calcular el valor de la raiz
        public override string Evaluate()
        {
            return Math.Sqrt(double.Parse(Args[0].Evaluate())).ToString();
        }

        public override string ToString()
        {
            string str = value + "(";
            foreach (MyExpression e in Args)
            {
                str += e + ", ";
            }

            return str.Substring(0, str.Length - 2) + ")";
        }
    }
    class Print : PredFunction
    {
        public Print(List<MyExpression> args) : base("Print", args)
        {
        }

        public override int CantArgs => 1;

        public override string Evaluate()
        {
            return Args[0].Evaluate();
        }

        public override string ToString()
        {
            string str = value + "(";
            foreach (MyExpression e in Args)
            {
                str += e + ", ";
            }

            return str.Substring(0, str.Length - 2) + ")";
        }
    }
    #endregion

    #region Clases Funciones Unitarias

    public class Bool : UnaryExpression
    {
        public Bool(string value) : base(value) { }
        public override string Evaluate()
        {
            return value;
        }

    }
    public class NegativeNumber : UnaryExpression
    {
        public NegativeNumber(string value) : base(value) { }
        public override string Evaluate()
        {
            return value;
        }

    }

    public class Number : UnaryExpression
    {
        public Number(string value) : base(value) { }
        public override string Evaluate()
        {
            return value;
        }

    }

    public class Text : UnaryExpression
    {
        public Text(string value) : base(value) { }
        public override string Evaluate()
        {
            return value;
        }
    }
    #endregion
}
