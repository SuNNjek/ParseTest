using System;
using System.Collections.Generic;
using System.Text;
using ParseTest.Lexing.Tokens;

namespace ParseTest.Lexing.Matchers
{
	public class NumberMatcher : Matcher
	{
		protected override Token GetToken(Tokenizer tokenizer)
		{
			StringBuilder builder = new StringBuilder();

			string leftPart = ReadInteger(tokenizer);

			if(leftPart != null)
			{
				builder.Append(leftPart);

				if(tokenizer.Current == ".")
				{
					builder.Append(tokenizer.Current);
					tokenizer.Consume();

					string rightPart = ReadInteger(tokenizer);

					if (rightPart != null)
						builder.Append(rightPart);
				}

				return new NumberToken(Double.Parse(builder.ToString()));
			}

			return null;
		}

		private string ReadInteger(Tokenizer tokenizer)
		{
			StringBuilder builder = new StringBuilder();

			while(tokenizer.Current != null && Char.IsDigit(tokenizer.Current, 0))
			{
				builder.Append(tokenizer.Current);
				tokenizer.Consume();
			}

			if(builder.Length > 0)
				return builder.ToString();

			return null;
		}
	}
}
