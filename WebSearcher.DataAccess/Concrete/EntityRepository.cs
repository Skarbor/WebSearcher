using System;
using System.Collections.Generic;
using System.Text;
using WebSearcher.DataAccess.Abstract;
using WebSearcher.DataAccess.Context;
using WebSearcher.Entities;
using System.Linq;

namespace WebSearcher.DataAccess.Concrete
{
    public class EntityRepository<T>: IEntityRepository<T> where T: Entity
    {
        private readonly BaseEntityContext<T> _baseEntityContext;

        public EntityRepository(BaseEntityContext<T> baseEntityContext)
        {
            _baseEntityContext = baseEntityContext;
        }

        public virtual void Add(T entity)
        {
            _baseEntityContext.Entities.Add(entity);
            _baseEntityContext.SaveChanges();
        }

        public virtual void AddBulk(IEnumerable<T> entities)
        {
            _baseEntityContext.Entities.AddRange(entities);
            _baseEntityContext.SaveChanges();
        }

        public virtual T Get(int id)
        {
            return (T) _baseEntityContext.Entities.Single(entity => entity.Id == id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return (IEnumerable<T>) _baseEntityContext.Entities;
        }
    }
}
