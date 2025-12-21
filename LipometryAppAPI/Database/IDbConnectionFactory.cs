using System.Data;

namespace LipometryAppAPI.Database
{
    public interface IDbConnectionFactory
    {
        Task<IDbConnection> CreateConnectionAsync(CancellationToken token = default);

    }
}
