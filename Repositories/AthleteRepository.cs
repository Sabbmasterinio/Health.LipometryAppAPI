// AthleteRepository.cs
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

        public async Task<IEnumerable<Athlete>> GetAthletesWithBMIAboveAsync(double bmiThreshold)
        {
            return await _dbSet
                .Where(a => a.WeightInKg.HasValue &&
                           a.HeightInCm.HasValue &&
                           a.HeightInCm.Value > 0)
                .Where(a => (a.WeightInKg.Value /
                           Math.Pow(a.HeightInCm.Value / 100, 2)) > bmiThreshold)
                .ToListAsync();
        }
    }
}