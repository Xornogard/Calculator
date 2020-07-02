using Calculator.Token;
using Calculator.Token.Functions;
using Calculator.Token.Operators;
using System;
using System.Collections.Generic;

namespace Calculator.Expression
{
    public class PostfixExpressionInterpreter
    {
        public double Compute(Queue<IToken> tokensStack)
        {
            double result = 0.0;
            Stack<IToken> stack = new Stack<IToken>();

            foreach (IToken item in tokensStack)
            {
                switch (item)
                {
                    case NumberValue numberValue:
                        stack.Push(numberValue);
                        break;
                    case Operator mathOperator:
                        HandleOperator(stack, mathOperator);
                        break;
                    case Function function:
                        stack.Push(function.GetValue());
                        break;
                    default:
                        break;
                }
            }

            try
            {
                result = (NumberValue)stack.Pop();
            }
            catch 
            {
                    
            }

            return result;
        }


        private void HandleOperator(Stack<IToken> tokenStack, Operator mathOperator)
        {
            NumberValue? rhs = tokenStack.Pop() as NumberValue?;
            NumberValue? lhs = tokenStack.Pop() as NumberValue?;

            if(lhs != null && rhs != null)
            {
                tokenStack.Push(mathOperator.Compute((NumberValue)lhs, (NumberValue)rhs));
            }
        }

        private void HandleFunction(Stack<IToken> tokensStack, Function mathFunction)
        {

        }
    }
}
