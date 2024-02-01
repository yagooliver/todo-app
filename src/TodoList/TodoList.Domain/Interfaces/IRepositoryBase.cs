using System.Linq.Expressions;

namespace TodoList.Domain.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        Task Add(T entity);

        Task<T> GetById(Guid id);

        Task<IReadOnlyCollection<T>> GetAll();

        Task<IReadOnlyCollection<T>> GetByPredicate(Expression<Func<T,bool>> predicate);

        Task Update(T entity);

        Task Remove(T Entity);
    }
}