using ParseTest.Lexing;
using ParseTest.Lexing.Tokens;
using System;

namespace ParseTest
{
	class Program
	{
		static void Main(string[] args)
		{
			string code = "if(5+8){x=9;}else{9*(5);}";
			Lexer lexer = new Lexer(code);

			foreach(Token t in lexer.Lex())
			{
				Console.WriteLine(t);
			}

			Console.ReadKey();
		}
	}
}