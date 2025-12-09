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

        /// <summary>
        /// Asynchronously retrieves a collection of people who are 18 years of age or older.
        /// </summary>
        /// <remarks>This method queries the underlying data source to identify people whose date of
        /// birth indicates they are at least 18 years old as of the current date.</remarks>
        /// <returns>A task that represents the asynchronous operation. The task result contains an  IEnumerable{T} of Person
        /// objects representing adults.</returns>
        public async Task<IEnumerable<Person>> GetAdultsAsync()
        {
            var cutoffDate = DateOnly.FromDateTime(DateTime.Today.AddYears(-18));
            return await _dbSet
                .Where(p => p.DateOfBirth <= cutoffDate)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves a collection of people filtered by the specified gender.
        /// </summary>
        /// <param name="gender">The gender to filter the people by. Must be a valid <see cref="PersonGender"/> value.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="IEnumerable{T}"/> 
        /// of <see cref="Person"/> objects matching the specified gender. Returns an empty collection if no people
        /// match the criteria.</returns>
        public async Task<IEnumerable<Person>> GetByGenderAsync(PersonGender gender)
        {
            return await _dbSet
                .Where(p => p.PersonGender == gender)
                .ToListAsync();
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