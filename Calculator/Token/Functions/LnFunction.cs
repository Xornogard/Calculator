using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Token.Functions
{
    public class LnFunction : Function
    {
        public override string Identifier => "ln";

        public override NumberValue GetValue() => Math.Log(Argument);
    }
}
