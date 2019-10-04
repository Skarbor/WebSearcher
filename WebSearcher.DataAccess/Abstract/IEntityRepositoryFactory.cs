using WebSearcher.DataAccess.Context;
using WebSearcher.Entities;

namespace WebSearcher.DataAccess.Abstract
{
    public interface IEntityRepositoryFactory
    {
        IEntityRepository<T> CreateEntityRepository<T>() where T : Entity;
        IEntityRepository<T> CreateEntityRepository<T>(WebSearcherContext context) where T : Entity;
    }
}
