using LangParser.Lexing.Tokens;
using LangParser.Visitors;

namespace LangParser.AST
{
	public class ReturnExpression : Ast<ReturnExpression>
	{
		public Ast Expression { get; set; }

		public ReturnExpression(Token token, Ast expr) : base(token)
		{
			Expression = expr;
		}

		public override void Accept(IVisitor<ReturnExpression> visitor) => visitor.Visit(this);
	}
}
