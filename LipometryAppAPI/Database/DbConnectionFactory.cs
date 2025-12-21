using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LipometryAppAPI.Database
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        #region Private Members
        private readonly string _connectionString;
        #endregion

        #region Constructors
        public DbConnectionFactory(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string not found");
        }
        #endregion

        #region Implemented Methods of IDbConnectionFactory
        public async Task<IDbConnection> CreateConnectionAsync(CancellationToken token = default)
        {
            var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync(token);
            return connection;
        }
        #endregion
    }
}
