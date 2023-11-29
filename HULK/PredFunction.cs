using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HULK
{
    public abstract class PredFunction : MyExpression
    {
        // Lista de argumentos
        public List<MyExpression> Args;
        // Cantidad de argumentos que la función debe recibir 
        public abstract int CantArgs { get; }
        public PredFunction(string value, List<MyExpression> args)
        {
            if (args.Count != CantArgs)
                throw new SyntaxException("Invalid Arguments at " + value + " function");

            this.value = value;
            Args = args;
        }
        public abstract override string Evaluate();

        public override string value { get; }
    }
}
