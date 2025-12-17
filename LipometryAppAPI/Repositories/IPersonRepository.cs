using LipometryAppAPI.Contracts.Models;
using LipometryAppAPI.Models;

namespace LipometryAppAPI.Repositories
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
        Task<IEnumerable<Person>> GetAdultsAsync(CancellationToken token = default);
        Task<IEnumerable<Person>> GetByGenderAsync(PersonGender gender, CancellationToken token = default);
    }
}
