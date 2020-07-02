using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Token
{
    public struct NumberValue : IToken
    {
        public string Identifier => "number";

        private double _value;

        public NumberValue(string character)
        {
            double.TryParse(character, out _value);
        }

        public NumberValue(double value)
        {
            _value = value;
        }

        public static NumberValue operator +(NumberValue lhs, NumberValue rhs) => lhs._value + rhs._value;
        public static NumberValue operator -(NumberValue lhs, NumberValue rhs) => lhs._value - rhs._value;
        public static NumberValue operator *(NumberValue lhs, NumberValue rhs) => lhs._value * rhs._value;
        public static NumberValue operator /(NumberValue lhs, NumberValue rhs) => lhs._value / rhs._value;
        public static NumberValue operator %(NumberValue lhs, NumberValue rhs) => lhs._value % rhs._value;


        public static implicit operator double(NumberValue value) => value._value;

        public static implicit operator NumberValue(double value) => new NumberValue(value);
    }
}
