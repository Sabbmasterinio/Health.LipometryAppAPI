using LipometryAppAPI.Contracts.Requests;
using LipometryAppAPI.Models;

namespace LipometryAppAPI.Services
{
    public interface IAthleteService
    {
        Task<Athlete> CreateAsync(AthleteCreateRequest createAthlete, CancellationToken token = default);
        Task<Athlete> UpdateAsync(Guid id, AthleteUpdateRequest updateAthlete, CancellationToken token = default);
        Task<bool> RemoveAsync(Guid id, CancellationToken token = default);
    }
}