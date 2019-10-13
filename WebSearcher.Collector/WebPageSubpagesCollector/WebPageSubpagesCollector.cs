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

        private void ResolveSubPagesForWebPage(WebPage webPage)
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
                unitOfWork.WebPages.Get(webPage.Id); //EF requires this
                unitOfWork.WebPages.AddBulk(linkedWebPages);
                unitOfWork.WebPagesConnections.AddBulk(webPageToLinkedWebPagesConnections);
                unitOfWork.Save();
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
