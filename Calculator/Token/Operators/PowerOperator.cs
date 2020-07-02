using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Token.Operators
{
    public class PowerOperator : Operator
    {
        public override string Identifier => "^";
        public override int Priority => 2;

        public override NumberValue Compute(NumberValue lhs, NumberValue rhs) => Math.Pow(lhs, rhs);
    }
}
