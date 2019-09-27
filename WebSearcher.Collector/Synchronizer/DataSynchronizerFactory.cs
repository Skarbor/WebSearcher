using WebSearcher.DataAccess.Abstract;
using WebSearcher.DataAccess.Concrete;
using WebSearcher.Entities;

namespace WebSearcher.Collector.Synchronizer
{
    public class DataSynchronizerFactory : IDataSynchronizerFactory
    {
        private readonly IEntityRepositoryFactory _entityRepositoryFactory;

        public DataSynchronizerFactory() : this(new EntityRepositoryFactory())
        { }

        public DataSynchronizerFactory(IEntityRepositoryFactory entityRepositoryFactory)
        {
            _entityRepositoryFactory = entityRepositoryFactory;
        }

        public DataSynchronizer<T> CreateDataSynchronizer<T>() where T : Entity
        {
            return new DataSynchronizer<T>(_entityRepositoryFactory);
        }
    }
}
