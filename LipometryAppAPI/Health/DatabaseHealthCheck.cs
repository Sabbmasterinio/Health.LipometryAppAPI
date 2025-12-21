using LipometryAppAPI.Database;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace LipometryAppAPI.Health
{
    public class DatabaseHealthCheck : IHealthCheck
    {
        #region Public Members
        public const string Name = "Database";
        #endregion

        #region Private Members
        private readonly IDbConnectionFactory _connection;
        #endregion

        #region Constructors
        public DatabaseHealthCheck(IDbConnectionFactory factory)
        {
            _connection = factory;
        }
        #endregion

        #region Implemented Methods of IHealthCheck
        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            try
            {
                using var connection = await _connection.CreateConnectionAsync(cancellationToken);
                return HealthCheckResult.Healthy();
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy("Database unavailable", ex);
            }
        }
        #endregion
    }
}
