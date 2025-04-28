using BookManagementAPI.Entities;

namespace BookManagementAPI.Repositories.Interfaces
{
    /// <summary>
    /// Kitap veri tabanı işlemleri için özel repository arayüzü
    /// </summary>
    public interface IBookRepository : IRepository<Book>
    {
        Task<Book> GetBookWithDetailsAsync(int id);
        Task<IEnumerable<Book>> GetAllBooksWithDetailsAsync();
        Task<bool> HasEnrollmentsAsync(int id);
    }
}