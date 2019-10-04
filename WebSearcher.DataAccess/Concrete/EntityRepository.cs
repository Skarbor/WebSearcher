using System.Collections.Generic;
using WebSearcher.DataAccess.Abstract;
using WebSearcher.DataAccess.Context;
using WebSearcher.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System;

namespace WebSearcher.DataAccess.Concrete
{
    public class EntityRepository<TEntity> : IEntityRepository<TEntity> where TEntity : Entity
    {
        private readonly WebSearcherContext _webSearcherContext;
        private DbSet<TEntity> dbSet;

        public EntityRepository(WebSearcherContext webSearcherContext)
        {
            _webSearcherContext = webSearcherContext;
            dbSet = webSearcherContext.Set<TEntity>();
        }

        public virtual void Add(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual TEntity Find(Expression<Func<TEntity,bool>> expression)
        {
            return dbSet.FirstOrDefault(expression);
        }

        public virtual void AddBulk(IEnumerable<TEntity> entities)
        {
            dbSet.AddRange(entities);        
        }

        public virtual TEntity Get(int id)
        {
            return dbSet.Single(entity => entity.Id == id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return dbSet;
        }

        public void Save()
        {
            _webSearcherContext.SaveChanges();
        }
    }
}
