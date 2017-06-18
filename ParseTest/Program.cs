using System;
using LangParser.AST;
using LangParser.Lexing;
using LangParser.Parsing;
using LangParser.Visitors;

namespace ParseTest
{
	class Program
	{
		static void Main(string[] args)
		{
			string code = @"
int test(float hallo)
{
	for(int i = 0; i < 5; i = i + 9)
	{
		if(i == 3) continue;
	}

	return 5 + 9;
}

string test2()
{
	return toString(test(4));
}";
			Lexer lexer = new Lexer(code);
			Parser parser = new Parser(lexer);

			PrintAstVisitor visitor = new PrintAstVisitor();

			try
			{
				foreach (Ast ast in parser.Parse())
					visitor.Start(ast);
			}
			catch(InvalidSymbolException e)
			{
				Console.WriteLine($"Invalid symbol at position {e.Position}: {e.Message}");
				Console.ReadKey();
				return;
			}
			catch(InvalidSyntaxException e)
			{
				Console.WriteLine($"Syntax error at position {e.Position}: {e.Message}");
				Console.ReadKey();
				return;
			}

			Console.WriteLine(visitor.Value);
			Console.ReadKey();
		}
	}
}