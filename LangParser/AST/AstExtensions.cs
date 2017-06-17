using System;
using System.Collections.Generic;
using System.Text;

namespace LangParser.AST
{
	public static class AstExtensions
	{
		public static Scope ToScope(this Ast ast) => (ast as Scope) ?? new Scope(new Ast[] { ast });
	}
}
