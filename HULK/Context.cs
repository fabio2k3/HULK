using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HULK
{
    public class Context
    {
        //ALmacenar objetos de tipo Varaible
        public List<Variable> myVars;

        public Context()
        {
            myVars = new List<Variable>();
            myVars.Add(new Variable("PI", new Number(double.Pi.ToString())));
        }

        //Verificar si la variable ya existe, sino agregarla a la lista myVars
        public void AddVar(Variable v)
        {
            if (FindVar(v.Name) == null)
                myVars.Add(v);
            else
                throw new SyntaxException("La variable " + v.Name + " ya existe en el contexto actual.");
        }

        //Se utilza para buscar una variable por su nombre
        public Variable? FindVar(string varName)
        {
            return myVars.Find(x => x.Name == varName);

        }
    }
}
