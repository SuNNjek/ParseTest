using System;
using System.Collections.Generic;
using System.Text;
using LangParser.Lexing;

namespace LangParser.Parsing
{
	public class Parser
	{
		private TokenStream _tokenStream;

		public Parser(Lexer lexer)
		{
			_tokenStream = new TokenStream(lexer);
		}
	}
}
