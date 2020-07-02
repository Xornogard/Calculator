namespace Calculator.Token
{
    public abstract class Token : IToken
    {
        public abstract string Identifier { get; }

        public override string ToString() => Identifier;
    }

}
