using LipometryAppAPI.Data;
using LipometryAppAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LipometryAppAPI.Repositories
{
    public class AthleteRepository : BaseRepository<Athlete>, IAthleteRepository
    {
        public AthleteRepository(LipometryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Athlete>> GetBySportAsync(string sport)
        {
            return await _dbSet
                .Where(a => a.Sport.ToLower() == sport.ToLower())
                .ToListAsync();
        }
    }
}