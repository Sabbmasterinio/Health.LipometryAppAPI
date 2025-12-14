using LipometryAppAPI.Contracts.Requests;
using LipometryAppAPI.Models;

namespace LipometryAppAPI.Services
{
    public interface IPersonService
    {
        Task<Person> CreateAsync(PersonCreate createPerson);
        Task<Person> UpdateAsync(int id, PersonUpdate updatePerson);
        Task RemoveAsync(int id);
    }
}