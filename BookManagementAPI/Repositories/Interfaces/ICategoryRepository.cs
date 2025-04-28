using BookManagementAPI.Entities;

namespace BookManagementAPI.Repositories.Interfaces
{
    /// <summary>
    /// Kategori veri tabanı işlemleri için özel repository arayüzü
    /// </summary>
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category> GetCategoryWithBooksAsync(int id);
        Task<IEnumerable<Category>> GetAllCategoriesWithBooksAsync();
        Task<bool> HasBooksAsync(int id);
    }
}