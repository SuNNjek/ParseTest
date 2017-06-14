namespace LangParser.Lexing.Tokens
{
	public abstract class Token
	{
		public object Value { get; }

		protected Token(object value)
		{
			Value = value;
		}

		public override string ToString() => $"{GetType().Name} - {Value}";
	}

	public abstract class Token<TValue> : Token
	{
		public new TValue Value { get; }

		protected Token(TValue value) : base(value) { }
	}
}
