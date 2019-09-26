using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WebSearcher.Entities;

namespace WebSearcher.DataAccess.Context
{
    public class WebSearcherContext<T> : DbContext where T : Entity
    {
        readonly TableNamesForEntitiesProvider _tableNamesForEntitiesProvider = new TableNamesForEntitiesProvider();
        public DbSet<T> Entities { get; set; }

        public WebSearcherContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<T>()
                .ToTable(_tableNamesForEntitiesProvider.GetTableNameForEntityType(typeof(T)));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=AWMACHINE\AWSQL;Initial Catalog=WebSearcher;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}
