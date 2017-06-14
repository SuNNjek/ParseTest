using LangParser.Lexing.Tokens;
using LangParser.Visitors;

namespace LangParser.AST
{
	public class WhileLoop : Ast<WhileLoop>
	{
		public Ast Condition { get; set; }
		public Scope Body { get; set; }

		public WhileLoop(Token token, Ast condition, Scope body) : base(token)
		{
			Condition = condition;
			Body = body;
		}

		public override void Accept(IVisitor<WhileLoop> visitor) => visitor.Visit(this);
	}
}
