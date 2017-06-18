namespace LangParser.Lexing.Tokens
{
	public class IfToken : Token
	{
		public IfToken() : base("if") { }
	}

	public class ElseToken : Token
	{
		public ElseToken() : base("else") { }
	}

	public class DoToken : Token
	{
		public DoToken() : base("do") { }
	}

	public class WhileToken : Token
	{
		public WhileToken() : base("while") { }
	}

	public class ForToken : Token
	{
		public ForToken() : base("for") { }
	}

	public class TrueToken : Token
	{
		public TrueToken() : base(true) { }
	}

	public class FalseToken : Token
	{
		public FalseToken() : base(false) { }
	}

	public class VoidToken : Token
	{
		public VoidToken() : base("void") { }
	}

	public class IntToken : Token
	{
		public IntToken() : base("int") { }
	}

	public class FloatToken : Token
	{
		public FloatToken() : base("float") { }
	}

	public class BoolToken : Token
	{
		public BoolToken() : base("bool") { }
	}

	public class StringKeywordToken : Token
	{
		public StringKeywordToken() : base("string") { }
	}

	public class ObjectToken : Token
	{
		public ObjectToken() : base("object") { }
	}

	public class ByteToken : Token
	{
		public ByteToken() : base("byte") { }
	}

	public class ReturnToken : Token
	{
		public ReturnToken() : base("return") { }
	}

	public class BreakToken : Token
	{
		public BreakToken() : base("break") { }
	}

	public class ContinueToken : Token
	{
		public ContinueToken() : base("continue") { }
	}
}
