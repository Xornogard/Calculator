using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Token
{
    public interface IToken
    {
        public abstract string Identifier { get; }
    }
}
