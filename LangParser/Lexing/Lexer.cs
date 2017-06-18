using LangParser.Lexing.Matchers;
using LangParser.Lexing.Tokens;
using System.Collections.Generic;
using System.Linq;

namespace LangParser.Lexing
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
				new SpecialMatcher<LeftSquareBracketToken>("[", true),
				new SpecialMatcher<RightSquareBracketToken>("]", true),
				new SpecialMatcher<EqualsToken>("==", true),
				new SpecialMatcher<LessOrEqualToken>("<=", true),
				new SpecialMatcher<LessThanToken>("<", true),
				new SpecialMatcher<GreaterOrEqualToken>(">=", true),
				new SpecialMatcher<GreaterThanToken>(">", true),
				new SpecialMatcher<NotEqualToken>("!=", true),
				new SpecialMatcher<NegationToken>("!", true),
				new SpecialMatcher<AndToken>("&&", true),
				new SpecialMatcher<OrToken>("||", true),
				new SpecialMatcher<XorToken>("^", true),
				new SpecialMatcher<PlusToken>("+", true),
				new SpecialMatcher<MinusToken>("-", true),
				new SpecialMatcher<MultiplyToken>("*", true),
				new SpecialMatcher<DivisionToken>("/", true),
				new SpecialMatcher<AssignmentToken>("=", true),
				new SpecialMatcher<CommaToken>(",", true),
				new SpecialMatcher<SemicolonToken>(";", true)
			};

			matchers.AddRange(symbols);

			IEnumerable<string> delimiters = symbols.Select(d => d.Match);
			matchers.Add(new SpecialMatcher<IfToken>("if", false, delimiters));
			matchers.Add(new SpecialMatcher<ElseToken>("else", false, delimiters));
			matchers.Add(new SpecialMatcher<DoToken>("do", false, delimiters));
			matchers.Add(new SpecialMatcher<WhileToken>("while", false, delimiters));
			matchers.Add(new SpecialMatcher<ForToken>("for", false, delimiters));
			matchers.Add(new SpecialMatcher<TrueToken>("true", false, delimiters));
			matchers.Add(new SpecialMatcher<FalseToken>("false", false, delimiters));
			matchers.Add(new SpecialMatcher<VoidToken>("void", false, delimiters));
			matchers.Add(new SpecialMatcher<IntToken>("int", false, delimiters));
			matchers.Add(new SpecialMatcher<FloatToken>("float", false, delimiters));
			matchers.Add(new SpecialMatcher<BoolToken>("bool", false, delimiters));
			matchers.Add(new SpecialMatcher<StringKeywordToken>("string", false, delimiters));
			matchers.Add(new SpecialMatcher<ObjectToken>("object", false, delimiters));
			matchers.Add(new SpecialMatcher<ByteToken>("byte", false, delimiters));
			matchers.Add(new SpecialMatcher<ReturnToken>("return", false, delimiters));
			matchers.Add(new SpecialMatcher<BreakToken>("break", false, delimiters));
			matchers.Add(new SpecialMatcher<ContinueToken>("continue", false, delimiters));

			matchers.Add(new WhitespaceMatcher());
			matchers.Add(new NumberMatcher());
			matchers.Add(new IdentifierMatcher());

			return matchers;
		}
	}
}
