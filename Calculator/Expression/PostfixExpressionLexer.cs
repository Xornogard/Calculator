using Calculator.Token;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Expression
{
    public class PostfixExpressionLexer
    {
        public List<IToken> AnalyzeExpression(string expression)
        {
            List<IToken> tokens = new List<IToken>();

            string[] tokensExpression = expression.Split(" ");

            foreach (string tokenExpression in tokensExpression)
            {
                if(tokenExpression.TryGetToken(out IToken token) == true)
                {
                    tokens.Add(token);
                }
            }

            return tokens;
        }
    }
}
