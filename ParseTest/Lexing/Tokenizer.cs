using System.Linq;

namespace ParseTest.Lexing
{
	public class Tokenizer : TokenizableStream<string>
	{
		public Tokenizer(string code) : base(() => code.ToCharArray().Select(d => d.ToString())) { }
	}
}
