using LipometryAppAPI.Contracts.Models;
using LipometryAppAPI.Data;
using LipometryAppAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LipometryAppAPI.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class , IHasAttributes
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

        #region Implemented Methods of IBaseRepository<T>
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
        /// Asynchronously retrieves all entities that fall within the specified age group of type <typeparamref name="T"/> from the data source.
        /// </summary>
        /// <param name="ageGroup"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public async Task<IEnumerable<T>> GetByAgeGroupAsync(AgeGroup ageGroup, CancellationToken token = default)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);

            (int min, int? max) = ageGroup switch
            {
                AgeGroup.Child => (4, 12),
                AgeGroup.Teenager => (13, 17),
                AgeGroup.YoungAdult => (18, 25),
                AgeGroup.Adult => (26, 64),
                AgeGroup.Senior => (65, (int?)null),
                _ => throw new ArgumentOutOfRangeException(nameof(ageGroup))
            };

            var maxDob = today.AddYears(-min);
            var minDob = max.HasValue
                ? today.AddYears(-max.Value - 1).AddDays(1)
                : DateOnly.MinValue;

            return await _dbSet
                .Where(e => e.DateOfBirth <= maxDob &&
                            (max == null || e.DateOfBirth >= minDob))
                .AsNoTracking()
                .ToListAsync(token);
        }

        public async Task<IEnumerable<T>> GetByAgeRangeAsync((int? minAge, int? maxAge) ageRange, CancellationToken token = default)
        {
            var minAge = ageRange.minAge ?? 4;
            var maxAge = ageRange.maxAge ?? 120;

            if (minAge < 0 || maxAge < 0 || minAge > maxAge)
                return Enumerable.Empty<T>();

            var today = DateOnly.FromDateTime(DateTime.Today);


            var maxDob = today.AddYears(-minAge);

            var minDob = today.AddYears(-maxAge - 1).AddDays(1);

            return await _dbSet
                .Where(e => e.DateOfBirth >= minDob && e.DateOfBirth <= maxDob)
                .AsNoTracking()
                .ToListAsync(token);
        }


        /// <summary>
        /// Asynchronously retrieves a paged subset of entities of type <typeparamref name="T"/> that match the
        /// specified filter criteria.
        /// </summary>
        /// <remarks>The method applies the specified filter to the entity set and returns the requested
        /// page of results. The total count reflects the number of entities matching the filter. Paging is zero-based
        /// internally; the first page is page 1.</remarks>
        /// <param name="page">The one-based page number to retrieve. Must be greater than or equal to 1.</param>
        /// <param name="pageSize">The number of items to include in each page. Must be greater than 0.</param>
        /// <param name="filter">An optional expression used to filter the entities. If <see langword="null"/>, all entities of type
        /// <typeparamref name="T"/> are included.</param>
        /// <param name="token">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="PagedResult{T}"/>
        /// with the items for the specified page, the total count of matching entities, and paging information.</returns>
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

        /// <summary>
        /// Asynchronously retrieves a collection of people who are 18 years of age or older.
        /// </summary>
        /// <remarks>This method queries the underlying data source to identify people whose date of
        /// birth indicates they are at least 18 years old as of the current date.</remarks>
        /// <returns>A task that represents the asynchronous operation. The task result contains an  IEnumerable{T} of Person
        /// objects representing adults.</returns>
        public async Task<IEnumerable<T>> GetAdults18PlusAsync(CancellationToken token = default)
        {
            var cutoffDate = DateOnly.FromDateTime(DateTime.Today.AddYears(-18));
            return await _dbSet
                .Where(p => p.DateOfBirth <= cutoffDate)
                .AsNoTracking()
                .ToListAsync(token);
        }

        /// <summary>
        /// Retrieves a collection of people filtered by the specified gender.
        /// </summary>
        /// <param name="gender">The gender to filter the people by. Must be a valid <see cref="PersonGender"/> value.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="IEnumerable{T}"/> 
        /// of <see cref="Person"/> objects matching the specified gender. Returns an empty collection if no people
        /// match the criteria.</returns>
        public async Task<IEnumerable<T>> GetByGenderAsync(PersonGender gender, CancellationToken token = default)
        {
            return await _dbSet
                .Where(p => p.PersonGender == gender)
                .AsNoTracking()
                .ToListAsync(token);
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
        #endregion
    }
}