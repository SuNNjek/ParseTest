using System.Text;

namespace LangParser.Lexing.Tokens
{
	public abstract class Token
	{
		public object Value { get; set; }
		public int Position { get; set; }

		protected Token(object value)
		{
			Value = value;
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder($"({Position}): {this.GetType().Name}");

			if (Value != null)
				builder.Append($" -  {Value}");

			return builder.ToString();
		}
	}

	public abstract class Token<TValue> : Token
	{
		public new TValue Value { get; }

		protected Token(TValue value) : base(value) { Value = value; }
	}
}
