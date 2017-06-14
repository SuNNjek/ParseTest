using LangParser.Lexing.Tokens;
using LangParser.Visitors;

namespace LangParser.AST
{
	public class ConditionalBranch : Ast<ConditionalBranch>
	{
		public Ast Predicate { get; set; }
		public Ast Body { get; set; }
		public Ast Else { get; set; }

		public ConditionalBranch(Token token, Ast predicate, Ast body, Ast @else = null) : base(token)
		{
			Predicate = predicate;
			Body = body;
			Else = @else;
		}

		public override void Accept(IVisitor<ConditionalBranch> visitor) => visitor.Visit(this);
	}
}
