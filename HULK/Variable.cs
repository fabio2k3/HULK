using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HULK
{
    public class Variable
    {
        //Guarda el nombre de la variable
        public string Name;
        //Guarda una arbol de expresion que representa la variable
        public MyExpression VarTree;

        public Variable(string name, MyExpression varTree)
        {
            Name = name;
            VarTree = varTree;
        }

    }

}
