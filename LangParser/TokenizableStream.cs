using System;
using System.Collections.Generic;

namespace LangParser
{
	public abstract class TokenizableStream<T> where T : class
	{
		private List<T> _items;
		private Stack<int> _snapshotIndexes;

		public int Index { get; protected set; }
		public virtual T Current => IsEOS(0) ? null : _items[Index];
		public bool EOS => IsEOS(0);

		protected TokenizableStream(Func<IEnumerable<T>> extractor)
		{
			_items = new List<T>(extractor());
			_snapshotIndexes = new Stack<int>();
		}

		protected bool IsEOS(int lookahead)
		{
			if (Index + lookahead >= _items.Count)
				return true;
			return false;
		}

		public T Consume() => _items[Index++];
		public virtual T Peek(int lookahead) => IsEOS(lookahead) ? null : _items[Index + lookahead];

		public ISnapshot TakeSnapshot() => new Snapshot(this);

		private class Snapshot : ISnapshot
		{
			bool _commited = false;
			TokenizableStream<T> _stream;

			public Snapshot(TokenizableStream<T> stream)
			{
				_stream = stream;

				_stream._snapshotIndexes.Push(_stream.Index);
			}

			public void Commit() => _commited = true;
			public void Rollback() => _commited = false;

			public void Dispose()
			{
				if (_commited)
					_stream._snapshotIndexes.Pop();
				else
					_stream.Index = _stream._snapshotIndexes.Pop();
			}
		}
	}
}
