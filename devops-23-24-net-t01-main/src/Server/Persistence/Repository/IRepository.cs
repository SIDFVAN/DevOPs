using System.Linq.Expressions;

namespace Blanche.Server.Persistence.Repository
{
	public interface IRepository<T> : IDisposable
	{
		Task<T> GetAsync(Expression<Func<T, bool>> predicate);
		Task<T> GetAsync(Guid id);
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> predicate);

        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);

        IQueryable<T> Query(Expression<Func<T, bool>> predicate);
        IQueryable<T> Queryable();
    }
}