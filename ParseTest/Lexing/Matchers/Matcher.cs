using ParseTest.Lexing.Tokens;

namespace ParseTest.Lexing.Matchers
{
	public abstract class Matcher
	{
		protected abstract Token GetToken(Tokenizer tokenizer);

		public bool IsMatch(Tokenizer tokenizer, out Token token)
		{
			if (tokenizer.EOS)
			{
				token = new EndOfFileToken();
				return true;
			}

			using (ISnapshot snapshot = tokenizer.TakeSnapshot())
			{
				token = GetToken(tokenizer);

				if (token == null)
					snapshot.Rollback();
				else
					snapshot.Commit();

				return token != null;
			}
		}
	}
}
