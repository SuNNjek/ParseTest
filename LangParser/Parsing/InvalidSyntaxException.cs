using System;

namespace LangParser.Parsing
{
	public class InvalidSyntaxException : Exception
	{
		private int _position;
		public int Position => _position;

		public InvalidSyntaxException(string message, int position, Exception innerException) : base(message, innerException)
		{
			_position = position;
		}
	}
}
