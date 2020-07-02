using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Token.Functions
{
    public class ExpFunction : Function
    {
        public override string Identifier => "exp";

        public override NumberValue GetValue() => Math.Exp(Argument);
    }
}
