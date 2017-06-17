using LangParser.Lexing.Tokens;
using LangParser.Visitors;

namespace LangParser.AST
{
	public class Type : Ast<Type>
	{
		public Type(Token token) : base(token) { }

		public override void Accept(IVisitor<Type> visitor) => visitor.Visit(this);
	}
}
