using Calculator.Token;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Context
{
    public class CalculatorContext
    {
        public CalculatorExpression Expression { get; } = new CalculatorExpression();
        public CalculatorEntry Entry { get; } = new CalculatorEntry();
    }
}
