using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliveryService.Core.Interfaces.Repositories
{
    public interface IRepository<T>
        where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetAsync(long id);
        Task<T> CreateAsync(T item);
        Task UpdateAsync(T item);
        Task DeleteAsync(T item);
    }
}