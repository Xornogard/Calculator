using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Token.Operators
{
    public class ModOperator : Operator
    {
        public override string Identifier => "mod";
        public override int Priority => 3;

        public override NumberValue Compute(NumberValue lhs, NumberValue rhs) => lhs % rhs;
    }
}
