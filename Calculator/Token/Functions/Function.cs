using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Token.Functions
{
    public abstract class Function : Token
    {
        public virtual NumberValue Argument { get; set; }

        public abstract NumberValue GetValue();

        public override string ToString()
        {
            return $"{Identifier}({Argument})";
        }
    }
}
