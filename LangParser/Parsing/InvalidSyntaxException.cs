using System;

namespace LangParser.Parsing
{
	public class InvalidSyntaxException : Exception
	{
		public InvalidSyntaxException(string message, Exception innerException) : base(message, innerException) { }
	}
}
