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
        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }
        
        public virtual async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public virtual async Task RemoveAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
                _dbSet.Remove(entity);
        }
        
        public virtual async Task<bool> ExistsAsync(int id)
        {
            return await _dbSet.FindAsync(id) != null;
        }
        #endregion
    }
}