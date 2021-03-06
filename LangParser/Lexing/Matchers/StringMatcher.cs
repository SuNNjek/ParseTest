﻿using LangParser.Lexing.Tokens;
using System.Text;

namespace LangParser.Lexing.Matchers
{
	public class StringMatcher : Matcher
	{
		protected override Token GetToken(Tokenizer tokenizer)
		{
			StringBuilder builder = new StringBuilder();

			if(tokenizer.Current == "\"")
			{
				tokenizer.Consume();

				while(!tokenizer.EOS && tokenizer.Current != "\"")
				{
					if(tokenizer.Current == "\\")
					{
						tokenizer.Consume();
						
						switch(tokenizer.Current)
						{
							case "n":
								builder.Append("\n");
								break;
							case "r":
								builder.Append("\r");
								break;
							case "t":
								builder.Append("\t");
								break;
							default:
								throw new InvalidSymbolException($"Invalid escape sequence: \\{tokenizer.Current}", tokenizer.Index, null);
						}

						tokenizer.Consume();
					}
					else
					{
						builder.Append(tokenizer.Consume());
					}
				}

				if (tokenizer.Current == "\"")
					tokenizer.Consume();
				else
					throw new InvalidSymbolException("Unexpected end of file, expected string delimiter", tokenizer.Index, null);
			}

			if (builder.Length > 0)
				return new StringToken(builder.ToString());

			return null;
		}
	}
}
