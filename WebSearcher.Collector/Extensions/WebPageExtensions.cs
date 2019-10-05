using System.Collections.Generic;
using System.Linq;
using WebSearcher.Entities;

namespace WebSearcher.Collector.Extensions
{
    public static class WebPageExtensions
    {
        public static IEnumerable<WebPageConnection> CreateConnections(this WebPage webPage, IEnumerable<WebPage> webPages)
        {
            return webPages.Select(toWebPage => new WebPageConnection {WebPageFrom = webPage, WebPageTo = toWebPage});
        }
    }
}
