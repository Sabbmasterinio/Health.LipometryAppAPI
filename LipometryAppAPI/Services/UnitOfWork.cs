using LipometryAppAPI.Data;

namespace LipometryAppAPI.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Private Members
        private readonly LipometryContext _context;
        public UnitOfWork(LipometryContext context)
        {
            _context = context;
        }
        #endregion

        #region Implemented Members of IUnitOfWork
        public Task<int> SaveChangesAsync(CancellationToken token = default)
            => _context.SaveChangesAsync(token);
        #endregion
    }

}
