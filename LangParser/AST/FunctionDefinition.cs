using System.Collections.Generic;
using LangParser.Lexing.Tokens;
using LangParser.Visitors;

namespace LangParser.AST
{
	public class FunctionDefinition : Ast<FunctionDefinition>
	{
		public IdentifierToken Name { get; set; }
		public Type ReturnType { get; set; }
		public IList<VariableDeclaration> Arguments { get; set; }
		public Scope Body { get; set; }

		public FunctionDefinition(IdentifierToken name, Type returnType, IList<VariableDeclaration> args, Scope body) : base(name)
		{
			Name = name;
			ReturnType = returnType;
			Arguments = args;
			Body = body;
		}

		public override void Accept(IVisitor<FunctionDefinition> visitor) => visitor.Visit(this);
	}
}
