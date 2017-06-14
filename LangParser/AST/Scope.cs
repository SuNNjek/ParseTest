using System.Collections.Generic;
using LangParser.Visitors;

namespace LangParser.AST
{
	public class Scope : Ast<Scope>
	{
		public IList<Ast> Statements { get; set; }

		public Scope(IList<Ast> statements) : base(null)
		{
			Statements = statements;
		}

		public override void Accept(IVisitor<Scope> visitor) => visitor.Visit(this);
	}
}
