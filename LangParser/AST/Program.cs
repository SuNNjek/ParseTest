using System;
using System.Collections.Generic;
using System.Text;
using LangParser.Lexing.Tokens;
using LangParser.Visitors;

namespace LangParser.AST
{
	public class Program : Ast<Program>
	{
		private IList<Ast> _statements;
		public IList<Ast> Statements => _statements;

		public Program(IList<Ast> statements) : base(null)
		{
			_statements = statements; 
		}

		public override void Accept(IVisitor<Program> visitor) => visitor.Visit(this);
	}
}
