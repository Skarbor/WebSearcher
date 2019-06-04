using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebSearcher.Entities;

namespace WebSearcher.DataAccess.Context
{
    public abstract class BaseEntityContext<T> : DbContext where T: Entity
    {
        protected abstract string EntityName { get; }

        public DbSet<T> Entities { get; set; }

        public BaseEntityContext() : base()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<T>()
                .ToTable(EntityName);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-OMV8OT1\SQLEXPRESS;Initial Catalog=WebSearcher;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}
