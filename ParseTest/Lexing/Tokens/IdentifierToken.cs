namespace ParseTest.Lexing.Tokens
{
	public class IdentifierToken : Token<string>
	{
		public IdentifierToken(string value) : base(value) { }
	}
}
