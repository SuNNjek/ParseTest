using LangParser.Lexing.Tokens;
using LangParser.Visitors;

namespace LangParser.AST
{
	public class DoWhileLoop : Ast<DoWhileLoop>
	{
		public Ast Condition { get; set; }
		public Scope Body { get; set; }

		public DoWhileLoop(Token token, Ast condition, Scope body) : base(token)
		{
			Condition = condition;
			Body = body;
		}

		public override void Accept(IVisitor<DoWhileLoop> visitor) => visitor.Visit(this);
	}
}
