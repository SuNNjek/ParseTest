using LangParser.Lexing.Tokens;
using LangParser.Visitors;

namespace LangParser.AST
{
	public class Continue : Ast<Continue>
	{
		public Continue(ContinueToken token) : base(token) { }

		public override void Accept(IVisitor<Continue> visitor) => visitor.Visit(this);
	}
}
