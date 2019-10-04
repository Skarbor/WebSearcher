using Microsoft.EntityFrameworkCore;
using WebSearcher.Entities;

namespace WebSearcher.DataAccess.Context
{
    public class WebSearcherContext : DbContext
    {
        public DbSet<WebPage> WebPages { get; set; }
        public DbSet<WebPageConnection> WebPageConnections { get; set; }

        public WebSearcherContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-OMV8OT1\SQLEXPRESS;Initial Catalog=WebSearcher;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            //optionsBuilder.UseSqlServer(@"Data Source=AWMACHINE\AWSQL;Initial Catalog=WebSearcher;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}
