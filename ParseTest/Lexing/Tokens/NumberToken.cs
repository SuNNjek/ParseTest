namespace ParseTest.Lexing.Tokens
{
	public class NumberToken : Token<double>
	{
		public NumberToken(double value) : base(value) { }
	}
}
