using System;
using System.Collections.Generic;
using System.Text;
using WebSearcher.Common.Logger;
using WebSearcher.DataAccess.Abstract;
using WebSearcher.DataAccess.Context;
using WebSearcher.Entities;

namespace WebSearcher.Collector.Synchronizer
{
    public abstract class BaseDataSynchronizer<T> where T: Entity
    {
        private static readonly object SyncObject = new object();

        protected readonly ILogger _logger = new Logger();
        protected readonly IEntityRepository<T> _entityRepository;

        private IList<T> _data;
        private IList<T> _addedDataNotYetSync = new List<T>();

        public BaseDataSynchronizer(IEntityRepository<T> entityRepository)
        {
            _entityRepository = entityRepository;
            Initialize();
        }

        private void Initialize()
        {
            _logger.Info($"Initializing data synchronizer for {typeof(T)} entity...");

            _data = new List<T>(_entityRepository.GetAll());
        }

        public virtual void AddIfUniqe(T entity)
        {
            lock (SyncObject)
            {
                if (!_data.Contains(entity) && !_addedDataNotYetSync.Contains(entity))
                {
                    _addedDataNotYetSync.Add(entity);
                }
            }

            if (ShouldSync())
            {
                Sync();
            }
        }

        public virtual void Sync()
        {
            _logger.Info($"Synchronize with data sorce for {typeof(T)} entity...");

            lock (SyncObject)
            {
                _entityRepository.AddBulk(_addedDataNotYetSync);
                (_data as List<T>).AddRange(_addedDataNotYetSync);
            }
            _addedDataNotYetSync = new List<T>();
        }

        private bool ShouldSync()
        {
            if (_addedDataNotYetSync.Count > 10) return true;

            return false;
        }
    }
}
