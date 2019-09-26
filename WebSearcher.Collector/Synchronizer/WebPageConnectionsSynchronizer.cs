using WebSearcher.DataAccess.Concrete;
using WebSearcher.DataAccess.Context;
using WebSearcher.Entities;

namespace WebSearcher.Collector.Synchronizer
{
    public class WebPageConnectionsSynchronizer : BaseDataSynchronizer<WebPageConnection>
    {
        public WebPageConnectionsSynchronizer() : base(new EntityRepository<WebPageConnection>(new WebSearcherContext<WebPageConnection>()))
        { }
    }
}
