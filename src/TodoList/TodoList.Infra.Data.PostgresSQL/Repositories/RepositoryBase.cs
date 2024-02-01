using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TodoList.Domain.Interfaces;
using TodoList.Infra.Data.PostgresSQL.Context;

namespace TodoList.Infra.Data.PostgresSQL.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly TodoListDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public RepositoryBase(TodoListDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _dbSet = _dbContext.Set<T>();
        }

        public async Task Add(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<T> GetById(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IReadOnlyCollection<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IReadOnlyCollection<T>> GetByPredicate(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public virtual async Task Update(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task Remove(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}