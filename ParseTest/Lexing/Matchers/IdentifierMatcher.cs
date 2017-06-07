using System;
using System.Collections.Generic;
using System.Text;
using ParseTest.Lexing.Tokens;

namespace ParseTest.Lexing.Matchers
{
	public class IdentifierMatcher : Matcher
	{
		private bool IsValidStartChar(string s) => Char.IsLetter(s, 0) || s[0] == '_';
		private bool IsValidChar(string s) => Char.IsLetterOrDigit(s, 0) || s[0] == '_';

		protected override Token GetToken(Tokenizer tokenizer)
		{
			if (IsValidStartChar(tokenizer.Current))
			{
				StringBuilder builder = new StringBuilder();
				builder.Append(tokenizer.Current);
				tokenizer.Consume();

				while(tokenizer.Current != null && IsValidChar(tokenizer.Current))
				{
					builder.Append(tokenizer.Current);
					tokenizer.Consume();
				}

				return new IdentifierToken(builder.ToString());
			}

			return null;
		}
	}
}
