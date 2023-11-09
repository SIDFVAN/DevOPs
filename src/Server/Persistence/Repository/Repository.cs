using Blanche.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Blanche.Server.Persistence.Repository
{
	public class Repository<T> : IRepository<T> where T : Entity
	{
		private readonly BlancheDbContext _dbContext;
		private readonly DbSet<T> _dbSet;

		public Repository(BlancheDbContext dbContext)
		{
			_dbContext = dbContext ?? throw new ArgumentNullException();
            _dbSet = _dbContext.Set<T>();
		}

        public async Task<T> GetAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).FirstOrDefaultAsync() ?? throw new ArgumentNullException(); ;
        }

        public async Task<T> GetAsync(Guid id)
        {
            return await _dbSet.FindAsync(id) ?? throw new ArgumentNullException();
        }

        public async Task<IEnumerable<T>> GetListAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            var items = _dbSet.Where(predicate);
            return await items.ToListAsync();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(T entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached) _dbSet.Attach(entity);
            _dbSet.Remove(entity);
        }

        public IQueryable<T> Query(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public IQueryable<T> Queryable()
        {
            return _dbSet;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}

