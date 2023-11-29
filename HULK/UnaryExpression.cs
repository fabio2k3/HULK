using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HULK
{
    public abstract class UnaryExpression : MyExpression
    {
        public UnaryExpression(string value) => this.value = value;
        public abstract override string Evaluate();

        public override string value { get; }

        public override string ToString()
        {
            return value;
        }
    }
}
