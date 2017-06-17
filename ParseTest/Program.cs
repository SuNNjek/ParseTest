using LangParser.Lexing;
using LangParser.Lexing.Tokens;
using System;
using LangParser.Parsing;
using LangParser.AST;
using System.Collections.Generic;
using LangParser.Visitors;

namespace ParseTest
{
	class Program
	{
		static void Main(string[] args)
		{
			string code = @"
int test(int hallo)
{
	for(int i = 0; !(true ^ 0); i = i + 5)
		while(5 < 4)
			print(""Hallo Welt"");

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