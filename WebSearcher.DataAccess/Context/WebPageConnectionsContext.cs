using WebSearcher.Entities;

namespace WebSearcher.DataAccess.Context
{
    public class WebPageConnectionsContext : BaseEntityContext<WebPageConnections>
    {
        protected override string EntityName { get => "WebPageConnections"; }
    }
}
