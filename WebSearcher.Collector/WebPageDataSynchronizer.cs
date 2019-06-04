using System;
using System.Collections.Generic;
using System.Text;
using WebSearcher.DataAccess.Abstract;
using WebSearcher.DataAccess.Concrete;
using WebSearcher.DataAccess.Context;
using WebSearcher.Entities;

namespace WebSearcher.Collector
{
    public class WebPageDataSynchronizer : BaseDataSynchronizer<WebPage>
    {
        public WebPageDataSynchronizer() : base(new EntityRepository<WebPage>(new WebPageContext()))
        {}
    }
}
