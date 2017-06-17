using LangParser.Lexing.Tokens;
using LangParser.Visitors;

namespace LangParser.AST
{
	public class UnaryExpression : Ast<UnaryExpression>
	{
		public Ast Expression { get; set; }

		public UnaryExpression(Token token, Ast expr) : base(token)
		{
			Expression = expr;
		}

		public override void Accept(IVisitor<UnaryExpression> visitor) => visitor.Visit(this);
	}
}
