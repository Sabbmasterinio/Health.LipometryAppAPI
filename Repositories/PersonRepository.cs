using LipometryAppAPI.Data;
using LipometryAppAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LipometryAppAPI.Repositories
{
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        public PersonRepository(LipometryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Person>> GetAdultsAsync()
        {
            return await _dbSet
                .Where(p => p.Age >= 18)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves a collection of persons filtered by the specified gender.
        /// </summary>
        /// <param name="gender">The gender to filter the persons by. Must be a valid <see cref="PersonGender"/> value.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="IEnumerable{T}"/> 
        /// of <see cref="Person"/> objects matching the specified gender. Returns an empty collection if no persons
        /// match the criteria.</returns>
        public async Task<IEnumerable<Person>> GetByGenderAsync(PersonGender gender)
        {
            return await _dbSet
                .Where(p => p.PersonGender == gender)
                .ToListAsync();
        }

        /// <summary>
        /// Asynchronously retrieves a person from the database whose first and last names match the specified values.
        /// </summary>
        /// <remarks>This method performs a case-insensitive comparison of the first and last
        /// names.</remarks>
        /// <param name="firstName">The first name of the person to search for. This value is case-insensitive.</param>
        /// <param name="lastName">The last name of the person to search for. This value is case-insensitive.</param>
        /// <returns>A <see cref="Person"/> object representing the person with the specified first and last names,  or <see
        /// langword="null"/> if no matching person is found.</returns>
        public async Task<Person?> GetByFullNameAsync(string firstName, string lastName)
        {
            return await _dbSet
                .FirstOrDefaultAsync(p =>
                    p.FirstName.ToLower() == firstName.ToLower() &&
                    p.LastName.ToLower() == lastName.ToLower());
        }

        // Override base method
        public override async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _dbSet
                .Where(p => p.GetType() == typeof(Person))
                .OrderBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .ToListAsync();
        }
    }
}