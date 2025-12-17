namespace LipometryAppAPI.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id, CancellationToken token = default);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken token = default);
        Task CreateAsync(T entity, CancellationToken token = default);
        void Update(T entity, CancellationToken token = default);
        Task RemoveAsync(Guid id, CancellationToken token = default);
        Task<bool> ExistsAsync(Guid id, CancellationToken token = default);
    }
}
