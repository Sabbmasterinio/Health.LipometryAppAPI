using LipometryAppAPI.Contracts.Requests;
using LipometryAppAPI.Models;

namespace LipometryAppAPI.Services
{
    public interface IAthleteService
    {
        Task<Athlete> CreateAsync(AthleteCreate createAthlete, CancellationToken token = default);
        Task<Athlete> UpdateAsync(Guid id, AthleteUpdate updateAthlete, CancellationToken token = default);
        Task RemoveAsync(Guid id, CancellationToken token = default);
    }
}