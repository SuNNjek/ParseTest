using LangParser.Lexing;
using LangParser.Lexing.Tokens;
using System;

namespace ParseTest
{
	class Program
	{
		static void Main(string[] args)
		{
			string code = @"
void Test(int test)
{
	test = 5;

	if(test < 4f)
		return test;
	else
		return 4.56f;
}
";
			Lexer lexer = new Lexer(code);

			foreach(Token t in lexer.Lex())
			{
				Console.WriteLine(t);
			}

			Console.ReadKey();
		}
	}
}