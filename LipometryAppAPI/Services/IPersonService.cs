using LipometryAppAPI.Contracts.Requests;
using LipometryAppAPI.Models;

namespace LipometryAppAPI.Services
{
    public interface IPersonService
    {
        Task<Person> CreateAsync(PersonCreateRequest createPerson, CancellationToken token = default);
        Task<Person> UpdateAsync(Guid id, PersonUpdateRequest updatePerson, CancellationToken token = default);
        Task<bool> RemoveAsync(Guid id, CancellationToken token = default);
    }
}