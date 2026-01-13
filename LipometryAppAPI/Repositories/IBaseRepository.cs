using LipometryAppAPI.Contracts.Models;
using System.Linq.Expressions;

namespace LipometryAppAPI.Repositories
{
    public interface IBaseRepository<T> : IBaseBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetByAgeGroupAsync(AgeGroup ageGroup, CancellationToken token = default);
        Task<IEnumerable<T>> GetByAgeRangeAsync((int? minAge, int? maxAge) ageRange, CancellationToken token = default);
        Task<PagedResult<T>> GetPagedAsync(
           int page,
           int pageSize,
           Expression<Func<T, bool>>? filter = null,
           CancellationToken token = default);
        Task<IEnumerable<T>> GetAdults18PlusAsync(CancellationToken token = default);
        Task<IEnumerable<T>> GetByGenderAsync(PersonGender gender, CancellationToken token = default);
        Task<HealthStatus?> GetHealthStatusAsync(Guid personId, CancellationToken token = default);
    }
}
