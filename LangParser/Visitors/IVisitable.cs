namespace LangParser.Visitors
{
	public interface IVisitable { }

	public interface IVisitable<TVisitable> : IVisitable where TVisitable : IVisitable<TVisitable>
	{
		void Accept(IVisitor<TVisitable> visitor);
	}
}
