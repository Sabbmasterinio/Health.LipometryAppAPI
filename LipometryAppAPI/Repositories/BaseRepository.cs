using LipometryAppAPI.Contracts.Models;
using LipometryAppAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace LipometryAppAPI.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        #region Protected members
        protected readonly DbSet<T> _dbSet;
        #endregion

        #region Constructors
        protected BaseRepository(LipometryContext context)
        {
            _dbSet = context.Set<T>();
        }
        #endregion

        #region Implemented Methods of IRepository<T>
        public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            return await _dbSet.FindAsync(id, token);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken token = default)
        {
            return await _dbSet.AsNoTracking().ToListAsync(token);
        }

        public virtual async Task<PagedResult<T>> GetPagedAsync(
            int page,
            int pageSize,
            Expression<Func<T, bool>>? filter = null,
            CancellationToken token= default)
        {
            IQueryable<T> query = _dbSet;

            query = query.Where(x => x.GetType() == typeof(T));

            if (filter != null)
                query = query.Where(filter);


            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<T>
            {
                Items = items,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };
        }

        public virtual async Task CreateAsync(T entity, CancellationToken token = default)
        {
            await _dbSet.AddAsync(entity, token);
        }

        public virtual void Update(T entity, CancellationToken token = default)
        {
            _dbSet.Update(entity);
        }

        public virtual async Task<bool> RemoveAsync(Guid id, CancellationToken token = default)
        {
            var entity = await _dbSet.FindAsync(id, token);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                return true;
            }
            return false;
        }
        
        public virtual async Task<bool> ExistsAsync(Guid id, CancellationToken token = default)
        {
            return await _dbSet.FindAsync(id, token) != null;
        }
        #endregion
    }
}