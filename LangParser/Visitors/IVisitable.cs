namespace LangParser.Visitors
{
	public interface IVisitable<TVisitable> where TVisitable : IVisitable<TVisitable>
	{
		void Accept(IVisitor<TVisitable> visitor);
	}
}
