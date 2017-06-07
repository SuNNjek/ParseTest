using ParseTest.Lexing.Matchers;
using ParseTest.Lexing.Tokens;
using System.Collections.Generic;
using System.Linq;

namespace ParseTest.Lexing
{
	public class Lexer
	{
		private Tokenizer _tokenizer;
		private List<Matcher> _matchers;

		public Lexer(string code)
		{
			_tokenizer = new Tokenizer(code);
			_matchers = GetMatchers();
		}

		public IEnumerable<Token> Lex()
		{
			for(Token curr = Next(); curr != null && !(curr is EndOfFileToken); curr = Next())
			{
				if (curr is WhitespaceToken)
					continue;

				yield return curr;
			}
		}

		private Token Next()
		{
			if (_tokenizer.EOS)
				return new EndOfFileToken();

			foreach(Matcher matcher in _matchers)
			{
				if (matcher.IsMatch(_tokenizer, out Token t))
					return t;
			}

			return null;
		}

		protected List<Matcher> GetMatchers()
		{
			List<Matcher> matchers = new List<Matcher>();

			matchers.Add(new StringMatcher());

			List<SpecialMatcher> symbols = new List<SpecialMatcher>
			{
				new SpecialMatcher<LeftParensToken>("(", true),
				new SpecialMatcher<RightParensToken>(")", true),
				new SpecialMatcher<LeftBracketToken>("{", true),
				new SpecialMatcher<RightBracketToken>("}", true),
				new SpecialMatcher<PlusToken>("+", true),
				new SpecialMatcher<MinusToken>("-", true),
				new SpecialMatcher<MultiplyToken>("*", true),
				new SpecialMatcher<DivisionToken>("/", true),
				new SpecialMatcher<EqualsToken>("=", true),
				new SpecialMatcher<SemicolonToken>(";", true)
			};

			matchers.AddRange(symbols);
			matchers.Add(new SpecialMatcher<IfToken>("if", false, symbols.Select(d => d.Match)));
			matchers.Add(new SpecialMatcher<ElseToken>("else", false, symbols.Select(d => d.Match)));
			matchers.Add(new SpecialMatcher<WhileToken>("while", false, symbols.Select(d => d.Match)));
			matchers.Add(new SpecialMatcher<ForToken>("for", false, symbols.Select(d => d.Match)));
			matchers.Add(new SpecialMatcher<TrueToken>("true", false, symbols.Select(d => d.Match)));
			matchers.Add(new SpecialMatcher<FalseToken>("false", false, symbols.Select(d => d.Match)));

			matchers.Add(new WhitespaceMatcher());
			matchers.Add(new NumberMatcher());
			matchers.Add(new IdentifierMatcher());

			return matchers;
		}
	}
}
