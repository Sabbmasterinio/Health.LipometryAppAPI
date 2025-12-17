using LipometryAppAPI.Contracts.Requests;
using LipometryAppAPI.Models;

namespace LipometryAppAPI.Services
{
    public interface IPersonService
    {
        Task<Person> CreateAsync(PersonCreate createPerson, CancellationToken token = default);
        Task<Person> UpdateAsync(Guid id, PersonUpdate updatePerson, CancellationToken token = default);
        Task RemoveAsync(Guid id, CancellationToken token = default);
    }
}