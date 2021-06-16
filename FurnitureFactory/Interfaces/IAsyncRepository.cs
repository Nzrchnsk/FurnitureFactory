using System.Collections.Generic;
using System.Threading.Tasks;

namespace FurnitureFactory.Interfaces
{
    public interface IAsyncRepository<T> 
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> AddAsync(T entity);
        Task<List<T>> AddAsyncRange(List<T> entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<int> CountAllAsync();
    }
}