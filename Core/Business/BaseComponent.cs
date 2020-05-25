using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Data.Repositories;
namespace Core.Business
{
    public abstract class BaseComponent<T> where T : class
    {
        protected readonly BaseRepository<T> _repository;

        public BaseComponent(BaseRepository<T> repository)
        {
            _repository = repository;
        }

        public T Create(T entity) => _repository.Create(entity);
        public IEnumerable<T> Read(Expression<Func<T, bool>> expression) => _repository.Read(expression);

    }
}
