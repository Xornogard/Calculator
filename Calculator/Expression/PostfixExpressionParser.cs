using Calculator.Exceptions;
using Calculator.Token;
using Calculator.Token.Functions;
using Calculator.Token.Operators;
using Calculator.Token.Parenthesis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Expression
{
    public class PostfixExpressionParser
    {

        public Queue<IToken> ParseTokens(List<IToken> tokens)
        {
            Queue<IToken> queue = new Queue<IToken>();
            Queue<IToken> output = new Queue<IToken>();

            for (int i = 0; i < tokens.Count; i++)
            {
                if(i + 1 < tokens.Count)
                {
                    ValidateTokens(tokens[i], tokens[i + 1]);
                }

                ProcessToken(tokens[i], queue, output);
            }

            while(queue.Count > 0)
            {
                if(queue.Peek() is OpenParenthesis)
                {
                    throw new Exception("There is a closing parenthesis missing");
                }
                output.Enqueue(queue.Dequeue());
            }

            return output;
        }

        private void ValidateTokens(IToken currentToken, IToken nextToken)
        {
            if (currentToken is NumberValue && nextToken is NumberValue)
                throw new Exception();

            if (currentToken is Operator && nextToken is Operator)
                throw new Exception();
        }

        private void ProcessToken(IToken token, Queue<IToken> queue, Queue<IToken> output)
        {
            switch (token)
            {
                case NumberValue number:
                    output.Enqueue(number);
                    break;
                case Function function:
                    output.Enqueue(function);
                    break;
                case Operator mathOperator:
                    while(queue.Count > 0 && mathOperator.Priority <= (queue.Peek() is OpenParenthesis ? 0 : (queue.Peek() as Operator).Priority))
                    {
                        output.Enqueue(queue.Dequeue());
                    }

                    queue.Enqueue(mathOperator);
                    break;
                case OpenParenthesis openParenthesis:
                    queue.Enqueue(openParenthesis);
                    break;
                case ClosedParenthesis closedParenthesis:
                    while(queue.Count > 0 && queue.Peek() is OpenParenthesis == false)
                    {
                        output.Enqueue(queue.Dequeue());
                    }
                    break;
                default:
                    throw new NotTokenException();
            }
        }
    }
}
