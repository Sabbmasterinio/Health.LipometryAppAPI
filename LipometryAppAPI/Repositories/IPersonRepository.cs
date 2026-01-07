using LipometryAppAPI.Contracts.Models;
using LipometryAppAPI.Models;

namespace LipometryAppAPI.Repositories
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
        Task<IEnumerable<Person>> GetAdults18PlusAsync(CancellationToken token = default);
        Task<IEnumerable<Person>> GetByGenderAsync(PersonGender gender, CancellationToken token = default);
    }
}
