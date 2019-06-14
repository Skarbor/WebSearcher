using System;
using System.Collections.Generic;
using System.Text;
using WebSearcher.Collector.Synchronizer;
using WebSearcher.Collector.WebPageUrlCollector;
using WebSearcher.DataAccess.Concrete;
using WebSearcher.DataAccess.Context;
using WebSearcher.Entities;

namespace WebSearcher.Collector.WebPageSubpagesCollector
{
    public class WebPageSubpagesCollector : ICollector
    {
        private readonly IWebPageUrlChecker _webPageUrlChecker;
        private readonly EntityRepository<WebPage> _entityRepository = new EntityRepository<WebPage>(new WebPageContext());
        private readonly WebPageContentGetter _webPageContentGetter = new WebPageContentGetter();
        public WebPageSubpagesCollector() : this(new WebPageUrlChecker())

        {}

        public WebPageSubpagesCollector(IWebPageUrlChecker webPageUrlChecker)
        {
            _webPageUrlChecker = webPageUrlChecker;
        }


        private void ResolveSubpagesForWebPage(WebPage webPage)
        {
            var content = _webPageContentGetter.GetWebpageContent(webPage.Url);
        }

        public void Start()
        {
            foreach (var item in _entityRepository.GetAll())
            {
                ResolveSubpagesForWebPage(item);
            }
        }
    }
}
