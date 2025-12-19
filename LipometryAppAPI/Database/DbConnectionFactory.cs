using LipometryAppAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LipometryAppAPI.Database
{

    public interface IDbConnectionFactory
    {
        Task<IDbConnection> CreateConnectionAsync(CancellationToken token = default);

    }
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly LipometryContext _context;


        public DbConnectionFactory(LipometryContext context)
        {
            _context = context;
        }
        public async Task<IDbConnection> CreateConnectionAsync(CancellationToken token = default)
        {
            await _context.Database.OpenConnectionAsync(token);
            return _context.Database.GetDbConnection();
        }
    }
}
