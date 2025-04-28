using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookManagementAPI.Repositories.Interfaces
{
    /// <summary>
    /// Veri tabanı işlemleri için genel repository arayüzü
    /// </summary>
    /// <typeparam name="T">Entity tipi</typeparam>
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> ExistsAsync(int id);
    }
}