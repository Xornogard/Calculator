using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Token.Functions
{
    public class AbsFunction : Function
    {
        public override string Identifier => "abs";

        public override NumberValue GetValue() => Math.Abs(Argument);
    }
}
