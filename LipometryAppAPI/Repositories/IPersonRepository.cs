using LipometryAppAPI.Contracts;
using LipometryAppAPI.Models;

namespace LipometryAppAPI.Repositories
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
        Task<IEnumerable<Person>> GetAdultsAsync();
        Task<IEnumerable<Person>> GetByGenderAsync(PersonGender gender);
    }
}
