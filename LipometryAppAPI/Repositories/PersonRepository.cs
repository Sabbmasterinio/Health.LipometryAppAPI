using LipometryAppAPI.Contracts.Models;
using LipometryAppAPI.Data;
using LipometryAppAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LipometryAppAPI.Repositories
{
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        #region Constructors
        public PersonRepository(LipometryContext context) : base(context)
        {
        }
        #endregion

        #region Implemented Methods of IPersonRepository
        
        #endregion

        #region Overridden methods
        /// <summary>
        /// Asynchronously retrieves all <see cref="Person"/> entities from the data source, ordered by last name and
        /// then by first name.
        /// </summary>
        /// <param name="token">A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an <see
        /// cref="IEnumerable{Person}"/> of all people, ordered by last name and then by first name. The collection is
        /// empty if no persons are found.</returns>
        public override async Task<IEnumerable<Person>> GetAllAsync(CancellationToken token = default)
        {
            return await _dbSet
                .Where(p => p.GetType() == typeof(Person))
                .OrderBy(p => p.LastName)
                .ThenBy(p=> p.FirstName)
                .AsNoTracking()
                .ToListAsync(token);
        }

        /// <summary>
        /// Asynchronously retrieves a <see cref="Person"/> entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the <see cref="Person"/> to retrieve.</param>
        /// <param name="token">A cancellation token that can be used to cancel the asynchronous operation. Optional.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="Person"/> with
        /// the specified identifier, or <see langword="null"/> if no matching entity is found.</returns>
        public override async Task<Person?> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            var person = await _dbSet.FindAsync(id, token);
            if (person != null && person.GetType() == typeof(Person))
            {
                return person;
            }
            return null;
        }
        #endregion
    }
}