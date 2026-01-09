using LipometryAppAPI.Models;

namespace LipometryAppAPI.Repositories
{
    public interface IBodyMeasurementRepository  : IBaseBaseRepository<BodyMeasurement>
    {
        Task<IEnumerable<BodyMeasurement>> GetHistoryByPersonIdAsync(Guid personId, CancellationToken token = default);
    }
}
