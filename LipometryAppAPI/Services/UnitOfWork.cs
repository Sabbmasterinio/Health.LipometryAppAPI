using LipometryAppAPI.Data;

namespace LipometryAppAPI.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LipometryContext _context;
        public UnitOfWork(LipometryContext context)
        {
            _context = context;
        }

        public Task<int> SaveChangesAsync()
            => _context.SaveChangesAsync();
    }
    
}
