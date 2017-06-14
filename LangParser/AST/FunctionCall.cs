using System.Collections.Generic;
using LangParser.Lexing.Tokens;
using LangParser.Visitors;

namespace LangParser.AST
{
	public class FunctionCall : Ast<FunctionCall>
	{
		public IdentifierToken Name { get; set; }
		public IList<Ast> Arguments { get; set; }

		public FunctionCall(Token token, IdentifierToken name, IList<Ast> args) : base(token)
		{
			Name = name;
			Arguments = args;
		}

		public override void Accept(IVisitor<FunctionCall> visitor) => visitor.Visit(this);
	}
}
