using LangParser.Lexing.Tokens;
using LangParser.Visitors;

namespace LangParser.AST
{
	public class ForLoop : Ast<ForLoop>
	{
		public Ast Initialization { get; set; }
		public Ast Condition { get; set; }
		public Ast Afterthought { get; set; }
		public Scope Body { get; set; }

		public ForLoop(Token token, Ast initialization, Ast condition, Ast afterthought, Scope body) : base(token)
		{
			Initialization = initialization;
			Condition = condition;
			Afterthought = afterthought;
			Body = body;
		}

		public override void Accept(IVisitor<ForLoop> visitor) => visitor.Visit(this);
	}
}
