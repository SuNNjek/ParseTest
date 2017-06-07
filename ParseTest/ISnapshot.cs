using System;

namespace ParseTest
{
	public interface ISnapshot : IDisposable
	{
		void Commit();
		void Rollback();
	}
}
