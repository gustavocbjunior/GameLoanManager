using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameLoanManager.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(long id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<bool> ExistsAsync(long id);
        Task<T> InsertAsync(T item);
        Task<T> UpdateAsync(T item);
        Task<T> DeleteAsync(long id);
    }
}
