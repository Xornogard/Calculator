using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Token.Functions
{
    public class CtgFunction : Function
    {
        public override string Identifier => "ctg";

        public override NumberValue GetValue() => 1.0/Math.Tan(Argument);
    }
}
