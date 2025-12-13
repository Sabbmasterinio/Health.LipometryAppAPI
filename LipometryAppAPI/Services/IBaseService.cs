using LipometryAppAPI.Contracts.Requests;
using LipometryAppAPI.Models;

namespace LipometryAppAPI.Services
{
    public interface IBaseService<T> where T : class
    {
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(int id, T entity);
    }
}