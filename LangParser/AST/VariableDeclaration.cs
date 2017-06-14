using LangParser.Lexing.Tokens;
using LangParser.Visitors;

namespace LangParser.AST
{
	public class VariableDeclaration : Ast<VariableDeclaration>
	{
		public IdentifierToken Name { get; set; }
		public Ast Type { get; set; }
		public Ast Value { get; set; }

		public VariableDeclaration(Token token, IdentifierToken name, Ast type, Ast value) : base(token)
		{
			Name = name;
			Type = type;
			Value = value;
		}

		public override void Accept(IVisitor<VariableDeclaration> visitor) => visitor.Visit(this);
	}
}
