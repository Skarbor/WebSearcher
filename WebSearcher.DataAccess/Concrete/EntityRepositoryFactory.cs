using WebSearcher.DataAccess.Abstract;
using WebSearcher.DataAccess.Context;
using WebSearcher.Entities;

namespace WebSearcher.DataAccess.Concrete
{
    public class EntityRepositoryFactory : IEntityRepositoryFactory
    {
        public IEntityRepository<T> CreateEntityRepository<T>() where T : Entity
        {
            return new EntityRepository<T>(new WebSearcherContext<T>());
        }
    }
}
