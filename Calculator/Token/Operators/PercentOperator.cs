using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Token.Operators
{
    public class PercentOperator : Operator
    {
        public override string Identifier => "%";
        public override int Priority => 2;

        public override NumberValue Compute(NumberValue lhs, NumberValue rhs) => lhs * 100.0 / rhs; 
    }
}
