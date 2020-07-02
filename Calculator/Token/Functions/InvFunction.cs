using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Token.Functions
{
    public class InvFunction : Function
    {
        public override string Identifier => "inv";

        public override NumberValue GetValue() => 1.0 / Argument;
    }
}
