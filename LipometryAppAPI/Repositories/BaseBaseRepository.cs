using LipometryAppAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace LipometryAppAPI.Repositories
{
    public abstract class BaseBaseRepository<T> : IBaseBaseRepository<T> where T : class
    {
        #region Protected members
        protected readonly DbSet<T> _dbSet;
        #endregion

        #region Constructors
        protected BaseBaseRepository(LipometryContext context)
        {
            _dbSet = context.Set<T>();
        }
        #endregion
        /// <summary>
        /// Asynchronously retrieves an entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to retrieve.</param>
        /// <param name="token">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the entity of type
        /// <typeparamref name="T"/> if found; otherwise, <see langword="null"/>.</returns>
        public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            return await _dbSet.FindAsync(id, token);
        }

        /// <summary>
        /// Asynchronously retrieves all entities of type <typeparamref name="T"/> from the data source.
        /// </summary>
        /// <param name="token">A cancellation token that can be used to cancel the asynchronous operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="IEnumerable{T}"/>
        /// with all entities of type <typeparamref name="T"/>. The collection will be empty if no entities are found.</returns>
        public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken token = default)
        {
            return await _dbSet.AsNoTracking().ToListAsync(token);
        }

        /// <summary>
        /// Asynchronously adds the specified entity to the context for insertion into the database.
        /// </summary>
        /// <param name="entity">The entity to add to the context. Cannot be <c>null</c>.</param>
        /// <param name="token">A cancellation token that can be used to cancel the asynchronous operation. The default value is <see
        /// cref="CancellationToken.None"/>.</param>
        /// <returns>A task that represents the asynchronous add operation.</returns>
        public virtual async Task CreateAsync(T entity, CancellationToken token = default)
        {
            await _dbSet.AddAsync(entity, token);
        }

        /// <summary>
        /// Updates the specified entity in the underlying data store.
        /// </summary>
        /// <param name="entity">The entity to update. Must not be <see langword="null"/>.</param>
        /// <param name="token">A cancellation token that can be used to cancel the update operation. The default value is <see
        /// cref="CancellationToken.None"/>.</param>
        public virtual void Update(T entity, CancellationToken token = default)
        {
            _dbSet.Update(entity);
        }

        /// <summary>
        /// Asynchronously removes the entity with the specified identifier from the data set.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to remove.</param>
        /// <param name="token">A cancellation token that can be used to cancel the operation.</param>
        /// <returns><see langword="true"/> if the entity was found and removed; otherwise, <see langword="false"/>.</returns>
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

        /// <summary>
        /// Asynchronously determines whether an entity with the specified identifier exists in the data store.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to check for existence.</param>
        /// <param name="token">A cancellation token that can be used to cancel the asynchronous operation. The default value is <see
        /// cref="CancellationToken.None"/>.</param>
        /// <returns><see langword="true"/> if an entity with the specified identifier exists; otherwise, <see
        /// langword="false"/>.</returns>
        public virtual async Task<bool> ExistsAsync(Guid id, CancellationToken token = default)
        {
            return await _dbSet.FindAsync(id, token) != null;
        }
    }
}
