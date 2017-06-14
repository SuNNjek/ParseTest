using System;
using System.Collections.Generic;
using System.Linq;
using LangParser.AST;
using LangParser.Lexing;
using LangParser.Lexing.Tokens;

namespace LangParser.Parsing
{
	public class TokenStream : TokenizableStream<Token>
	{
		private Dictionary<int, Memo> _cachedAst = new Dictionary<int, Memo>();

		public override Token Current => base.Current ?? new EndOfFileToken();

		public TokenStream(Lexer lexer) : base(lexer.Lex().ToList) { }

		public bool IsMatch<TToken>() where TToken : Token => Current is TToken;

		public Ast Capture(Func<Ast> ast)
		{
			if (Alternative(ast))
				return Get(ast);

			return null;
		}

		public Ast Get(Func<Ast> getter)
		{
			if (_cachedAst.TryGetValue(Index, out Memo memo))
			{
				Index = memo.NextIndex;
				return memo.Ast;
			}

			return getter();
		}

		public TToken Take<TToken>() where TToken : Token
		{
			if (IsMatch<TToken>())
				return (TToken)Consume();

			throw new InvalidSyntaxException($"Expected {typeof(TToken).Name} but got {Current.GetType().Name}", null);
		}

		public bool Alternative(Func<Ast> action)
		{
			using (ISnapshot snapshot = TakeSnapshot())
			{
				bool found = false;

				try
				{
					int currIndex = Index;
					Ast ast = action();

					if(ast != null)
					{
						found = true;

						_cachedAst[currIndex] = new Memo(ast, Index);
					}
				}
				catch { }

				snapshot.Rollback();
				return found;
			}
		}

		public override Token Peek(int lookahead) => base.Peek(lookahead) ?? new EndOfFileToken();

		private class Memo
		{
			public Ast Ast { get; set; }
			public int NextIndex { get; set; }

			public Memo(Ast ast, int nextIndex)
			{
				Ast = ast;
				NextIndex = nextIndex;
			}
		}
	}
}
