using System;

namespace LangParser
{
	public interface ISnapshot : IDisposable
	{
		void Commit();
		void Rollback();
	}
}
