using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Token.Operators
{
    public class DivisionOperator : Operator
    {
        public override string Identifier => "x";
        public override int Priority => 1;

        public override NumberValue Compute(NumberValue lhs, NumberValue rhs) => lhs / rhs;
    }
}
