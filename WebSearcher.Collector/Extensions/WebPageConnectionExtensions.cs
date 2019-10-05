using System.Collections.Generic;
using System.Linq;
using WebSearcher.DataAccess.Abstract;
using WebSearcher.DataAccess.Concrete;
using WebSearcher.Entities;

namespace WebSearcher.Collector.Extensions
{
    public static class WebPageConnectionExtensions
    {
        private static readonly IUnitOfWorkFactory UnitOfWorkFactory = new UnitOfWorkFactory();

        public static IEnumerable<WebPageConnection> GetNewConnections(this IEnumerable<WebPageConnection> webPageConnections)
        {
            using (var unitOfWork = UnitOfWorkFactory.Create())
            {
                return webPageConnections.Where(webPageConnection =>
                    unitOfWork.WebPagesConnections.Find(connection =>
                        connection.WebPageFrom == webPageConnection.WebPageFrom &&
                        connection.WebPageTo == webPageConnection.WebPageTo) == null).ToList();
            }
        }
    }
}
