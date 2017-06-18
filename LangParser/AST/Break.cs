using LangParser.Lexing.Tokens;
using LangParser.Visitors;

namespace LangParser.AST
{
	public class Break : Ast<Break>
	{
		public Break(BreakToken token) : base(token) { }

		public override void Accept(IVisitor<Break> visitor) => visitor.Visit(this);
	}
}
