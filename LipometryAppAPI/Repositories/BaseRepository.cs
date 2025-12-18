using LipometryAppAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace LipometryAppAPI.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        #region Protected members
        protected readonly LipometryContext _context;
        protected readonly DbSet<T> _dbSet;
        #endregion

        #region Constructors
        protected BaseRepository(LipometryContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        #endregion

        #region Implemented methods of IRepository<T>
        public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            return await _dbSet.FindAsync(id, token);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken token = default)
        {
            return await _dbSet.AsNoTracking().ToListAsync(token);
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