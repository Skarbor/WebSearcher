using System;
using WebSearcher.Collector.Synchronizer;
using WebSearcher.Common;
using WebSearcher.Common.Logger;
using WebSearcher.DataAccess.Abstract;
using WebSearcher.DataAccess.Concrete;
using WebSearcher.Entities;

namespace WebSearcher.Collector.WebPageSubPagesCollector
{
    public class WebPageSubPagesCollector : ICollector
    {
        private readonly IWebPageUrlChecker _webPageUrlChecker;
        private readonly IWebPageContentGetter _webPageContentGetter;
        private readonly ILogger _logger = new Logger();
        private readonly HtmlParser.HtmlParser _htmlParser = new HtmlParser.HtmlParser();
        private readonly IEntityRepository<WebPage> _entityRepository;
        private readonly DataSynchronizer<WebPage> _webPagsSynchronizer;
        private readonly DataSynchronizer<WebPageConnection> _webPageConnectionsSynchronizer;

        public WebPageSubPagesCollector() : this(new WebPageUrlChecker(), new WebPageContentGetter(), new EntityRepositoryFactory(), new DataSynchronizerFactory())
        {}

        public WebPageSubPagesCollector(IWebPageUrlChecker webPageUrlChecker, IWebPageContentGetter webPageContentGetter, IEntityRepositoryFactory entityRepositoryFactory, IDataSynchronizerFactory dataSynchronizerFactory)
        {
            _webPageUrlChecker = webPageUrlChecker;
            _webPageContentGetter = webPageContentGetter;

            _entityRepository = entityRepositoryFactory.CreateEntityRepository<WebPage>();
            _webPagsSynchronizer = dataSynchronizerFactory.CreateDataSynchronizer<WebPage>();
            _webPageConnectionsSynchronizer = dataSynchronizerFactory.CreateDataSynchronizer<WebPageConnection>();
        }


        private async void ResolveSubPagesForWebPage(WebPage webPage)
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
                        _webPagsSynchronizer.AddIfUnique(new WebPage() { Url = link.Url });

                        _logger.Debug($"Adding connection between {webPage.Url} and {link.Url} to database");
                        _webPageConnectionsSynchronizer.AddIfUnique(new WebPageConnection() { WebPageFromId = webPage.Id, WebPageToUrl = link.Url });
                    }
                    else
                    {
                        _logger.Debug($"Url: {link.Url} not working");
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
                ResolveSubPagesForWebPage(item);
            }
        }
    }
}
