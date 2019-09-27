using System.Collections.Generic;
using WebSearcher.Common.Logger;
using WebSearcher.DataAccess.Abstract;
using WebSearcher.Entities;

namespace WebSearcher.Collector.Synchronizer
{
    public class DataSynchronizer<T> where T : Entity
    {
        protected readonly ILogger Logger = new Logger();
        protected readonly IEntityRepository<T> EntityRepository;

        private IList<T> _synchronizedData;
        private IList<T> _addedDataNotYetSynchronized = new List<T>();
        private static readonly object SyncObject = new object();

        public DataSynchronizer(IEntityRepositoryFactory entityRepositoryFactory)
        {
            EntityRepository = entityRepositoryFactory.CreateEntityRepository<T>();
            Initialize();
        }

        private void Initialize()
        {
            Logger.Info($"Initializing data synchronizer for {typeof(T)} entity...");

            _synchronizedData = new List<T>(EntityRepository.GetAll());
        }

        public virtual void AddIfUnique(T entity)
        {
            lock (SyncObject)
            {
                if (!_synchronizedData.Contains(entity) && !_addedDataNotYetSynchronized.Contains(entity))
                {
                    _addedDataNotYetSynchronized.Add(entity);
                }
            }

            if (ShouldSync())
            {
                Sync();
            }
        }

        public virtual void Sync()
        {
            Logger.Debug($"Synchronize with data source for {typeof(T)} entity...");

            lock (SyncObject)
            {
                EntityRepository.AddBulk(_addedDataNotYetSynchronized);
                ((List<T>)_synchronizedData).AddRange(_addedDataNotYetSynchronized);
                _addedDataNotYetSynchronized = new List<T>();
            }
        }

        private bool ShouldSync()
        {
            lock (SyncObject)
            {
                if (_addedDataNotYetSynchronized.Count > 10) return true;
            }

            return false;
        }
    }
}
