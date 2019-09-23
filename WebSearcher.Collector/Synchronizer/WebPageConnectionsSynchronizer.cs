using WebSearcher.DataAccess.Concrete;
using WebSearcher.DataAccess.Context;
using WebSearcher.Entities;

namespace WebSearcher.Collector.Synchronizer
{
    public class WebPageConnectionsSynchronizer : BaseDataSynchronizer<WebPageConnections>
    {
        public WebPageConnectionsSynchronizer() : base(new EntityRepository<WebPageConnections>(new WebPageConnectionsContext()))
        { }
    }
}
