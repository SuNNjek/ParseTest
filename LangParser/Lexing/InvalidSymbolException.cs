using System;

namespace LangParser.Lexing
{
	public class InvalidSymbolException : Exception
	{
		private int _position;
		public int Position => _position;

		public InvalidSymbolException(string message, int position, Exception innerException) : base(message, innerException)
		{
			_position = position;
		}
	}
}
