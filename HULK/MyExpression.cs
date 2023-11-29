using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HULK
{
    public abstract class MyExpression
    {
        //Proporciona la funcionalidad para evaluar la expresion y devolver el resultado
        public abstract string Evaluate();

        //Proporciona el valor de la expresión
        public abstract string value { get; }

        //Representa las expresiones en forma de string
        public abstract override string ToString();
    }
}
