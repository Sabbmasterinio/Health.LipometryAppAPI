// PersonRepository.cs
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

        public async Task<IEnumerable<Person>> GetAdultsAsync(int ageThreshold = 18)
        {
            var cutoffDate = DateOnly.FromDateTime(DateTime.Today.AddYears(-ageThreshold));
            return await _dbSet
                .Where(p => p.DateOfBirth <= cutoffDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Person>> GetByGenderAsync(PersonGender gender)
        {
            return await _dbSet
                .Where(p => p.PersonGender == gender)
                .ToListAsync();
        }

        public async Task<Person?> GetByFullNameAsync(string firstName, string lastName)
        {
            return await _dbSet
                .FirstOrDefaultAsync(p =>
                    p.FirstName.ToLower() == firstName.ToLower() &&
                    p.LastName.ToLower() == lastName.ToLower());
        }

        // Override base method if needed
        public override async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _dbSet
                .OrderBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .ToListAsync();
        }
    }
}