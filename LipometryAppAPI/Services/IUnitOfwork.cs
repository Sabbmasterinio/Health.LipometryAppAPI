namespace LipometryAppAPI.Services
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}
