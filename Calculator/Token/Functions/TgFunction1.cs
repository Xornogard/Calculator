using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Token.Functions
{
    public class TgFunction : Function
    {
        public override string Identifier => "tg";

        public override NumberValue GetValue() => Math.Tan(Argument);
    }
}
