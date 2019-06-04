using System;
using System.Collections.Generic;
using System.Text;
using WebSearcher.Entities;

namespace WebSearcher.DataAccess.Abstract
{
    public interface IEntityRepository<T> where T : Entity
    {
        void Add(T entiy);
        void AddBulk(IEnumerable<T> entities);
        T Get(int id);
        IEnumerable<T> GetAll();
    }
}
