using System.Collections.Generic;
using WebSearcher.Common.Logger;
using WebSearcher.DataAccess.Abstract;
using WebSearcher.Entities;

namespace WebSearcher.Collector.Synchronizer
{
    public abstract class BaseDataSynchronizer<T> where T: Entity
    {
        private static readonly object SyncObject = new object();

        protected readonly ILogger _logger = new Logger();
        protected readonly IEntityRepository<T> _entityRepository;

        private IList<T> _synchronizedData;
        private IList<T> _addedDataNotYetSynchronized = new List<T>();

        public BaseDataSynchronizer(IEntityRepository<T> entityRepository)
        {
            _entityRepository = entityRepository;
            Initialize();
        }

        private void Initialize()
        {
            _logger.Info($"Initializing data synchronizer for {typeof(T)} entity...");

            _synchronizedData = new List<T>(_entityRepository.GetAll());
        }

        public virtual void AddIfUniqe(T entity)
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
            _logger.Debug($"Synchronize with data sorce for {typeof(T)} entity...");

            lock (SyncObject)
            {
                _entityRepository.AddBulk(_addedDataNotYetSynchronized);
                (_synchronizedData as List<T>).AddRange(_addedDataNotYetSynchronized);
            }
            _addedDataNotYetSynchronized = new List<T>();
        }

        private bool ShouldSync()
        {
            if (_addedDataNotYetSynchronized.Count > 10) return true;

            return false;
        }
    }
}
