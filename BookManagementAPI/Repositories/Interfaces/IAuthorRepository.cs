using BookManagementAPI.Entities;

namespace BookManagementAPI.Repositories.Interfaces
{
    /// <summary>
    /// Yazar veri tabanı işlemleri için özel repository arayüzü
    /// </summary>
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<Author> GetAuthorWithBooksAsync(int id);
        Task<IEnumerable<Author>> GetAllAuthorsWithBooksAsync();
        Task<bool> HasBooksAsync(int id);
    }
}