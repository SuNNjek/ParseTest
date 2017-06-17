using LangParser.Lexing.Tokens;
using LangParser.Visitors;

namespace LangParser.AST
{
	public class Literal : Ast<Literal>
	{
		public object Value => Token.Value;

		public Literal(IntNumberToken token) : base(token) { }
		public Literal(FloatNumberToken token) : base(token) { }
		public Literal(StringToken token) : base(token) { }
		public Literal(TrueToken token) : base(token) { }
		public Literal(FalseToken token) : base(token) { }

		public override void Accept(IVisitor<Literal> visitor) => visitor.Visit(this);
	}
}
