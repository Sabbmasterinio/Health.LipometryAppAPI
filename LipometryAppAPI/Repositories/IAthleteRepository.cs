using LipometryAppAPI.Models;

namespace LipometryAppAPI.Repositories
{
    public interface IAthleteRepository : IBaseRepository<Athlete>
    {
        Task<IEnumerable<Athlete>> GetBySportAsync(string sport);
    }
}