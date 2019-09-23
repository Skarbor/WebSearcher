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
        private readonly HtmlParser.HtmlParser _htmlParser = new HtmlParser.HtmlParser();
        private readonly EntityRepository<WebPage> _entityRepository = new EntityRepository<WebPage>(new WebPageContext());
        private readonly WebPageContentGetter _webPageContentGetter = new WebPageContentGetter();
        private WebPageDataSynchronizer _webPageDataSynchronizer { get; set; }
        private WebPageConnectionsSynchronizer _webPageConnectionsSynchronizer { get; set; }

        public WebPageSubpagesCollector() : this(new WebPageUrlChecker())
        {}

        public WebPageSubpagesCollector(IWebPageUrlChecker webPageUrlChecker)
        {
            _webPageUrlChecker = webPageUrlChecker;
            _webPageDataSynchronizer = new WebPageDataSynchronizer();
            _webPageConnectionsSynchronizer = new WebPageConnectionsSynchronizer();
        }


        private async void ResolveSubpagesForWebPage(WebPage webPage)
        {
            try
            {
                var content = _webPageContentGetter.GetWebpageContent(webPage.Url);
                var links = _htmlParser.Parse(content).Links;

                foreach (var link in links)
                {
                    var isWebPageWorking = await _webPageUrlChecker.CheckIfPageIsWorkingAsync(link.Url);

                    if (isWebPageWorking)
                    {
                        Console.WriteLine($"Found working webpage with url: {link.Url}");
                        _webPageDataSynchronizer.AddIfUniqe(new Entities.WebPage() { Url = link.Url });

                        Console.WriteLine($"Adding connection betwen {webPage.Url} and {link.Url} to database");
                        _webPageConnectionsSynchronizer.AddIfUniqe(new WebPageConnections() { WebPageFromId = webPage.Id, WebPageToUrl = link.Url });
                    }
                    else
                    {
                        Console.WriteLine($"Url: {link.Url} not wokring");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured: {ex.Message}");
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
