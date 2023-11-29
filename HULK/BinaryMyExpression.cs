using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HULK
{
    public abstract class BinaryMyExpression : MyExpression
    {
        // Valores de las expresiones Izquierda y Derecha respectivamente
        public MyExpression LeftMyExpression { get; }
        public MyExpression RightMyExpression { get; }
        // DEvuelve el valor de la expresion
        public abstract override string value { get; }

        public BinaryMyExpression(MyExpression left, MyExpression right)
        {
            LeftMyExpression = left;
            RightMyExpression = right;
        }

        public override string ToString()
        {
            return LeftMyExpression + " " + value + " " + RightMyExpression;
        }

        public abstract override string Evaluate();

    }
}
