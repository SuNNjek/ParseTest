namespace LangParser.Lexing.Tokens
{
	public class IntNumberToken : Token<int>
	{
		public IntNumberToken(int value) : base(value) { }
	}

	public class FloatNumberToken : Token<float>
	{
		public FloatNumberToken(float value) : base(value) { }
	}
}
