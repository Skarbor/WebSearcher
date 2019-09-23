using WebSearcher.DataAccess.Concrete;
using WebSearcher.DataAccess.Context;
using WebSearcher.Entities;

namespace WebSearcher.Collector.Synchronizer
{
    public class WebPageDataSynchronizer : BaseDataSynchronizer<WebPage>
    {
        public WebPageDataSynchronizer() : base(new EntityRepository<WebPage>(new WebPageContext()))
        {}
    }
}
