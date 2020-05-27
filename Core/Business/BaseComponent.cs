using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.Repositories;
namespace Core.Business
{
    public abstract class BaseComponent<T> where T : class
    {
        protected readonly BaseRepository<T> _repository;
        protected Func<T, Tuple<bool, string>> _validateOperation;

        public BaseComponent(BaseRepository<T> repository, Func<T, Tuple<bool, string>> validateOperation = null)
        {
            _repository = repository;
            _validateOperation = validateOperation;
        }

        public IEnumerable<T> Read(Expression<Func<T, bool>> expression) => _repository.Read(expression);

        public virtual OperationResult<T> Create(T entity)
        {

            OperationResult<T> result = new OperationResult<T>() { OperationType = Activator.CreateInstance<T>().GetType().Name };

            if (_validateOperation != null)
            {
                Tuple<bool, string> validationResult = _validateOperation(entity);
                if (!validationResult.Item1)
                {
                    result.Error = validationResult.Item2;
                    return result;
                };
            }
            T data = _repository.Create(entity);
            result.data = data;
            return result;
        }
    }
}
