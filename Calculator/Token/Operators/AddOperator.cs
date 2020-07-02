using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Token.Operators
{
    public class AddOperator : Operator
    {
        public override string Identifier => "+";
        public override int Priority => 0;

        public override NumberValue Compute(NumberValue lhs, NumberValue rhs) => lhs + rhs;
    }
}
