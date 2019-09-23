using System;
using WebSearcher.Collector.Synchronizer;
using WebSearcher.Collector.WebPageUrlCollector;
using WebSearcher.Common.Logger;
using WebSearcher.DataAccess.Concrete;
using WebSearcher.DataAccess.Context;
using WebSearcher.Entities;

namespace WebSearcher.Collector.WebPageSubpagesCollector
{
    public class WebPageSubpagesCollector : ICollector
    {
        private readonly IWebPageUrlChecker _webPageUrlChecker;
        private readonly IWebPageContentGetter _webPageContentGetter;
        protected readonly ILogger _logger = new Logger();
        private readonly HtmlParser.HtmlParser _htmlParser = new HtmlParser.HtmlParser();
        private readonly EntityRepository<WebPage> _entityRepository = new EntityRepository<WebPage>(new WebPageContext());
        
        private WebPageDataSynchronizer _webPageDataSynchronizer { get; set; }
        private WebPageConnectionsSynchronizer _webPageConnectionsSynchronizer { get; set; }

        public WebPageSubpagesCollector() : this(new WebPageUrlChecker(), new WebPageContentGetter())
        {}

        public WebPageSubpagesCollector(IWebPageUrlChecker webPageUrlChecker, IWebPageContentGetter webPageContentGetter)
        {
            _webPageUrlChecker = webPageUrlChecker;
            _webPageContentGetter = webPageContentGetter;
            _webPageDataSynchronizer = new WebPageDataSynchronizer();
            _webPageConnectionsSynchronizer = new WebPageConnectionsSynchronizer();
        }


        private async void ResolveSubpagesForWebPage(WebPage webPage)
        {
            try
            {
                var content = _webPageContentGetter.GetWebPageContent(webPage.Url);
                var links = _htmlParser.Parse(content).Links;

                foreach (var link in links)
                {
                    var isWebPageWorking = await _webPageUrlChecker.CheckIfPageIsWorkingAsync(link.Url);

                    if (isWebPageWorking)
                    {
                        _logger.Debug($"Found working webpage with url: {link.Url}");
                        _webPageDataSynchronizer.AddIfUniqe(new Entities.WebPage() { Url = link.Url });

                        _logger.Debug($"Adding connection betwen {webPage.Url} and {link.Url} to database");
                        _webPageConnectionsSynchronizer.AddIfUniqe(new WebPageConnections() { WebPageFromId = webPage.Id, WebPageToUrl = link.Url });
                    }
                    else
                    {
                        _logger.Debug($"Url: {link.Url} not wokring");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Error occured: {ex.Message}");
            }
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
