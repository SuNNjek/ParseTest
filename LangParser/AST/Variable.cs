using LangParser.Lexing.Tokens;
using LangParser.Visitors;

namespace LangParser.AST
{
	public class Variable : Ast<Variable>
	{
		public Variable(IdentifierToken token) : base(token) { }

		public override void Accept(IVisitor<Variable> visitor) => visitor.Visit(this);
	}
}
