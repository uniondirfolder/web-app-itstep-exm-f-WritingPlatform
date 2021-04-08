

using System;
using System.Collections.Generic;

namespace WP.DataLayer.Repository
{
    interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Func<T, Boolean> predicate);
        T Get(int? id);
        void Create(T entity);
        void Update(T entity);
        void Delete(int id);
        void SaveChange();
    }
}
