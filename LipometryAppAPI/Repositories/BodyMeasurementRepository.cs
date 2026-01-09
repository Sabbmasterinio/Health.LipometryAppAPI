using LipometryAppAPI.Data;
using LipometryAppAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LipometryAppAPI.Repositories
{
    public class BodyMeasurementRepository : BaseBaseRepository<BodyMeasurement> , IBodyMeasurementRepository
    {        
        public BodyMeasurementRepository(LipometryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<BodyMeasurement>> GetHistoryByPersonIdAsync(Guid personId, CancellationToken token = default)
        {
            return await _dbSet
                .Where(m => m.PersonId == personId)
                .OrderByDescending(m => m.MeasurementDate) // Newest first
                .AsNoTracking()
                .ToListAsync(token);
        }
    }
}
