using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HULK
{
    abstract class MyFunction : MyExpression
    {

        public override string value
        {
            get;
        }

        public MyFunction(string value) => this.value = value;

        public abstract override string Evaluate();
    }
}
