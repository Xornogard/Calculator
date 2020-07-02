using Calculator.Context;
using Calculator.Token;
using Calculator.Token.Functions;
using Calculator.Token.Operators;
using Calculator.Token.Parenthesis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CalculatorContext Context = new CalculatorContext();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = Context;
        }

        #region BASIC_METHODS

        public void ClearAll()
        {
            Context.Expression.ClearExpression();
            Context.Entry.ClearEntry();
        }

        private void RemoveEntry() => Context.Entry.RemoveEntry();
        private void NegateEntry() => Context.Entry.NegateEntry();
        public void SetFloatingEntry() => Context.Entry.SetFloatingEntry(); 

        #endregion

        #region NUMERAL_BUTTONS_EVENTS

        private void NumeralButton_Click(object sender, RoutedEventArgs e)
        {
            Button senderButton = sender as Button;

            if (senderButton != null)
            {
                Context.Entry.AddEntry(senderButton.Content.ToString());
            }
        }

        #endregion

        #region OPERATOR_BUTTONS_EVENTS

        private void EqualOperatorButton_Click(object sender, RoutedEventArgs e) => Compute();
        private void ClearButton_Click(object sender, RoutedEventArgs e) => ClearAll();
        private void DeleteButton_Click(object sender, RoutedEventArgs e) => RemoveEntry();
        private void SignOperatorButton_Click(object sender, RoutedEventArgs e) => NegateEntry();
        private void CommaOperatorButton_Click(object sender, RoutedEventArgs e) => SetFloatingEntry();

        private void PlusOperatorButton_Click(object sender, RoutedEventArgs e) => AddOperatorToExpression<AddOperator>();
        private void MinusOperatorButton_Click(object sender, RoutedEventArgs e) => AddOperatorToExpression<SubstractOperator>();
        private void MultiplicationOperatorButton_Click(object sender, RoutedEventArgs e) => AddOperatorToExpression<MultiplicationOperator>();
        private void PercentOperatorButton_Click(object sender, RoutedEventArgs e) => AddOperatorToExpression<PercentOperator>();
        private void ModOperatorButton_Click(object sender, RoutedEventArgs e) => AddOperatorToExpression<ModOperator>();
        private void DivisionButton_Click(object sender, RoutedEventArgs e) => AddOperatorToExpression<DivisionOperator>();
        private void PowerOperatorButton_Click(object sender, RoutedEventArgs e) => AddOperatorToExpression<PowerOperator>();

        private void SquarePowerOperatorButton_Click(object sender, RoutedEventArgs e)
        {
            AddOperatorToExpression<PowerOperator>();
            Context.Entry.AddEntry("2");
            Compute();
        }

        private void OpenParenthesisOperatorButton_Click(object sender, RoutedEventArgs e)
        {
            string openParenthesisTokenIdentifier = TokenUtils.GetTokenIdentifier<OpenParenthesis>();

            if(Context.Expression.IsEmpty() || Context.Expression.IsLastTokenOperator() || Context.Expression.IsLastTokenOpenParenthesis())
            {
                Context.Expression.AddTokens(openParenthesisTokenIdentifier);
            }
        }

        private void ClosedParenthesisOperatorButton_Click(object sender, RoutedEventArgs e)
        {
            string closedParenthesisTokenIdentifier = TokenUtils.GetTokenIdentifier<ClosedParenthesis>();

            if (Context.Expression.IsLastTokenOperator() || Context.Expression.IsLastTokenOpenParenthesis())
            {
                Context.Expression.AddTokens(Context.Entry.Entry);
            }

            if (Context.Expression.IsLastTokenOperator() == false)
            {
                Context.Expression.AddTokens(closedParenthesisTokenIdentifier);
                Context.Entry.ClearEntry();
            }
        }

        #endregion

        #region FUNCTION_BUTTONS_EVENTS

        private void AbsoluteOperatorButton_Click(object sender, RoutedEventArgs e) => AddFunctionToExpression<AbsFunction>();
        private void InverseOperatorButton_Click(object sender, RoutedEventArgs e) => AddFunctionToExpression<InvFunction>();
        private void ExponentialOperatorButton_Click(object sender, RoutedEventArgs e) => AddFunctionToExpression<ExpFunction>();
        private void SqrtOperatorButton_Click(object sender, RoutedEventArgs e) => AddFunctionToExpression<SqrtFunction>();
        private void SinOperatorButton_Click(object sender, RoutedEventArgs e) => AddFunctionToExpression<SinFunction>();
        private void CosOperatorButton_Click(object sender, RoutedEventArgs e) => AddFunctionToExpression<CosFunction>();
        private void TgOperatorButton_Click(object sender, RoutedEventArgs e) => AddFunctionToExpression<TgFunction>();
        private void CtgOperatorButton_Click(object sender, RoutedEventArgs e) => AddFunctionToExpression<CtgFunction>();
        private void LogOperatorButton_Click(object sender, RoutedEventArgs e) => AddFunctionToExpression<LogFunction>();
        private void LnOperatorButton_Click(object sender, RoutedEventArgs e) => AddFunctionToExpression<LnFunction>();


        #endregion

        #region MAIN_LOGIC

        private void AddFunctionToExpression<T>() where T : Function
        {
            string functionTokenIdentifier = TokenUtils.GetTokenIdentifier<T>();

            if(Context.Expression.IsLastTokenCalculationFinish())
            {
                Context.Expression.ClearExpression();
            }
            
            if(Context.Expression.IsEmpty() || Context.Expression.IsLastTokenOperator() || Context.Expression.IsLastTokenOpenParenthesis())
            {
                Context.Expression.AddTokens($"{functionTokenIdentifier}({Context.Entry.Entry})");
                Context.Entry.ClearEntry();
                Compute();
            }
        }

        private void AddOperatorToExpression<T>() where T : Operator
        {
            string operatorIdentifier = TokenUtils.GetTokenIdentifier<T>();

            if (Context.Expression.IsLastTokenCalculationFinish())
            {
                Context.Expression.ClearExpression();
                Context.Expression.AddTokens(Context.Entry.Entry, operatorIdentifier);
                Context.Entry.ClearEntry();
            }
            else if (Context.Entry.IsValid == false && Context.Expression.IsLastTokenOperator())
            {
                Context.Expression.ReplaceLastToken(operatorIdentifier);
            }
            else if (Context.Expression.IsLastTokenFunction() || Context.Expression.IsLastTokenClosingParenthesis())
            {
                Context.Expression.AddTokens(operatorIdentifier);
            }
            else if (Context.Expression.IsEmpty() == false && Context.Expression.IsLastTokenOperator() == false && Context.Expression.IsLastTokenClosingParenthesis())
            {
                Context.Expression.AddTokens(operatorIdentifier, Context.Entry.Entry);
                Context.Entry.ClearEntry();
            }
            else if(Context.Entry.IsValid)
            {
                Context.Expression.AddTokens(Context.Entry.Entry, operatorIdentifier);
                Context.Entry.ClearEntry();
            }
        }

        private void Compute()
        {
            if(Context.Expression.IsLastTokenCalculationFinish())
            {
                Context.Expression.ClearExpression();
            }

            if(Context.Entry.IsValid && Context.Expression.IsLastTokenFunction() == false && Context.Expression.IsLastTokenClosingParenthesis() == false)
            {
                Context.Expression.AddTokens(Context.Entry.Entry);
            }

            if ((Context.Expression.IsEmpty() == false && Context.Expression.IsLastTokenOperator() == false) || Context.Expression.IsLastTokenClosingParenthesis())
            {
                try
                {
                    Context.Entry.Entry = Context.Expression.Compute().ToString();
                }
                catch (Exception e)
                {

                }
            }
        }

        #endregion
    }
}
