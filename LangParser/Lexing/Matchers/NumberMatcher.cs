using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using LangParser.Lexing.Tokens;

namespace LangParser.Lexing.Matchers
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
					builder.Append(tokenizer.Consume());

					string rightPart = ReadInteger(tokenizer);

					if (rightPart != null)
					{
						builder.Append(rightPart);

						//Check for "f" suffix
						if (tokenizer.Current == "f")
							tokenizer.Consume();
					}

					return new FloatNumberToken(Single.Parse(builder.ToString(), CultureInfo.InvariantCulture));
				}

				if(tokenizer.Current == "f")
				{
					tokenizer.Consume();
					return new FloatNumberToken(Single.Parse(builder.ToString(), CultureInfo.InvariantCulture));
				}

				return new IntNumberToken(Int32.Parse(builder.ToString(), CultureInfo.InvariantCulture));
			}

			return null;
		}

		private string ReadInteger(Tokenizer tokenizer)
		{
			StringBuilder builder = new StringBuilder();

			while(tokenizer.Current != null && Char.IsDigit(tokenizer.Current, 0))
				builder.Append(tokenizer.Consume());

			if(builder.Length > 0)
				return builder.ToString();

			return null;
		}
	}
}
