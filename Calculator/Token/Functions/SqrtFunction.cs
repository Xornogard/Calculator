using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Token.Functions
{
    public class SqrtFunction : Function
    {
        public override string Identifier => "sqrt";

        public override NumberValue GetValue() => Math.Sqrt(Argument);
    }
}
