using System.Collections.Generic;
using LangParser.Lexing.Tokens;
using LangParser.Visitors;

namespace LangParser.AST
{
	public abstract class Ast : IVisitable { }

	public abstract class Ast<TAst> : Ast, IVisitable<TAst> where TAst : Ast<TAst>
	{
		public Token Token { get; set; }

		public Ast(Token token)
		{
			Token = token;
		}

		public abstract void Accept(IVisitor<TAst> visitor);
	}
}
