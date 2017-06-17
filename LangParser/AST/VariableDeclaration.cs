using LangParser.Lexing.Tokens;
using LangParser.Visitors;

namespace LangParser.AST
{
	public class VariableDeclaration : Ast<VariableDeclaration>
	{
		public IdentifierToken Name { get; set; }
		public Type Type { get; set; }

		public VariableDeclaration(IdentifierToken name, Type type) : base(name)
		{
			Name = name;
			Type = type;
		}

		public override void Accept(IVisitor<VariableDeclaration> visitor) => visitor.Visit(this);

		public override string ToString() => $"{Name.Value}: {Type.Token.Value}";
	}
}
