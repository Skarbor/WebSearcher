using WebSearcher.Entities;

namespace WebSearcher.DataAccess.Abstract
{
    public interface IEntityRepositoryFactory
    {
        IEntityRepository<T> CreateEntityRepository<T>() where T : Entity;
    }
}
