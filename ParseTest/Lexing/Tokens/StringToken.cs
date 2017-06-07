namespace ParseTest.Lexing.Tokens
{
	public class StringToken : Token<string>
	{
		public StringToken(string value) : base(value) { }
	}
}
