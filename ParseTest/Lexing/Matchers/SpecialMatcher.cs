using ParseTest.Lexing.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParseTest.Lexing.Matchers
{
	public abstract class SpecialMatcher : Matcher
	{
		public abstract string Match { get; }
	}

	public class SpecialMatcher<TToken> : SpecialMatcher where TToken : Token, new()
	{
		private string _match;
		public bool _allowAsSubstring = true;

		public override string Match => _match;
		public List<string> Delimiters { get; set; } = new List<string>();

		public SpecialMatcher(string match, bool allowAsSubstring)
		{
			_match = match;
			_allowAsSubstring = allowAsSubstring;
		}

		public SpecialMatcher(string match, bool allowAsSubstring, IEnumerable<string> delimiters) : this(match, allowAsSubstring)
		{
			Delimiters = new List<string>(delimiters);
		}

		protected override Token GetToken(Tokenizer tokenizer)
		{
			foreach(char c in _match)
			{
				if (tokenizer.Current == c.ToString())
					tokenizer.Consume();
				else
					return null;
			}

			bool found = true;
			if(!_allowAsSubstring)
			{
				string next = tokenizer.Current;

				found = String.IsNullOrWhiteSpace(next) || Delimiters.Any(d => d == next);
			}

			if (found)
				return new TToken();

			return null;
		}
	}
}
