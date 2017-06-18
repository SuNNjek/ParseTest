using System;
using System.Collections.Generic;
using System.Text;
using LangParser.AST;

namespace LangParser.Visitors
{
	public class PrintAstVisitor : ProgramVisitor
	{
		private StringBuilder _builder = new StringBuilder();
		private int _indentLevel = 0;

		public string Value => _builder.ToString();

		public void Clear()
		{
			_builder.Clear();
			_indentLevel = 0;
		}

		public override void Visit(Program visitable)
		{
			WriteIndentantion(true).AppendLine("Program start");

			foreach (Ast statement in visitable.Statements)
				Start(statement);

			_indentLevel--;
		}

		public override void Visit(BinaryExpression visitable)
		{
			WriteIndentantion(true).AppendLine($"Binary expression: {visitable.Token.Value}");

			Start(visitable.Left);
			Start(visitable.Right);

			_indentLevel--;
		}

		public override void Visit(ConditionalBranch visitable)
		{
			WriteIndentantion(true).AppendLine("If statement");

			WriteIndentantion(true).AppendLine("Predicate:");
			Start(visitable.Predicate);
			_indentLevel--;

			WriteIndentantion(true).AppendLine("Body:");
			Start(visitable.Body);
			_indentLevel--;

			if(visitable.Else != null)
			{
				WriteIndentantion(true).AppendLine("Else:");
				Start(visitable.Else);
				_indentLevel--;
			}

			_indentLevel -= 2;
		}

		public override void Visit(DoWhileLoop visitable)
		{
			WriteIndentantion(true).AppendLine("Do-while loop");

			WriteIndentantion(true).AppendLine("Body:");
			Start(visitable.Body);
			_indentLevel--;

			WriteIndentantion(true).AppendLine("Condition:");
			Start(visitable.Condition);
			_indentLevel--;

			_indentLevel--;
		}

		public override void Visit(ForLoop visitable)
		{
			WriteIndentantion(true).AppendLine("For-loop");

			if (visitable.Initialization != null)
			{
				WriteIndentantion(true).AppendLine("Initialization:");
				Start(visitable.Initialization);
				_indentLevel--;
			}

			if (visitable.Condition != null)
			{
				WriteIndentantion(true).AppendLine("Condition:");
				Start(visitable.Condition);
				_indentLevel--;
			}

			if (visitable.Afterthought != null)
			{
				WriteIndentantion(true).AppendLine("Afterthought:");
				Start(visitable.Afterthought);
				_indentLevel--;
			}

			WriteIndentantion(true).AppendLine("Body:");
			Start(visitable.Body);
			_indentLevel--;
		}

		public override void Visit(FunctionCall visitable)
		{
			WriteIndentantion(true).AppendLine($"Function call: {visitable.Name.Value}");

			for(int i = 0; i < visitable.Arguments.Count; i++)
			{
				WriteIndentantion(true).AppendLine($"Argument {i}:");
				Start(visitable.Arguments[i]);
				_indentLevel--;
			}

			_indentLevel--;
		}

		public override void Visit(FunctionDefinition visitable)
		{
			WriteIndentantion(true).AppendLine($"Function definition: {visitable.ReturnType.Token.Value} {visitable.Name.Value}");

			for(int i = 0; i < visitable.Arguments.Count; i++)
			{
				WriteIndentantion(true).AppendLine($"Parameter {i}:");
				Start(visitable.Arguments[i]);
				_indentLevel--;
			}

			WriteIndentantion(true).AppendLine("Body:");
			Start(visitable.Body);

			_indentLevel--;
		}

		public override void Visit(Literal visitable)
		{
			WriteIndentantion(true).AppendLine($"Literal: {visitable.Value}");
			_indentLevel--;
		}

		public override void Visit(ReturnExpression visitable)
		{
			WriteIndentantion(true).AppendLine("Return value:");
			Start(visitable.Expression);
			_indentLevel--;
		}

		public override void Visit(Scope visitable)
		{
			WriteIndentantion(true).AppendLine("Scope");

			foreach (Ast ast in visitable.Statements) Start(ast);
			_indentLevel--;
		}

		public override void Visit(AST.Type visitable)
		{
			WriteIndentantion(true).AppendLine($"Type: {visitable.Token.Value}");
			_indentLevel--;
		}

		public override void Visit(UnaryExpression visitable)
		{
			WriteIndentantion(true).AppendLine($"Unary expression: {visitable.Token.Value}");

			Start(visitable.Expression);

			_indentLevel--;
		}

		public override void Visit(Variable visitable)
		{
			WriteIndentantion(true).AppendLine($"Variable: {visitable.Token.Value}");
			_indentLevel--;
		}

		public override void Visit(VariableDeclaration visitable)
		{
			WriteIndentantion(true).AppendLine($"{visitable.Type.Token.Value} {visitable.Name.Value}");
			_indentLevel--;
		}

		public override void Visit(WhileLoop visitable)
		{
			WriteIndentantion(true).AppendLine("While loop");

			WriteIndentantion(true).AppendLine("Condition:");
			Start(visitable.Condition);
			_indentLevel--;

			WriteIndentantion(true).AppendLine("Body:");
			Start(visitable.Body);
			_indentLevel--;

			_indentLevel--;
		}

		public override void Visit(Continue visitable)
		{
			WriteIndentantion(true).AppendLine("Continue");
			_indentLevel--;
		}

		public override void Visit(Break visitable)
		{
			WriteIndentantion(true).AppendLine("Break");
			_indentLevel--;
		}

		private StringBuilder WriteIndentantion(bool increment)
		{
			for(int i = 0; i < _indentLevel; i++)
			{
				if (i == _indentLevel - 1)
					_builder.Append("|- ");
				else
					_builder.Append("  ");
			}

			if (increment)
				_indentLevel++;

			return _builder;
		}
	}
}
