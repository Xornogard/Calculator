using Calculator.Exceptions;
using Calculator.Token.Functions;
using Calculator.Token.Operators;
using System.Collections.Generic;
using System.Linq;

namespace Calculator.Token
{
    public static class TokenUtils
    {
        private static List<Operator> Operators { get; } = new List<Operator>()
        {
            new AddOperator(),
            new SubstractOperator(),
            new DivisionOperator(),
            new MultiplicationOperator(),
            new PowerOperator(),
            new ModOperator(),
            new PercentOperator()
        };

        private static List<Function> Functions { get; } = new List<Function>()
        {
            new AbsFunction(),
            new InvFunction(),
            new SinFunction(),
            new CosFunction(),
            new TgFunction(),
            new CtgFunction(),
            new LogFunction(),
            new LnFunction(),
            new ExpFunction(),
            new SqrtFunction()
        };

        private static List<Parenthesis.Parenthesis> Parentheses = new List<Parenthesis.Parenthesis>()
        {
            new Parenthesis.OpenParenthesis(),
            new Parenthesis.ClosedParenthesis()
        };

        private static List<IToken> _tokens;

        private static List<IToken> Tokens
        {
            get
            {
                if(_tokens == null)
                {
                    _tokens = new List<IToken>(GatherTokens());
                }

                return _tokens;
            }
        }

        public static bool TryGetToken(this string tokenExpression, out IToken token)
        {
            try
            {
                if(tokenExpression.IsNumberValue())
                {
                    token = new NumberValue(tokenExpression);
                }
                else if (tokenExpression.IsFunction())
                {
                    token = tokenExpression.GetFunctionToken();
                }
                else if(tokenExpression.IsOperator())
                {
                    token = tokenExpression.GetOperatorToken();
                }
                else
                {
                    token = tokenExpression.GetParenthesisToken();
                }

                return true;
            }
            catch(NotTokenException e)
            {
                token = new NaT();
                return false;
            }
        }

        public static bool IsNumberValue(this string tokenExpression)
        {
            return double.TryParse(tokenExpression, out double number);
        }

        public static bool IsOperator(this string tokenExpression)
        {
            return IsTokenType<Operator>(Operators, tokenExpression);
        }

        public static bool IsFunction(this string tokenExpression)
        {
            bool isFunction = false;

            int paramsIndex = tokenExpression.IndexOf("(");
            if(paramsIndex >= 0)
            {
                isFunction = IsTokenType<Function>(Functions, tokenExpression.Remove(paramsIndex));
            }

            return isFunction;
        }

        public static bool IsParenthesis(this string tokenExpression)
        {
            return IsTokenType<Parenthesis.Parenthesis>(Parentheses, tokenExpression);
        }

        public static bool IsOpenParenthesis(this string tokenExpression)
        {
            return IsTokenType<Parenthesis.Parenthesis>(Parentheses[0], tokenExpression);
        }

        public static bool IsClosingParenthesis(this string tokenExpression)
        {
            return IsTokenType<Parenthesis.Parenthesis>(Parentheses[1], tokenExpression);
        }

        public static bool IsCalculationFinish(this string tokenExpression) => tokenExpression.Equals("=");

        public static string GetTokenIdentifier<T>() where T: IToken
        {
            string result = string.Empty;

            for (int i = 0; i < Tokens.Count && result == string.Empty; i++)
            {
                if(Tokens[i].GetType().Equals(typeof(T)) == true)
                {
                    result = Tokens[i].Identifier;
                }
            }

            return result;
        }

        private static bool IsTokenType<T>(List<T> tokens, string tokenExpression) where T : Token
        {
            for (int i = 0; i < tokens.Count; i++)
            {
                if (IsTokenType(tokens[i], tokenExpression) == true)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsTokenType<T>(T token, string tokenExpression) where T : Token
        {
            return token.Identifier.Equals(tokenExpression) == true;
        }

        private static IToken GetOperatorToken(this string tokenExpression)
        {
            return GetToken(Operators, tokenExpression);
        }

        public static Operator GetOperatorToken<T>() where T : Operator
        {
            return GetToken<Operator, T>(Operators);
        }

        private static IToken GetFunctionToken(this string tokenExpression)
        {
            int indexOfParam = tokenExpression.IndexOf("(");
            int indexOfParamEnd = tokenExpression.IndexOf(")");

            Function functionToken = GetToken(Functions, tokenExpression.Remove(indexOfParam)) as Function;

            if(functionToken != null)
            {
                functionToken.Argument = new NumberValue(tokenExpression.Substring(indexOfParam + 1, indexOfParamEnd - indexOfParam - 1));
            }

            return functionToken;
        }

        public static Function GetFunctionToken<T>() where T : Function
        {
            return GetToken<Function, T>(Functions);
        }

        private static IToken GetParenthesisToken(this string tokenExpression)
        {
            return GetToken(Parentheses, tokenExpression);
        }

        public static Parenthesis.Parenthesis GetParenthesisToken<T>() where T : Parenthesis.Parenthesis
        {
            return GetToken<Parenthesis.Parenthesis, T>(Parentheses);
        }

        private static IToken GetToken<T>(List<T> tokens, string tokenExpression) where T : IToken
        {
            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].Identifier.Equals(tokenExpression) == true)
                {
                    return tokens[i];
                }
            }

            throw new NotTokenException();
        }

        private static U GetToken<T, U>(List<T> tokens) 
            where T : IToken 
            where U : T
        {
            U result = default(U);

            for (int i = 0; i < tokens.Count && result.Equals(default(U)); i++)
            {
                if(tokens[i] is U)
                {
                    result = (U) tokens[i];
                }
            }

            return result;
        }

        private static IEnumerable<IToken> GatherTokens()
        {
            List<IToken> result = new List<IToken>();

            result.AddRange(Operators.GetTokens());
            result.AddRange(Functions.GetTokens());
            result.AddRange(Parentheses.GetTokens());

            return result;
        }

        private static IEnumerable<IToken> GetTokens<T>(this List<T> tokens) where T : IToken
        {
            List<IToken> result = new List<IToken>();

            for (int i = 0; i < tokens.Count; i++)
            {
                result.Add(tokens[i]);
            }

            return result;
        }
    }
}
