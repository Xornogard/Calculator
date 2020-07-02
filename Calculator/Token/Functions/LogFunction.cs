using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Token.Functions
{
    public class LogFunction : Function
    {
        public override string Identifier => "log";

        public override NumberValue GetValue() => Math.Log10(Argument);
    }
}
