using LipometryAppAPI.Models;

namespace LipometryAppAPI.Repositories
{
    public interface IPersonRepository : IRepository<Person>
    {
        // Person-specific methods
        Task<IEnumerable<Person>> GetAdultsAsync();
        Task<IEnumerable<Person>> GetByGenderAsync(PersonGender gender);
        Task<Person?> GetByFullNameAsync(string firstName, string lastName);
    }
}
