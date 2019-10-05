using HtmlParser.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using WebSearcher.Collector.Extensions;
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
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        private readonly ILogger _logger = new Logger();
        private readonly HtmlParser.HtmlParser _htmlParser = new HtmlParser.HtmlParser();

        public WebPageSubPagesCollector() : this(new WebPageUrlChecker(), new WebPageContentGetter(), new UnitOfWorkFactory())
        {}

        public WebPageSubPagesCollector(IWebPageUrlChecker webPageUrlChecker, IWebPageContentGetter webPageContentGetter, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _webPageUrlChecker = webPageUrlChecker;
            _webPageContentGetter = webPageContentGetter;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        private async void ResolveSubPagesForWebPage(WebPage webPage)
        {
            var linkedWebPages = GetLinksFromWebPage(webPage)
                .GetWorkingLinks()
                .GetNewLinks()
                .CreateWebPagesFromLinks()
                .ToList();

            if (!linkedWebPages.Any())
                return;

            var webPageToLinkedWebPagesConnections = webPage.CreateConnections(linkedWebPages).GetNewConnections();

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                unitOfWork.WebPages.AddBulk(linkedWebPages);
                unitOfWork.Save();

                unitOfWork.WebPagesConnections.AddBulk(webPageToLinkedWebPagesConnections);
                unitOfWork.Save();
            }
        }

        private async void ResolveSubPagesForWebPage2(WebPage webPage)
        {
            try
            {
                var links = GetLinksFromWebPage(webPage);

                foreach (var link in links)
                {
                    var isWebPageWorking = await _webPageUrlChecker.CheckIfPageIsWorkingAsync(link.Url);

                    if (isWebPageWorking)
                    {
                        using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
                        {
                            var webPageFromLink = unitOfWork.WebPages.Find(_webPage => _webPage.Url == link.Url);

                            if (webPageFromLink == null)
                            {
                                _logger.Debug($"Found working webpage with url: {link.Url}");
                                webPageFromLink = new WebPage() { Url = link.Url };
                                unitOfWork.WebPages.Add(webPageFromLink);

                                _logger.Debug($"Adding connection between {webPage.Url} and {link.Url} to database");
                                unitOfWork.WebPagesConnections.Add(new WebPageConnection() { WebPageFrom = webPage, WebPageTo = webPageFromLink });
                            }
                            else
                            {
                                var connection = unitOfWork.WebPagesConnections.Find(_webPageConnection => _webPageConnection.WebPageFrom.Url == webPage.Url 
                                                && _webPageConnection.WebPageTo.Url == webPageFromLink.Url);

                                if (connection == null)
                                {
                                    connection = new WebPageConnection { WebPageFrom = webPage, WebPageTo = webPageFromLink };
                                    unitOfWork.WebPagesConnections.Add(connection);
                                }
                            }

                            unitOfWork.Save();
                        }
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

        private IEnumerable<Link> GetLinksFromWebPage(WebPage webPage)
        {
            var content = _webPageContentGetter.GetWebPageContent(webPage.Url);
            return _htmlParser.Parse(content).Links;
        }

        public void Start()
        {
            while (true)
            {
                IEnumerable<WebPage> allWebPages;
                using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
                {
                    allWebPages = new List<WebPage>(unitOfWork.WebPages.GetAll());
                }
                    
                foreach (var webPage in allWebPages)
                {
                    ResolveSubPagesForWebPage(webPage);
                }
            }
        }
    }
}
