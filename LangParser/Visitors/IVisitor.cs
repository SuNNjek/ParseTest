namespace LangParser.Visitors
{
	public interface IVisitor<TVisitable> where TVisitable : IVisitable<TVisitable>
    {
		void Start(IVisitable visitable);
		void Visit(TVisitable visitable);
    }
}
