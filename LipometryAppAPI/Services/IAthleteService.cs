using LipometryAppAPI.Contracts.Requests;
using LipometryAppAPI.Models;

namespace LipometryAppAPI.Services
{
    public interface IAthleteService
    {
        Task<Athlete> CreateAsync(AthleteCreate createAthlete);
        Task<Athlete> UpdateAsync(int id, AthleteUpdate updateAthlete);
    }
}