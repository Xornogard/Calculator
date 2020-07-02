using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Token.Functions
{
    public class SinFunction : Function
    {
        public override string Identifier => "sin";

        public override NumberValue GetValue() => Math.Sin(Argument);
    }
}
