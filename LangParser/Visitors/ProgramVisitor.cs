using System;
using LangParser.AST;

namespace LangParser.Visitors
{
	public abstract class ProgramVisitor
		: IVisitor<Program>
		, IVisitor<BinaryExpression>
		, IVisitor<ConditionalBranch>
		, IVisitor<DoWhileLoop>
		, IVisitor<ForLoop>
		, IVisitor<FunctionCall>
		, IVisitor<FunctionDefinition>
		, IVisitor<Literal>
		, IVisitor<ReturnExpression>
		, IVisitor<Scope>
		, IVisitor<AST.Type>
		, IVisitor<UnaryExpression>
		, IVisitor<Variable>
		, IVisitor<VariableDeclaration>
		, IVisitor<WhileLoop>
		, IVisitor<Break>
		, IVisitor<Continue>
	{
		public virtual void Start(IVisitable visitable)
		{
			switch (visitable)
			{
				case Program program:
					Visit(program);
					break;
				case BinaryExpression bin:
					Visit(bin);
					break;
				case ConditionalBranch branch:
					Visit(branch);
					break;
				case DoWhileLoop doWhile:
					Visit(doWhile);
					break;
				case ForLoop forLoop:
					Visit(forLoop);
					break;
				case FunctionCall funcCall:
					Visit(funcCall);
					break;
				case FunctionDefinition funcDef:
					Visit(funcDef);
					break;
				case Literal lit:
					Visit(lit);
					break;
				case ReturnExpression ret:
					Visit(ret);
					break;
				case Scope scope:
					Visit(scope);
					break;
				case AST.Type type:
					Visit(type);
					break;
				case UnaryExpression un:
					Visit(un);
					break;
				case Variable var:
					Visit(var);
					break;
				case VariableDeclaration decl:
					Visit(decl);
					break;
				case WhileLoop whileLoop:
					Visit(whileLoop);
					break;
				case Break brk:
					Visit(brk);
					break;
				case Continue cont:
					Visit(cont);
					break;

				default:
					throw new InvalidOperationException($"Visitor doesn't support visitables of type {visitable.GetType()}");
			}
		}

		public abstract void Visit(WhileLoop visitable);
		public abstract void Visit(VariableDeclaration visitable);
		public abstract void Visit(AST.Type visitable);
		public abstract void Visit(UnaryExpression visitable);
		public abstract void Visit(Variable visitable);
		public abstract void Visit(Scope visitable);
		public abstract void Visit(ReturnExpression visitable);
		public abstract void Visit(Literal visitable);
		public abstract void Visit(FunctionDefinition visitable);
		public abstract void Visit(FunctionCall visitable);
		public abstract void Visit(ForLoop visitable);
		public abstract void Visit(DoWhileLoop visitable);
		public abstract void Visit(ConditionalBranch visitable);
		public abstract void Visit(BinaryExpression visitable);
		public abstract void Visit(Program visitable);
		public abstract void Visit(Continue visitable);
		public abstract void Visit(Break visitable);
	}
}
