using LangParser.Lexing.Tokens;
using LangParser.Visitors;

namespace LangParser.AST
{
	public class BinaryExpression : Ast<BinaryExpression>
	{
		public Ast Left { get; set; }
		public Ast Right { get; set; }

		public BinaryExpression(Ast left, Token token, Ast right) : base(token)
		{
			Left = left;
			Right = right;
		}

		public override void Accept(IVisitor<BinaryExpression> visitor) => visitor.Visit(this);
	}
}
