namespace LangParser.Visitors
{
	public interface IVisitor<TVisitable> where TVisitable : IVisitable<TVisitable>
    {
		void Visit(TVisitable visitable);
    }
}
