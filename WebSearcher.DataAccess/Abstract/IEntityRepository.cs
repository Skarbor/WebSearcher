using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebSearcher.Entities;

namespace WebSearcher.DataAccess.Abstract
{
    public interface IEntityRepository<TEntity> where TEntity : Entity
    {
        void Add(TEntity entiy);
        TEntity Find(Expression<Func<TEntity, bool>> expression);
        void AddBulk(IEnumerable<TEntity> entities);
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        void Save();
    }
}
