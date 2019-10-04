using System;
using WebSearcher.DataAccess.Abstract;
using WebSearcher.DataAccess.Context;
using WebSearcher.Entities;

namespace WebSearcher.DataAccess.Concrete
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private WebSearcherContext _webSearcherContext;

        public IEntityRepository<WebPage> WebPages { get; set; }
        public IEntityRepository<WebPageConnection> WebPagesConnections { get; set; }

        public UnitOfWork() : this(new EntityRepositoryFactory())
        {
        }

        public UnitOfWork(IEntityRepositoryFactory entityRepositoryFactory)
        {
            _webSearcherContext = new WebSearcherContext();
            this.WebPages = entityRepositoryFactory.CreateEntityRepository<WebPage>(_webSearcherContext);
            this.WebPagesConnections = entityRepositoryFactory.CreateEntityRepository<WebPageConnection>(_webSearcherContext);
        }

        public void Save()
        {
            _webSearcherContext.SaveChanges();
        }

        public void Dispose()
        {
            _webSearcherContext.Dispose();
        }
    }
}
