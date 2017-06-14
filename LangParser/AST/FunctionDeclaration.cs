using System.Collections.Generic;
using LangParser.Lexing.Tokens;
using LangParser.Visitors;

namespace LangParser.AST
{
	public class FunctionDeclaration : Ast<FunctionDeclaration>
	{
		public IdentifierToken Name { get; set; }
		public Ast ReturnType { get; set; }
		public IList<Ast> Arguments { get; set; }
		public Scope Body { get; set; }

		public FunctionDeclaration(Token token, IdentifierToken name, Ast returnType, IList<Ast> args, Scope body) : base(token)
		{
			Name = name;
			ReturnType = returnType;
			Arguments = args;
			Body = body;
		}

		public override void Accept(IVisitor<FunctionDeclaration> visitor) => visitor.Visit(this);
	}
}
