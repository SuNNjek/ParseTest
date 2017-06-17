namespace LangParser.Lexing.Tokens
{
	public class LeftParensToken : Token
	{
		public LeftParensToken() : base("(") { }
	}

	public class RightParensToken : Token
	{
		public RightParensToken() : base(")") { }
	}

	public class LeftBracketToken : Token
	{
		public LeftBracketToken() : base("{") { }
	}

	public class RightBracketToken : Token
	{
		public RightBracketToken() : base("}") { }
	}

	public class LeftSquareBracketToken : Token
	{
		public LeftSquareBracketToken() : base("[") { }
	}

	public class RightSquareBracketToken : Token
	{
		public RightSquareBracketToken() : base("]") { }
	}

	public class PlusToken : Token
	{
		public PlusToken() : base("+") { }
	}

	public class MinusToken : Token
	{
		public MinusToken() : base("-") { }
	}

	public class MultiplyToken : Token
	{
		public MultiplyToken() : base("*") { }
	}

	public class DivisionToken : Token
	{
		public DivisionToken() : base("/") { }
	}

	public class EqualsToken : Token
	{
		public EqualsToken() : base("==") { }
	}

	public class NotEqualToken : Token
	{
		public NotEqualToken() : base("!=") { }
	}

	public class LessThanToken : Token
	{
		public LessThanToken() : base("<") { }
	}

	public class GreaterThanToken : Token
	{
		public GreaterThanToken() : base(">") { }
	}

	public class LessOrEqualToken : Token
	{
		public LessOrEqualToken() : base("<=") { }
	}

	public class GreaterOrEqualToken : Token
	{
		public GreaterOrEqualToken() : base(">=") { }
	}

	public class NegationToken : Token
	{
		public NegationToken() : base("!") { }
	}

	public class AndToken : Token
	{
		public AndToken() : base("&&") { }
	}

	public class OrToken : Token
	{
		public OrToken() : base("||") { }
	}

	public class XorToken : Token
	{
		public XorToken() : base("^") { }
	}

	public class AssignmentToken : Token
	{
		public AssignmentToken() : base("=") { }
	}

	public class CommaToken : Token
	{
		public CommaToken() : base(",") { }
	}

	public class SemicolonToken : Token
	{
		public SemicolonToken() : base(";") { }
	}
}
