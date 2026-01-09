using LipometryAppAPI.Contracts.Requests;
using LipometryAppAPI.Models;

namespace LipometryAppAPI.Services
{
    public interface IBodyMeasurementService
    {
        Task<BodyMeasurement> LogMeasurementAsync(BodyMeasurementCreateRequest request, CancellationToken token = default);

    }
}