﻿using LangParser.Lexing.Tokens;
using System;

namespace LangParser.Lexing.Matchers
{
	public class WhitespaceMatcher : Matcher
	{
		protected override Token GetToken(Tokenizer tokenizer)
		{
			bool foundWhitespace = false;

			while(!tokenizer.EOS && String.IsNullOrWhiteSpace(tokenizer.Current))
			{
				foundWhitespace = true;

				tokenizer.Consume();
			}

			if (foundWhitespace)
				return new WhitespaceToken();

			return null;
		}
	}
}
