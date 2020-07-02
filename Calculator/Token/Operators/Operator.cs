using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Token.Operators
{
    public abstract class Operator : Token
    {
        public abstract int Priority { get;}

        public abstract NumberValue Compute(NumberValue lhs, NumberValue rhs);
    }
}
