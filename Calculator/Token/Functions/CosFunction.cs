using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Token.Functions
{
    public class CosFunction : Function
    {
        public override string Identifier => "cos";

        public override NumberValue GetValue() => Math.Cos(Argument);
    }
}
