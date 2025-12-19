using LipometryAppAPI.Data;
using LipometryAppAPI.Database;
using Microsoft.AspNetCore.Connections;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using System.Data.Common;

namespace LipometryAppAPI.Health
{
    public class DatabaseHealthCheck : IHealthCheck
    {
        public const string Name = "Database";
        private readonly string _connectionString;

        public DatabaseHealthCheck(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken)
        {
            try
            {
                using var conn = new SqlConnection(_connectionString);
                await conn.OpenAsync(cancellationToken);
                return HealthCheckResult.Healthy();
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy("DB unreachable", ex);
            }
        }
    }
}
