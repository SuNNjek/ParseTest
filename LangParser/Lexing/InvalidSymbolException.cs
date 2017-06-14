using System;

namespace LangParser.Lexing
{
	public class InvalidSymbolException : Exception
	{
		public InvalidSymbolException(string message, Exception innerException) : base(message, innerException) { }
	}
}
