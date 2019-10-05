using System.Collections.Generic;
using System.Linq;
using HtmlParser.Model;
using WebSearcher.Common;
using WebSearcher.DataAccess.Abstract;
using WebSearcher.DataAccess.Concrete;
using WebSearcher.Entities;

namespace WebSearcher.Collector.Extensions
{
    public static class LinkExtensions
    {
        private static readonly IUnitOfWorkFactory UnitOfWorkFactory = new UnitOfWorkFactory();
        public static IEnumerable<Link> GetWorkingLinks(this IEnumerable<Link> links)
        {
            var webPageUrlChecker = new WebPageUrlChecker();
            return links.Where(link => webPageUrlChecker.CheckIfPageIsWorking(link.Url)).AsEnumerable();
        }

        public static IEnumerable<Link> GetNewLinks(this IEnumerable<Link> links)
        {
            using (var unitOfWork = UnitOfWorkFactory.Create())
            {
                return links.Where(link => unitOfWork.WebPages.Find(_link => _link.Url == link.Url) == null)
                    .ToList();
            }
        }

        public static IEnumerable<WebPage> CreateWebPagesFromLinks(this IEnumerable<Link> links)
        {
            return links.Select(link => new WebPage {Url = link.Url});
        }
    }
}
