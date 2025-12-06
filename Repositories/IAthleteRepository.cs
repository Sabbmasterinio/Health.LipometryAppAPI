// IAthleteRepository.cs
using LipometryAppAPI.Models;

namespace LipometryAppAPI.Repositories
{
    public interface IAthleteRepository : IRepository<Athlete>
    {
        Task<IEnumerable<Athlete>> GetBySportAsync(string sport);
        Task<IEnumerable<Athlete>> GetAthletesWithBMIAboveAsync(double bmiThreshold);
    }
}