using Calculator.Expression;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Calculator.Token
{
    public class CalculatorExpression : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _expression;

        public string Expression
        {
            get => _expression;
            set
            {
                if(_expression != value)
                {
                    _expression = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private List<string> Tokens { get; set; } = new List<string>();
        private PostfixExpressionLexer Lexer { get; set; } = new PostfixExpressionLexer();
        private PostfixExpressionParser Parser { get; set; } = new PostfixExpressionParser();
        private PostfixExpressionInterpreter Interpreter { get; set; } = new PostfixExpressionInterpreter();

        public bool IsLastTokenOperator() => IsEmpty() == false && GetLastElement().IsOperator();
        public bool IsLastTokenFunction() => IsEmpty() == false && GetLastElement().IsFunction();
        public bool IsLastTokenOpenParenthesis() => IsEmpty() == false && GetLastElement().IsOpenParenthesis();
        public bool IsLastTokenClosingParenthesis() => IsEmpty() == false && GetLastElement().IsClosingParenthesis();
        public bool IsLastTokenCalculationFinish() => IsEmpty() == false && GetLastElement().IsCalculationFinish();
        public bool IsEmpty() => Tokens.Count == 0;

        public void ClearExpression()
        {
            Tokens.Clear();

            TransferTokensToExpression();
        }

        public void AddTokens(params string[] tokens)
        {
            Tokens.AddRange(tokens);
            TransferTokensToExpression();
        }

        public void ReplaceLastToken(string token)
        {
            if(Tokens.Count > 0)
            {
                Tokens[Tokens.Count - 1] = token;
            }
            TransferTokensToExpression();
        }

        private void TransferTokensToExpression()
        {
            StringBuilder expressionBuilder = new StringBuilder();

            for (int i = 0; i < Tokens.Count; i++)
            {
                expressionBuilder.AppendFormat("{0} ", Tokens[i]);
            }

            Expression = expressionBuilder.ToString().TrimEnd();
        }

        public double Compute()
        {
            Tokens.Add("=");
            List<IToken> tokens = Lexer.AnalyzeExpression(Expression);
            Queue<IToken> queuedTokens = Parser.ParseTokens(tokens);
            return Interpreter.Compute(queuedTokens);
        }

        private string GetLastElement() => Tokens[Tokens.Count - 1];

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
