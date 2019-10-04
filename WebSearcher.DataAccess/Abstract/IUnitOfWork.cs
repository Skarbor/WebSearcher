using System;
using WebSearcher.Entities;

namespace WebSearcher.DataAccess.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IEntityRepository<WebPage> WebPages { get; set; }
        IEntityRepository<WebPageConnection> WebPagesConnections { get; set; }

        void Save();
    }
}
