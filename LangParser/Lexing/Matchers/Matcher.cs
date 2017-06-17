using LangParser.Lexing.Tokens;

namespace LangParser.Lexing.Matchers
{
	public abstract class Matcher
	{
		protected abstract Token GetToken(Tokenizer tokenizer);

		public bool IsMatch(Tokenizer tokenizer, out Token token)
		{
			int position = tokenizer.Index;

			if (tokenizer.EOS)
			{
				token = new EndOfFileToken { Position = position };
				return true;
			}

			using (ISnapshot snapshot = tokenizer.TakeSnapshot())
			{
				token = GetToken(tokenizer);

				if(token != null)
				{
					token.Position = position;
					snapshot.Commit();
				}

				return token != null;
			}
		}
	}
}
