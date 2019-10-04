namespace WebSearcher.DataAccess.Abstract
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}
