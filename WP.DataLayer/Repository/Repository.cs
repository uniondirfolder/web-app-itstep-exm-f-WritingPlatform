using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WP.DataLayer.Entities;

namespace WP.DataLayer.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private ModelDb _db;
        private DbSet<T> _table = null;

        public Repository(ModelDb db)
        {
            _db = db;
            _table = _db.Set<T>();
        }
        public void Create(T entity)
        {
            _table.Add(entity);
        }

        public void Delete(int id)
        {
            T existingEntity = _table.Find(id);
            if (null != existingEntity) { _table.Remove(existingEntity); }
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return _table.Where(predicate);
        }

        public T Get(int? id)
        {
            return _table.Find(id);
        }
        public T Get(int id)
        {
            return _table.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _table.ToList();
        }

        public void SaveChange()
        {
            _db.SaveChanges();
        }

        public void Update(T entity)
        {
            using (var db = new ModelDb())
            {
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
