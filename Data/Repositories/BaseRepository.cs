using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public abstract class BaseRepository<T> where T : class
    {
        protected DbSet<T> _entity;
        protected DbContext context;

        public BaseRepository()
        {
            this.context = new BankDbContext();
            _entity = context.Set<T>();
        }

        public T Create(T entity)
        {
            _entity.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public virtual IEnumerable<T> Read(Expression<Func<T, bool>> expression) => _entity.Where(expression);
    }
}
