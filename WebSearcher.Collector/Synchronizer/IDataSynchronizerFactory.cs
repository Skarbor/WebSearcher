using WebSearcher.Entities;

namespace WebSearcher.Collector.Synchronizer
{
    public interface IDataSynchronizerFactory
    {
        DataSynchronizer<T> CreateDataSynchronizer<T>() where T : Entity;
    }
}
